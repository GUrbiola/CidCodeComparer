using CidCodeComparer.Models;
using CidCodeComparer.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CidCodeComparer.Engine
{
    public class ComparisonEngine
    {
        /// <summary>
        /// When true, ignores all whitespace (spaces, tabs, newlines) and non-printable characters when comparing lines
        /// </summary>
        public bool IgnoreWhitespace { get; set; } = true;

        public ComparisonResult CompareFiles(string file1Path, string file2Path, string fileType)
        {
            var result = new ComparisonResult
            {
                FileType = fileType,
                File1Lines = File.ReadAllLines(file1Path),
                File2Lines = File.ReadAllLines(file2Path)
            };

            var parser = ParserFactory.GetParser(fileType);

            if (parser != null)
            {
                result.File1Structure = parser.Parse(file1Path);
                result.File2Structure = parser.Parse(file2Path);
            }

            result.Differences = ComputeDifferences(result.File1Lines, result.File2Lines);

            return result;
        }

        private List<LineDifference> ComputeDifferences(string[] file1Lines, string[] file2Lines)
        {
            var differences = new List<LineDifference>();
            var lcs = LongestCommonSubsequence(file1Lines, file2Lines);

            int i = 0, j = 0;
            int lineNumber = 0;

            while (i < file1Lines.Length || j < file2Lines.Length)
            {
                if (i < file1Lines.Length && j < file2Lines.Length && lcs.Contains((i, j)))
                {
                    differences.Add(new LineDifference
                    {
                        LineNumber = lineNumber++,
                        File1Content = file1Lines[i],
                        File2Content = file2Lines[j],
                        Type = DifferenceType.None,
                        File1LineNumber = i,
                        File2LineNumber = j
                    });
                    i++;
                    j++;
                }
                else if (i < file1Lines.Length && (j >= file2Lines.Length || !lcs.Contains((i, j))))
                {
                    differences.Add(new LineDifference
                    {
                        LineNumber = lineNumber++,
                        File1Content = file1Lines[i],
                        File2Content = string.Empty,
                        Type = DifferenceType.Removed,
                        File1LineNumber = i,
                        File2LineNumber = -1
                    });
                    i++;
                }
                else if (j < file2Lines.Length)
                {
                    differences.Add(new LineDifference
                    {
                        LineNumber = lineNumber++,
                        File1Content = string.Empty,
                        File2Content = file2Lines[j],
                        Type = DifferenceType.Added,
                        File1LineNumber = -1,
                        File2LineNumber = j
                    });
                    j++;
                }
            }

            return differences;
        }

        private HashSet<(int, int)> LongestCommonSubsequence(string[] file1, string[] file2)
        {
            int m = file1.Length;
            int n = file2.Length;
            int[,] dp = new int[m + 1, n + 1];

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (AreLinesEqual(file1[i - 1], file2[j - 1]))
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }

            var result = new HashSet<(int, int)>();
            int x = m, y = n;

            while (x > 0 && y > 0)
            {
                if (AreLinesEqual(file1[x - 1], file2[y - 1]))
                {
                    result.Add((x - 1, y - 1));
                    x--;
                    y--;
                }
                else if (dp[x - 1, y] > dp[x, y - 1])
                {
                    x--;
                }
                else
                {
                    y--;
                }
            }

            return result;
        }

        private bool AreLinesEqual(string line1, string line2)
        {
            if (IgnoreWhitespace)
            {
                // Remove all whitespace (spaces, tabs, newlines) and non-printable characters (control characters) and compare
                string normalized1 = new string(line1.Where(c => !char.IsWhiteSpace(c) && !char.IsControl(c)).ToArray());
                string normalized2 = new string(line2.Where(c => !char.IsWhiteSpace(c) && !char.IsControl(c)).ToArray());
                return normalized1 == normalized2;
            }
            else
            {
                return line1 == line2;
            }
        }
    }
}
