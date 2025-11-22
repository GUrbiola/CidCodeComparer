using System.Collections.Generic;

namespace CidCodeComparer.Models
{
    public class CodeNode
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int StartLine { get; set; }
        public int EndLine { get; set; }
        public List<CodeNode> Children { get; set; }
        public string Content { get; set; }

        // Detailed properties
        public string Description { get; set; }
        public string SourceCode { get; set; }
        public string AccessModifier { get; set; }
        public List<string> Modifiers { get; set; }

        // For methods and properties
        public string ReturnType { get; set; }
        public List<MethodParameter> Parameters { get; set; }

        // For fields and properties
        public string DataType { get; set; }

        // For classes, interfaces, etc.
        public List<string> BaseTypes { get; set; }
        public List<string> Attributes { get; set; }

        public CodeNode()
        {
            Children = new List<CodeNode>();
            Modifiers = new List<string>();
            Parameters = new List<MethodParameter>();
            BaseTypes = new List<string>();
            Attributes = new List<string>();
        }

        public override string ToString()
        {
            return $"{Name} ({Type})";
        }

        public string GetFullSignature()
        {
            switch (Type)
            {
                case "Method":
                    var modifiers = string.Join(" ", Modifiers);
                    var parameters = string.Join(", ", Parameters);
                    return $"{AccessModifier} {modifiers} {ReturnType} {Name}({parameters})".Trim();

                case "Property":
                    return $"{AccessModifier} {DataType} {Name}".Trim();

                case "Field":
                    return $"{AccessModifier} {DataType} {Name}".Trim();

                default:
                    return ToString();
            }
        }
    }

    public class MethodParameter
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public List<string> Modifiers { get; set; }

        public MethodParameter()
        {
            Modifiers = new List<string>();
        }

        public override string ToString()
        {
            var modifiers = Modifiers.Count > 0 ? string.Join(" ", Modifiers) + " " : "";
            var defaultVal = !string.IsNullOrEmpty(DefaultValue) ? $" = {DefaultValue}" : "";
            return $"{modifiers}{Type} {Name}{defaultVal}";
        }
    }
}
