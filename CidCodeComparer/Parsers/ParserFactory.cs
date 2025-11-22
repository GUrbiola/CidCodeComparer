namespace CidCodeComparer.Parsers
{
    public static class ParserFactory
    {
        public static IParser GetParser(string fileType)
        {
            switch (fileType)
            {
                case "C#":
                    return new CSharpParser();
                case "JavaScript":
                    return new JavaScriptParser();
                case "HTML":
                    return new HtmlParser();
                case "XML":
                    return new XmlParser();
                case "JSON":
                    return new JsonParser();
                case "Text":
                    return null;
                default:
                    return null;
            }
        }
    }
}
