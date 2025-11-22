using CidCodeComparer.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CidCodeComparer.Parsers
{
    public class CSharpParser : IParser
    {
        public string GetFileExtension()
        {
            return ".cs";
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
                string code = File.ReadAllText(filePath);
                root.SourceCode = code.Trim();

                // Parse using Roslyn
                SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
                var syntaxRoot = tree.GetRoot();

                root.EndLine = syntaxRoot.GetLocation().GetLineSpan().EndLinePosition.Line;

                // Parse the structure
                ParseSyntaxNode(syntaxRoot, root, code);
            }
            catch (Exception ex)
            {
                // If parsing fails, return basic structure with error info
                root.Description = $"Error parsing file: {ex.Message}";
            }

            return root;
        }

        private void ParseSyntaxNode(SyntaxNode node, CodeNode parentNode, string sourceCode)
        {
            foreach (var child in node.ChildNodes())
            {
                CodeNode codeNode = null;

                switch (child)
                {
                    case NamespaceDeclarationSyntax namespaceDecl:
                        codeNode = ParseNamespace(namespaceDecl, sourceCode);
                        break;

                    case FileScopedNamespaceDeclarationSyntax fileScopedNamespace:
                        codeNode = ParseFileScopedNamespace(fileScopedNamespace, sourceCode);
                        break;

                    case ClassDeclarationSyntax classDecl:
                        codeNode = ParseClass(classDecl, sourceCode);
                        break;

                    case InterfaceDeclarationSyntax interfaceDecl:
                        codeNode = ParseInterface(interfaceDecl, sourceCode);
                        break;

                    case StructDeclarationSyntax structDecl:
                        codeNode = ParseStruct(structDecl, sourceCode);
                        break;

                    case EnumDeclarationSyntax enumDecl:
                        codeNode = ParseEnum(enumDecl, sourceCode);
                        break;

                    case RecordDeclarationSyntax recordDecl:
                        codeNode = ParseRecord(recordDecl, sourceCode);
                        break;

                    case MethodDeclarationSyntax methodDecl:
                        codeNode = ParseMethod(methodDecl, sourceCode);
                        break;

                    case PropertyDeclarationSyntax propertyDecl:
                        codeNode = ParseProperty(propertyDecl, sourceCode);
                        break;

                    case FieldDeclarationSyntax fieldDecl:
                        codeNode = ParseField(fieldDecl, sourceCode);
                        break;

                    case ConstructorDeclarationSyntax constructorDecl:
                        codeNode = ParseConstructor(constructorDecl, sourceCode);
                        break;

                    case EventDeclarationSyntax eventDecl:
                        codeNode = ParseEvent(eventDecl, sourceCode);
                        break;
                }

                if (codeNode != null)
                {
                    parentNode.Children.Add(codeNode);

                    // Recursively parse children for container types
                    if (IsContainerType(child))
                    {
                        ParseSyntaxNode(child, codeNode, sourceCode);
                    }
                }
            }
        }

        private bool IsContainerType(SyntaxNode node)
        {
            return node is NamespaceDeclarationSyntax ||
                   node is FileScopedNamespaceDeclarationSyntax ||
                   node is ClassDeclarationSyntax ||
                   node is InterfaceDeclarationSyntax ||
                   node is StructDeclarationSyntax ||
                   node is RecordDeclarationSyntax ||
                   node is EnumDeclarationSyntax;
        }

        private CodeNode ParseNamespace(NamespaceDeclarationSyntax namespaceDecl, string sourceCode)
        {
            var lineSpan = namespaceDecl.GetLocation().GetLineSpan();
            return new CodeNode
            {
                Name = namespaceDecl.Name.ToString(),
                Type = "Namespace",
                StartLine = lineSpan.StartLinePosition.Line,
                EndLine = lineSpan.EndLinePosition.Line,
                Description = "Namespace declaration",
                SourceCode = GetSourceText(namespaceDecl, sourceCode)
            };
        }

        private CodeNode ParseFileScopedNamespace(FileScopedNamespaceDeclarationSyntax namespaceDecl, string sourceCode)
        {
            var lineSpan = namespaceDecl.GetLocation().GetLineSpan();
            return new CodeNode
            {
                Name = namespaceDecl.Name.ToString(),
                Type = "Namespace",
                StartLine = lineSpan.StartLinePosition.Line,
                EndLine = lineSpan.EndLinePosition.Line,
                Description = "File-scoped namespace declaration",
                SourceCode = GetSourceText(namespaceDecl, sourceCode),
                Modifiers = new List<string> { "file-scoped" }
            };
        }

        private CodeNode ParseClass(ClassDeclarationSyntax classDecl, string sourceCode)
        {
            var lineSpan = classDecl.GetLocation().GetLineSpan();
            var node = new CodeNode
            {
                Name = classDecl.Identifier.Text,
                Type = "Class",
                StartLine = lineSpan.StartLinePosition.Line,
                EndLine = lineSpan.EndLinePosition.Line,
                Description = "Class declaration",
                SourceCode = GetSourceText(classDecl, sourceCode),
                AccessModifier = GetAccessModifier(classDecl.Modifiers)
            };

            // Get modifiers
            foreach (var modifier in classDecl.Modifiers)
            {
                var modText = modifier.Text;
                if (modText != "public" && modText != "private" && modText != "protected" && modText != "internal")
                {
                    node.Modifiers.Add(modText);
                }
            }

            // Get base types
            if (classDecl.BaseList != null)
            {
                foreach (var baseType in classDecl.BaseList.Types)
                {
                    node.BaseTypes.Add(baseType.Type.ToString());
                }
            }

            // Get attributes
            foreach (var attributeList in classDecl.AttributeLists)
            {
                foreach (var attribute in attributeList.Attributes)
                {
                    node.Attributes.Add(attribute.ToString());
                }
            }

            return node;
        }

        private CodeNode ParseInterface(InterfaceDeclarationSyntax interfaceDecl, string sourceCode)
        {
            var lineSpan = interfaceDecl.GetLocation().GetLineSpan();
            var node = new CodeNode
            {
                Name = interfaceDecl.Identifier.Text,
                Type = "Interface",
                StartLine = lineSpan.StartLinePosition.Line,
                EndLine = lineSpan.EndLinePosition.Line,
                Description = "Interface declaration",
                SourceCode = GetSourceText(interfaceDecl, sourceCode),
                AccessModifier = GetAccessModifier(interfaceDecl.Modifiers)
            };

            // Get modifiers
            foreach (var modifier in interfaceDecl.Modifiers)
            {
                var modText = modifier.Text;
                if (modText != "public" && modText != "private" && modText != "protected" && modText != "internal")
                {
                    node.Modifiers.Add(modText);
                }
            }

            // Get base interfaces
            if (interfaceDecl.BaseList != null)
            {
                foreach (var baseType in interfaceDecl.BaseList.Types)
                {
                    node.BaseTypes.Add(baseType.Type.ToString());
                }
            }

            return node;
        }

        private CodeNode ParseStruct(StructDeclarationSyntax structDecl, string sourceCode)
        {
            var lineSpan = structDecl.GetLocation().GetLineSpan();
            var node = new CodeNode
            {
                Name = structDecl.Identifier.Text,
                Type = "Struct",
                StartLine = lineSpan.StartLinePosition.Line,
                EndLine = lineSpan.EndLinePosition.Line,
                Description = "Struct declaration",
                SourceCode = GetSourceText(structDecl, sourceCode),
                AccessModifier = GetAccessModifier(structDecl.Modifiers)
            };

            foreach (var modifier in structDecl.Modifiers)
            {
                var modText = modifier.Text;
                if (modText != "public" && modText != "private" && modText != "protected" && modText != "internal")
                {
                    node.Modifiers.Add(modText);
                }
            }

            if (structDecl.BaseList != null)
            {
                foreach (var baseType in structDecl.BaseList.Types)
                {
                    node.BaseTypes.Add(baseType.Type.ToString());
                }
            }

            return node;
        }

        private CodeNode ParseEnum(EnumDeclarationSyntax enumDecl, string sourceCode)
        {
            var lineSpan = enumDecl.GetLocation().GetLineSpan();
            return new CodeNode
            {
                Name = enumDecl.Identifier.Text,
                Type = "Enum",
                StartLine = lineSpan.StartLinePosition.Line,
                EndLine = lineSpan.EndLinePosition.Line,
                Description = "Enum declaration",
                SourceCode = GetSourceText(enumDecl, sourceCode),
                AccessModifier = GetAccessModifier(enumDecl.Modifiers)
            };
        }

        private CodeNode ParseRecord(RecordDeclarationSyntax recordDecl, string sourceCode)
        {
            var lineSpan = recordDecl.GetLocation().GetLineSpan();
            var node = new CodeNode
            {
                Name = recordDecl.Identifier.Text,
                Type = "Record",
                StartLine = lineSpan.StartLinePosition.Line,
                EndLine = lineSpan.EndLinePosition.Line,
                Description = "Record declaration",
                SourceCode = GetSourceText(recordDecl, sourceCode),
                AccessModifier = GetAccessModifier(recordDecl.Modifiers)
            };

            foreach (var modifier in recordDecl.Modifiers)
            {
                var modText = modifier.Text;
                if (modText != "public" && modText != "private" && modText != "protected" && modText != "internal")
                {
                    node.Modifiers.Add(modText);
                }
            }

            if (recordDecl.BaseList != null)
            {
                foreach (var baseType in recordDecl.BaseList.Types)
                {
                    node.BaseTypes.Add(baseType.Type.ToString());
                }
            }

            return node;
        }

        private CodeNode ParseMethod(MethodDeclarationSyntax methodDecl, string sourceCode)
        {
            var lineSpan = methodDecl.GetLocation().GetLineSpan();
            var node = new CodeNode
            {
                Name = methodDecl.Identifier.Text,
                Type = "Method",
                StartLine = lineSpan.StartLinePosition.Line,
                EndLine = lineSpan.EndLinePosition.Line,
                Description = "Method declaration",
                SourceCode = GetSourceText(methodDecl, sourceCode),
                AccessModifier = GetAccessModifier(methodDecl.Modifiers),
                ReturnType = methodDecl.ReturnType.ToString()
            };

            // Get modifiers
            foreach (var modifier in methodDecl.Modifiers)
            {
                var modText = modifier.Text;
                if (modText != "public" && modText != "private" && modText != "protected" && modText != "internal")
                {
                    node.Modifiers.Add(modText);
                }
            }

            // Get parameters
            foreach (var param in methodDecl.ParameterList.Parameters)
            {
                var methodParam = new MethodParameter
                {
                    Name = param.Identifier.Text,
                    Type = param.Type?.ToString() ?? "var"
                };

                // Get parameter modifiers (ref, out, in, params)
                foreach (var mod in param.Modifiers)
                {
                    methodParam.Modifiers.Add(mod.Text);
                }

                // Get default value
                if (param.Default != null)
                {
                    methodParam.DefaultValue = param.Default.Value.ToString();
                }

                node.Parameters.Add(methodParam);
            }

            // Get attributes
            foreach (var attributeList in methodDecl.AttributeLists)
            {
                foreach (var attribute in attributeList.Attributes)
                {
                    node.Attributes.Add(attribute.ToString());
                }
            }

            return node;
        }

        private CodeNode ParseProperty(PropertyDeclarationSyntax propertyDecl, string sourceCode)
        {
            var lineSpan = propertyDecl.GetLocation().GetLineSpan();
            var node = new CodeNode
            {
                Name = propertyDecl.Identifier.Text,
                Type = "Property",
                StartLine = lineSpan.StartLinePosition.Line,
                EndLine = lineSpan.EndLinePosition.Line,
                Description = "Property declaration",
                SourceCode = GetSourceText(propertyDecl, sourceCode),
                AccessModifier = GetAccessModifier(propertyDecl.Modifiers),
                DataType = propertyDecl.Type.ToString()
            };

            // Get modifiers
            foreach (var modifier in propertyDecl.Modifiers)
            {
                var modText = modifier.Text;
                if (modText != "public" && modText != "private" && modText != "protected" && modText != "internal")
                {
                    node.Modifiers.Add(modText);
                }
            }

            // Get attributes
            foreach (var attributeList in propertyDecl.AttributeLists)
            {
                foreach (var attribute in attributeList.Attributes)
                {
                    node.Attributes.Add(attribute.ToString());
                }
            }

            return node;
        }

        private CodeNode ParseField(FieldDeclarationSyntax fieldDecl, string sourceCode)
        {
            var lineSpan = fieldDecl.GetLocation().GetLineSpan();

            // Fields can declare multiple variables, so we create a node for each
            var nodes = new List<CodeNode>();

            foreach (var variable in fieldDecl.Declaration.Variables)
            {
                var node = new CodeNode
                {
                    Name = variable.Identifier.Text,
                    Type = "Field",
                    StartLine = lineSpan.StartLinePosition.Line,
                    EndLine = lineSpan.EndLinePosition.Line,
                    Description = "Field declaration",
                    SourceCode = GetSourceText(fieldDecl, sourceCode),
                    AccessModifier = GetAccessModifier(fieldDecl.Modifiers),
                    DataType = fieldDecl.Declaration.Type.ToString()
                };

                // Get modifiers
                foreach (var modifier in fieldDecl.Modifiers)
                {
                    var modText = modifier.Text;
                    if (modText != "public" && modText != "private" && modText != "protected" && modText != "internal")
                    {
                        node.Modifiers.Add(modText);
                    }
                }

                // Get attributes
                foreach (var attributeList in fieldDecl.AttributeLists)
                {
                    foreach (var attribute in attributeList.Attributes)
                    {
                        node.Attributes.Add(attribute.ToString());
                    }
                }

                nodes.Add(node);
            }

            // Return first node (caller will need to handle multiple fields)
            // For simplicity, we're returning just the first one
            // In a more complete implementation, you might want to handle this differently
            return nodes.FirstOrDefault();
        }

        private CodeNode ParseConstructor(ConstructorDeclarationSyntax constructorDecl, string sourceCode)
        {
            var lineSpan = constructorDecl.GetLocation().GetLineSpan();
            var node = new CodeNode
            {
                Name = constructorDecl.Identifier.Text,
                Type = "Constructor",
                StartLine = lineSpan.StartLinePosition.Line,
                EndLine = lineSpan.EndLinePosition.Line,
                Description = "Constructor declaration",
                SourceCode = GetSourceText(constructorDecl, sourceCode),
                AccessModifier = GetAccessModifier(constructorDecl.Modifiers)
            };

            // Get modifiers
            foreach (var modifier in constructorDecl.Modifiers)
            {
                var modText = modifier.Text;
                if (modText != "public" && modText != "private" && modText != "protected" && modText != "internal")
                {
                    node.Modifiers.Add(modText);
                }
            }

            // Get parameters
            foreach (var param in constructorDecl.ParameterList.Parameters)
            {
                var methodParam = new MethodParameter
                {
                    Name = param.Identifier.Text,
                    Type = param.Type?.ToString() ?? "var"
                };

                foreach (var mod in param.Modifiers)
                {
                    methodParam.Modifiers.Add(mod.Text);
                }

                if (param.Default != null)
                {
                    methodParam.DefaultValue = param.Default.Value.ToString();
                }

                node.Parameters.Add(methodParam);
            }

            return node;
        }

        private CodeNode ParseEvent(EventDeclarationSyntax eventDecl, string sourceCode)
        {
            var lineSpan = eventDecl.GetLocation().GetLineSpan();
            return new CodeNode
            {
                Name = eventDecl.Identifier.Text,
                Type = "Event",
                StartLine = lineSpan.StartLinePosition.Line,
                EndLine = lineSpan.EndLinePosition.Line,
                Description = "Event declaration",
                SourceCode = GetSourceText(eventDecl, sourceCode),
                AccessModifier = GetAccessModifier(eventDecl.Modifiers),
                DataType = eventDecl.Type.ToString()
            };
        }

        private string GetAccessModifier(SyntaxTokenList modifiers)
        {
            foreach (var modifier in modifiers)
            {
                var text = modifier.Text;
                if (text == "public" || text == "private" || text == "protected" || text == "internal")
                {
                    return text;
                }
            }
            return "private"; // Default access modifier
        }

        private string GetSourceText(SyntaxNode node, string sourceCode)
        {
            try
            {
                // Get the full string and trim all leading/trailing whitespace, tabs, and newlines
                string sourceText = node.ToFullString();
                return sourceText.Trim();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
