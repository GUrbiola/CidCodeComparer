using CidCodeComparer.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace CidCodeComparer.Parsers
{
    public class XmlParser : IParser
    {
        public string GetFileExtension()
        {
            return ".xml";
        }

        public CodeNode Parse(string filePath)
        {
            var root = new CodeNode
            {
                Name = Path.GetFileName(filePath),
                Type = "File",
                StartLine = 0
            };

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                if (xmlDoc.DocumentElement != null)
                {
                    ParseXmlNode(xmlDoc.DocumentElement, root, 0);
                }

                var lines = File.ReadAllLines(filePath);
                root.EndLine = lines.Length - 1;
            }
            catch
            {
            }

            return root;
        }

        private int ParseXmlNode(XmlNode xmlNode, CodeNode parent, int lineNumber)
        {
            var node = new CodeNode
            {
                Name = xmlNode.Name,
                Type = "Element",
                StartLine = lineNumber
            };

            parent.Children.Add(node);

            foreach (XmlNode child in xmlNode.ChildNodes)
            {
                if (child.NodeType == XmlNodeType.Element)
                {
                    lineNumber = ParseXmlNode(child, node, lineNumber + 1);
                }
            }

            node.EndLine = lineNumber;
            return lineNumber;
        }
    }
}
