namespace CidCodeComparer.Models
{
    public class LineDifference
    {
        public int LineNumber { get; set; }
        public string File1Content { get; set; }
        public string File2Content { get; set; }
        public DifferenceType Type { get; set; }
        public int File1LineNumber { get; set; }
        public int File2LineNumber { get; set; }
    }
}
