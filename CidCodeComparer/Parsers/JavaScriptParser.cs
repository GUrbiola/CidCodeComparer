using CidCodeComparer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CidCodeComparer.Parsers
{
    public class JavaScriptParser : IParser
    {
        public string GetFileExtension()
        {
            return ".js";
        }

        public CodeNode Parse(string filePath)
        {
            var root = new CodeNode
            {
                Name = Path.GetFileName(filePath),
                Type = "File",
                StartLine = 0
            };

            var lines = File.ReadAllLines(filePath);
            root.EndLine = lines.Length - 1;

            ParseFunctionsAndClasses(lines, root);

            return root;
        }

        private void ParseFunctionsAndClasses(string[] lines, CodeNode parent)
        {
            var functionPattern = @"^\s*function\s+([\w]+)\s*\(";
            var arrowFunctionPattern = @"^\s*(const|let|var)\s+([\w]+)\s*=\s*(\([\w,\s]*\)|[\w]+)\s*=>";
            var classPattern = @"^\s*class\s+([\w]+)";

            Stack<CodeNode> nodeStack = new Stack<CodeNode>();
            nodeStack.Push(parent);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string trimmedLine = line.Trim();

                if (string.IsNullOrWhiteSpace(trimmedLine) || trimmedLine.StartsWith("//"))
                    continue;

                var classMatch = Regex.Match(line, classPattern);
                if (classMatch.Success)
                {
                    var node = new CodeNode
                    {
                        Name = classMatch.Groups[1].Value,
                        Type = "Class",
                        StartLine = i
                    };

                    parent.Children.Add(node);
                    nodeStack.Push(node);
                    continue;
                }

                var functionMatch = Regex.Match(line, functionPattern);
                if (functionMatch.Success)
                {
                    var node = new CodeNode
                    {
                        Name = functionMatch.Groups[1].Value,
                        Type = "Function",
                        StartLine = i
                    };

                    nodeStack.Peek().Children.Add(node);
                    continue;
                }

                var arrowMatch = Regex.Match(line, arrowFunctionPattern);
                if (arrowMatch.Success)
                {
                    var node = new CodeNode
                    {
                        Name = arrowMatch.Groups[2].Value,
                        Type = "Function",
                        StartLine = i
                    };

                    nodeStack.Peek().Children.Add(node);
                    continue;
                }

                if (trimmedLine == "}")
                {
                    if (nodeStack.Count > 1)
                    {
                        var currentNode = nodeStack.Pop();
                        currentNode.EndLine = i;
                    }
                }
            }
        }
    }
}
