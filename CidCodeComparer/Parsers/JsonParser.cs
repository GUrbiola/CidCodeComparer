using CidCodeComparer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CidCodeComparer.Parsers
{
    public class JsonParser : IParser
    {
        public string GetFileExtension()
        {
            return ".json";
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

            ParseJsonStructure(lines, root);

            return root;
        }

        private void ParseJsonStructure(string[] lines, CodeNode parent)
        {
            var propertyPattern = @"""([\w]+)""\s*:";
            Stack<CodeNode> nodeStack = new Stack<CodeNode>();
            nodeStack.Push(parent);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string trimmedLine = line.Trim();

                if (string.IsNullOrWhiteSpace(trimmedLine))
                    continue;

                var match = Regex.Match(line, propertyPattern);
                if (match.Success)
                {
                    var propertyName = match.Groups[1].Value;
                    var isObject = trimmedLine.Contains("{");
                    var isArray = trimmedLine.Contains("[");

                    if (isObject || isArray)
                    {
                        var node = new CodeNode
                        {
                            Name = propertyName,
                            Type = isArray ? "Array" : "Object",
                            StartLine = i
                        };

                        nodeStack.Peek().Children.Add(node);
                        nodeStack.Push(node);
                    }
                    else
                    {
                        var node = new CodeNode
                        {
                            Name = propertyName,
                            Type = "Property",
                            StartLine = i,
                            EndLine = i
                        };

                        nodeStack.Peek().Children.Add(node);
                    }
                }

                if (trimmedLine.StartsWith("}") || trimmedLine.StartsWith("]"))
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
