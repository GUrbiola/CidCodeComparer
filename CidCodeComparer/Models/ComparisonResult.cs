using System.Collections.Generic;

namespace CidCodeComparer.Models
{
    public class ComparisonResult
    {
        public List<LineDifference> Differences { get; set; }
        public CodeNode File1Structure { get; set; }
        public CodeNode File2Structure { get; set; }
        public string[] File1Lines { get; set; }
        public string[] File2Lines { get; set; }
        public string FileType { get; set; }

        public ComparisonResult()
        {
            Differences = new List<LineDifference>();
        }
    }
}
