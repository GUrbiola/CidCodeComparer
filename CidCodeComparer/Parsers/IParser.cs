using CidCodeComparer.Models;

namespace CidCodeComparer.Parsers
{
    public interface IParser
    {
        CodeNode Parse(string filePath);
        string GetFileExtension();
    }
}
