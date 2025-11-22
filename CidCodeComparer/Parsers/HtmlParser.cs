using CidCodeComparer.Models;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace CidCodeComparer.Parsers
{
    public class HtmlParser : IParser
    {
        public string GetFileExtension()
        {
            return ".html";
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

            ParseElements(lines, root);

            return root;
        }

        private void ParseElements(string[] lines, CodeNode parent)
        {
            var openTagPattern = @"<([\w]+)[\s>]";
            var closeTagPattern = @"</([\w]+)>";

            Stack<CodeNode> nodeStack = new Stack<CodeNode>();
            nodeStack.Push(parent);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string trimmedLine = line.Trim();

                if (string.IsNullOrWhiteSpace(trimmedLine))
                    continue;

                var openMatches = Regex.Matches(line, openTagPattern);
                foreach (Match match in openMatches)
                {
                    var tagName = match.Groups[1].Value.ToLower();

                    if (tagName == "html" || tagName == "head" || tagName == "body" ||
                        tagName == "div" || tagName == "section" || tagName == "article" ||
                        tagName == "header" || tagName == "footer" || tagName == "nav")
                    {
                        var node = new CodeNode
                        {
                            Name = $"<{tagName}>",
                            Type = "Element",
                            StartLine = i
                        };

                        nodeStack.Peek().Children.Add(node);

                        if (!line.Contains($"</{tagName}>"))
                        {
                            nodeStack.Push(node);
                        }
                    }
                }

                var closeMatches = Regex.Matches(line, closeTagPattern);
                foreach (Match match in closeMatches)
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
