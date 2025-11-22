using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CidCodeComparer.Engine
{
    /// <summary>
    /// Formats and writes code to a file with proper indentation
    /// </summary>
    public class CodeFormatter
    {
        private readonly string _code;
        private readonly string _indentString;
        private readonly int _baseIndentLevel;

        /// <summary>
        /// Gets or sets the namespace for the generated class. Defaults to "Default" if not set.
        /// </summary>
        public string Namespace { get; set; } = "Default";

        /// <summary>
        /// Creates a new CodeFormatter instance
        /// </summary>
        /// <param name="code">The code to format and write</param>
        /// <param name="indentString">The string to use for indentation (default: 4 spaces)</param>
        /// <param name="baseIndentLevel">The base indentation level (default: 0)</param>
        public CodeFormatter(string code, string indentString = "    ", int baseIndentLevel = 0)
        {
            _code = code ?? string.Empty;
            _indentString = indentString;
            _baseIndentLevel = baseIndentLevel;
        }

        /// <summary>
        /// Writes the formatted code to a file
        /// </summary>
        /// <param name="filePath">Path to the output file</param>
        public void WriteToFile(string filePath)
        {
            var formattedLines = FormatCode();
            File.WriteAllLines(filePath, formattedLines);
        }

        /// <summary>
        /// Returns the formatted code as a string
        /// </summary>
        public string GetFormattedCode()
        {
            var formattedLines = FormatCode();
            return string.Join(Environment.NewLine, formattedLines);
        }

        /// <summary>
        /// Formats the code with proper indentation
        /// </summary>
        private List<string> FormatCode()
        {
            var lines = _code.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var formattedLines = new List<string>();
            int currentIndentLevel = _baseIndentLevel;

            foreach (var line in lines)
            {
                string trimmedLine = line.Trim();

                // Skip empty lines but preserve them
                if (string.IsNullOrWhiteSpace(trimmedLine))
                {
                    formattedLines.Add(string.Empty);
                    continue;
                }

                // Count braces on this line
                int openBraces = CountChar(trimmedLine, '{');
                int closeBraces = CountChar(trimmedLine, '}');

                // Decrease indent if line starts with closing brace
                if (trimmedLine.StartsWith("}"))
                {
                    currentIndentLevel = Math.Max(0, currentIndentLevel - 1);
                }

                // Add the line with proper indentation
                string indent = string.Concat(Enumerable.Repeat(_indentString, currentIndentLevel));
                formattedLines.Add(indent + trimmedLine);

                // Adjust indent level based on braces
                int netBraceChange = openBraces - closeBraces;

                // Special handling: if line started with }, we already decreased, so add back before calculating
                if (trimmedLine.StartsWith("}") && !trimmedLine.StartsWith("} "))
                {
                    // Line is just a closing brace, netBraceChange should be -1
                    // We already decreased by 1 before printing, so don't change anything more
                }
                else if (trimmedLine.StartsWith("}"))
                {
                    // Line starts with } but has more (like "} else {")
                    // We already decreased by 1, now apply the net change excluding that first brace
                    currentIndentLevel = Math.Max(0, currentIndentLevel + netBraceChange + 1);
                }
                else
                {
                    // Normal line - just apply net change
                    currentIndentLevel = Math.Max(0, currentIndentLevel + netBraceChange);
                }
            }

            return formattedLines;
        }


        /// <summary>
        /// Counts occurrences of a character in a string, excluding those in strings or comments
        /// </summary>
        private int CountChar(string line, char ch)
        {
            int count = 0;
            bool inString = false;
            bool inChar = false;
            bool inLineComment = false;
            bool inBlockComment = false;
            bool escaped = false;

            for (int i = 0; i < line.Length; i++)
            {
                char current = line[i];
                char next = (i + 1 < line.Length) ? line[i + 1] : '\0';

                if (escaped)
                {
                    escaped = false;
                    continue;
                }

                if (current == '\\' && (inString || inChar))
                {
                    escaped = true;
                    continue;
                }

                // Check for comments
                if (!inString && !inChar)
                {
                    if (current == '/' && next == '/')
                    {
                        inLineComment = true;
                        continue;
                    }
                    if (current == '/' && next == '*')
                    {
                        inBlockComment = true;
                        i++;
                        continue;
                    }
                    if (current == '*' && next == '/' && inBlockComment)
                    {
                        inBlockComment = false;
                        i++;
                        continue;
                    }
                }

                // Skip if in comment
                if (inLineComment || inBlockComment)
                {
                    continue;
                }

                // Handle strings and chars
                if (current == '"' && !inChar)
                {
                    inString = !inString;
                    continue;
                }

                if (current == '\'' && !inString)
                {
                    inChar = !inChar;
                    continue;
                }

                // Count the character if not in string/char/comment
                if (!inString && !inChar && current == ch)
                {
                    count++;
                }
            }

            return count;
        }

    }
}
