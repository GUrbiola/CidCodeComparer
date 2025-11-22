using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CidCodeComparer.Models;
using ICSharpCode.TextEditor;

namespace CidCodeComparer.Controls
{
    public partial class DiffViewerControl : UserControl
    {
        private bool _isScrolling = false;
        private bool _isTrackBarScrolling = false;
        private List<LineDifference> _currentDifferences;
        private int _totalLines = 0;
        private int _currentDiffIndex = -1;

        // Diff algorithm options
        public bool IgnoreWhitespace { get; set; } = false;
        public bool IgnoreCase { get; set; } = false;
        public double SimilarityThreshold { get; set; } = 1.0; // 30% similarity to consider lines as "modified"

        public bool ShowJumpButtons
        {
            get
            {
                return navigationPanel.Visible;
            }
            set
            {
                navigationPanel.Visible = value;
            }
        }

        public bool ShowSummaryPanel
        {
            get
            {
                return locationPanel.Visible;
            }
            set
            {
                locationPanel.Visible = value;
                navigationTrackBar.Visible = value;
            }
        }

        public DiffViewerControl()
        {
            InitializeComponent();


            rightTextEditor.IsReadOnly = true;
            leftTextEditor.IsReadOnly = true;

            // Set up synchronized scrolling using the VScrollBar and HScrollBar ValueChanged events
            leftTextEditor.ActiveTextAreaControl.VScrollBar.ValueChanged += OnLeftScroll;
            rightTextEditor.ActiveTextAreaControl.VScrollBar.ValueChanged += OnRightScroll;
            leftTextEditor.ActiveTextAreaControl.HScrollBar.ValueChanged += OnLeftHScroll;
            rightTextEditor.ActiveTextAreaControl.HScrollBar.ValueChanged += OnRightHScroll;

            // Set up location panel events (read-only, no interaction)
            locationPanel.Paint += LocationPanel_Paint;

            // Set up navigation button events
            btnPrevDiff.Click += BtnPrevDiff_Click;
            btnNextDiff.Click += BtnNextDiff_Click;

            // Set up track bar events
            navigationTrackBar.Scroll += NavigationTrackBar_Scroll;

            // Initialize button states and trackbar
            UpdateNavigationButtons();
            UpdateTrackBar();
        }

        /// <summary>
        /// Loads two texts and automatically calculates and displays their differences
        /// </summary>
        public void LoadTexts(string text1, string text2)
        {
            LoadTexts(text1, text2, null);
        }

        /// <summary>
        /// Loads two texts and displays differences. If differences are not provided, they will be calculated automatically.
        /// </summary>
        public void LoadTexts(string text1, string text2, List<LineDifference> differences)
        {
            // Clear previous highlights
            ClearHighlights();

            // If differences not provided, calculate them
            if (differences == null)
            {
                var lines1 = (text1 ?? string.Empty).Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                var lines2 = (text2 ?? string.Empty).Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                differences = ComputeDifferences(lines1, lines2);
            }

            if (differences != null && differences.Count > 0)
            {
                // Create aligned versions of the text with placeholder lines
                var (alignedText1, alignedText2, alignedDifferences) = AlignTexts(text1, text2, differences);
                leftTextEditor.Text = alignedText1;
                rightTextEditor.Text = alignedText2;

                // Store differences and total lines for location panel
                _currentDifferences = alignedDifferences;
                _totalLines = alignedText1.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).Length;

                // Apply syntax highlighting colors to differences
                HighlightDifferences(alignedDifferences);
            }
            else
            {
                // No differences, just show the text as-is
                leftTextEditor.Text = text1 ?? string.Empty;
                rightTextEditor.Text = text2 ?? string.Empty;

                _currentDifferences = new List<LineDifference>();
                _totalLines = (text1 ?? string.Empty).Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).Length;
            }

            // Reset navigation
            _currentDiffIndex = -1;
            UpdateNavigationButtons();
            UpdateTrackBar();

            // Refresh location panel
            locationPanel.Invalidate();
        }

        private List<LineDifference> ComputeDifferences(string[] file1Lines, string[] file2Lines)
        {
            var differences = new List<LineDifference>();

            // Use Myers' diff algorithm for better results
            var editScript = MyersDiff(file1Lines, file2Lines);

            // Post-process to detect modified lines based on similarity
            var processedDifferences = DetectModifiedLines(editScript, file1Lines, file2Lines);

            return processedDifferences;
        }

        /// <summary>
        /// Myers' diff algorithm - produces optimal edit script
        /// </summary>
        private List<LineDifference> MyersDiff(string[] file1Lines, string[] file2Lines)
        {
            int n = file1Lines.Length;
            int m = file2Lines.Length;
            int max = n + m;

            var v = new Dictionary<int, int>();
            var trace = new List<Dictionary<int, int>>();

            v[1] = 0;

            for (int d = 0; d <= max; d++)
            {
                trace.Add(new Dictionary<int, int>(v));

                for (int k = -d; k <= d; k += 2)
                {
                    int x;

                    if (k == -d || (k != d && v[k - 1] < v[k + 1]))
                    {
                        x = v[k + 1];
                    }
                    else
                    {
                        x = v[k - 1] + 1;
                    }

                    int y = x - k;

                    while (x < n && y < m && LinesEqual(file1Lines[x], file2Lines[y]))
                    {
                        x++;
                        y++;
                    }

                    v[k] = x;

                    if (x >= n && y >= m)
                    {
                        return BuildDifferencesFromTrace(trace, file1Lines, file2Lines, d);
                    }
                }
            }

            // Shouldn't reach here, but return empty if we do
            return new List<LineDifference>();
        }

        private bool LinesEqual(string line1, string line2)
        {
            if (IgnoreWhitespace)
            {
                line1 = NormalizeWhitespace(line1);
                line2 = NormalizeWhitespace(line2);
            }

            if (IgnoreCase)
            {
                return string.Equals(line1, line2, StringComparison.OrdinalIgnoreCase);
            }

            return line1 == line2;
        }

        private string NormalizeWhitespace(string line)
        {
            return System.Text.RegularExpressions.Regex.Replace(line.Trim(), @"\s+", " ");
        }

        private List<LineDifference> BuildDifferencesFromTrace(List<Dictionary<int, int>> trace, string[] file1Lines, string[] file2Lines, int d)
        {
            var differences = new List<LineDifference>();
            int x = file1Lines.Length;
            int y = file2Lines.Length;

            for (int depth = d; depth >= 0; depth--)
            {
                var v = trace[depth];
                int k = x - y;

                int prevK;
                if (k == -depth || (k != depth && v[k - 1] < v[k + 1]))
                {
                    prevK = k + 1;
                }
                else
                {
                    prevK = k - 1;
                }

                int prevX = v.ContainsKey(prevK) ? v[prevK] : 0;
                int prevY = prevX - prevK;

                while (x > prevX && y > prevY)
                {
                    differences.Insert(0, new LineDifference
                    {
                        LineNumber = differences.Count,
                        File1Content = file1Lines[x - 1],
                        File2Content = file2Lines[y - 1],
                        Type = DifferenceType.None,
                        File1LineNumber = x,
                        File2LineNumber = y
                    });
                    x--;
                    y--;
                }

                if (depth > 0)
                {
                    if (x == prevX)
                    {
                        // Addition
                        differences.Insert(0, new LineDifference
                        {
                            LineNumber = differences.Count,
                            File1Content = string.Empty,
                            File2Content = file2Lines[y - 1],
                            Type = DifferenceType.Added,
                            File1LineNumber = -1,
                            File2LineNumber = y
                        });
                        y--;
                    }
                    else
                    {
                        // Deletion
                        differences.Insert(0, new LineDifference
                        {
                            LineNumber = differences.Count,
                            File1Content = file1Lines[x - 1],
                            File2Content = string.Empty,
                            Type = DifferenceType.Removed,
                            File1LineNumber = x,
                            File2LineNumber = -1
                        });
                        x--;
                    }
                }
            }

            // Renumber
            for (int i = 0; i < differences.Count; i++)
            {
                differences[i].LineNumber = i;
            }

            return differences;
        }

        /// <summary>
        /// Detect modified lines by checking similarity between removed and added lines
        /// </summary>
        private List<LineDifference> DetectModifiedLines(List<LineDifference> editScript, string[] file1Lines, string[] file2Lines)
        {
            var result = new List<LineDifference>();
            var i = 0;

            while (i < editScript.Count)
            {
                var current = editScript[i];

                // Look for removed lines followed by added lines (potential modifications)
                if (current.Type == DifferenceType.Removed)
                {
                    var removedLines = new List<LineDifference> { current };
                    int j = i + 1;

                    // Collect consecutive removed lines
                    while (j < editScript.Count && editScript[j].Type == DifferenceType.Removed)
                    {
                        removedLines.Add(editScript[j]);
                        j++;
                    }

                    // Collect consecutive added lines
                    var addedLines = new List<LineDifference>();
                    while (j < editScript.Count && editScript[j].Type == DifferenceType.Added)
                    {
                        addedLines.Add(editScript[j]);
                        j++;
                    }

                    // Try to pair removed and added lines based on similarity
                    if (addedLines.Count > 0)
                    {
                        var paired = PairSimilarLines(removedLines, addedLines);
                        result.AddRange(paired);
                        i = j;
                        continue;
                    }
                }

                result.Add(current);
                i++;
            }

            // Renumber
            for (int idx = 0; idx < result.Count; idx++)
            {
                result[idx].LineNumber = idx;
            }

            return result;
        }

        /// <summary>
        /// Pair removed and added lines based on similarity to create modified line entries
        /// </summary>
        private List<LineDifference> PairSimilarLines(List<LineDifference> removedLines, List<LineDifference> addedLines)
        {
            var result = new List<LineDifference>();
            var usedAdded = new HashSet<int>();
            var usedRemoved = new HashSet<int>();

            // If counts are similar, try to pair them in order first (preserves line proximity)
            if (removedLines.Count == addedLines.Count)
            {
                for (int i = 0; i < removedLines.Count; i++)
                {
                    double similarity = CalculateSimilarity(removedLines[i].File1Content, addedLines[i].File2Content);

                    if (similarity >= SimilarityThreshold)
                    {
                        result.Add(new LineDifference
                        {
                            LineNumber = result.Count,
                            File1Content = removedLines[i].File1Content,
                            File2Content = addedLines[i].File2Content,
                            Type = DifferenceType.Modified,
                            File1LineNumber = removedLines[i].File1LineNumber,
                            File2LineNumber = addedLines[i].File2LineNumber
                        });
                        usedRemoved.Add(i);
                        usedAdded.Add(i);
                    }
                }
            }

            // Second pass: find best matches for unpaired lines
            for (int i = 0; i < removedLines.Count; i++)
            {
                if (usedRemoved.Contains(i)) continue;

                double bestSimilarity = 0;
                int bestMatch = -1;

                for (int j = 0; j < addedLines.Count; j++)
                {
                    if (usedAdded.Contains(j)) continue;

                    double similarity = CalculateSimilarity(removedLines[i].File1Content, addedLines[j].File2Content);

                    // Prefer closer matches (proximity bonus)
                    double proximityBonus = 1.0 / (1.0 + Math.Abs(i - j) * 0.1);
                    double score = similarity * (0.8 + 0.2 * proximityBonus);

                    if (score > bestSimilarity && similarity >= SimilarityThreshold)
                    {
                        bestSimilarity = score;
                        bestMatch = j;
                    }
                }

                if (bestMatch >= 0)
                {
                    // Create a modified line entry
                    result.Add(new LineDifference
                    {
                        LineNumber = result.Count,
                        File1Content = removedLines[i].File1Content,
                        File2Content = addedLines[bestMatch].File2Content,
                        Type = DifferenceType.Modified,
                        File1LineNumber = removedLines[i].File1LineNumber,
                        File2LineNumber = addedLines[bestMatch].File2LineNumber
                    });

                    usedRemoved.Add(i);
                    usedAdded.Add(bestMatch);
                }
            }

            // Interleave unpaired removed and added lines to show them aligned
            var unpairedRemoved = new List<LineDifference>();
            var unpairedAdded = new List<LineDifference>();

            for (int i = 0; i < removedLines.Count; i++)
            {
                if (!usedRemoved.Contains(i))
                    unpairedRemoved.Add(removedLines[i]);
            }

            for (int j = 0; j < addedLines.Count; j++)
            {
                if (!usedAdded.Contains(j))
                    unpairedAdded.Add(addedLines[j]);
            }

            // Pair unpaired lines positionally for better visual alignment
            int maxUnpaired = Math.Max(unpairedRemoved.Count, unpairedAdded.Count);
            for (int i = 0; i < maxUnpaired; i++)
            {
                if (i < unpairedRemoved.Count && i < unpairedAdded.Count)
                {
                    // Both exist - show as modified even if similarity is low
                    result.Add(new LineDifference
                    {
                        LineNumber = result.Count,
                        File1Content = unpairedRemoved[i].File1Content,
                        File2Content = unpairedAdded[i].File2Content,
                        Type = DifferenceType.Modified,
                        File1LineNumber = unpairedRemoved[i].File1LineNumber,
                        File2LineNumber = unpairedAdded[i].File2LineNumber
                    });
                }
                else if (i < unpairedRemoved.Count)
                {
                    result.Add(unpairedRemoved[i]);
                }
                else if (i < unpairedAdded.Count)
                {
                    result.Add(unpairedAdded[i]);
                }
            }

            return result;
        }

        /// <summary>
        /// Calculate similarity between two strings using Levenshtein distance
        /// Returns a value between 0 (completely different) and 1 (identical)
        /// </summary>
        private double CalculateSimilarity(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2))
                return 1.0;

            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return 0.0;

            // Normalize if options are set
            string str1 = s1;
            string str2 = s2;

            if (IgnoreWhitespace)
            {
                str1 = NormalizeWhitespace(str1);
                str2 = NormalizeWhitespace(str2);
            }

            if (IgnoreCase)
            {
                str1 = str1.ToLowerInvariant();
                str2 = str2.ToLowerInvariant();
            }

            int distance = LevenshteinDistance(str1, str2);
            int maxLength = Math.Max(str1.Length, str2.Length);

            if (maxLength == 0)
                return 1.0;

            return 1.0 - ((double)distance / maxLength);
        }

        /// <summary>
        /// Compute Levenshtein distance between two strings
        /// </summary>
        private int LevenshteinDistance(string s1, string s2)
        {
            int n = s1.Length;
            int m = s2.Length;

            if (n == 0) return m;
            if (m == 0) return n;

            int[,] d = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
                d[i, 0] = i;

            for (int j = 0; j <= m; j++)
                d[0, j] = j;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost
                    );
                }
            }

            return d[n, m];
        }

        private (string, string, List<LineDifference>) AlignTexts(string text1, string text2, List<LineDifference> differences)
        {
            // The differences list already contains all lines in aligned order
            // We just need to build the text from it
            var alignedLines1 = new List<string>();
            var alignedLines2 = new List<string>();
            var alignedDifferences = new List<LineDifference>();

            int displayLineNumber = 1;

            foreach (var diff in differences)
            {
                // Create aligned difference with display line numbers (1-based)
                var alignedDiff = new LineDifference
                {
                    Type = diff.Type,
                    File1Content = diff.File1Content,
                    File2Content = diff.File2Content,
                    File1LineNumber = displayLineNumber,
                    File2LineNumber = displayLineNumber,
                    LineNumber = diff.LineNumber
                };

                // Add content to aligned lines based on difference type
                switch (diff.Type)
                {
                    case DifferenceType.None:
                        // Both files have this line - no difference
                        alignedLines1.Add(diff.File1Content);
                        alignedLines2.Add(diff.File2Content);
                        break;

                    case DifferenceType.Added:
                        // Line exists only in file2 - empty placeholder in file1
                        alignedLines1.Add("");
                        alignedLines2.Add(diff.File2Content);
                        alignedDifferences.Add(alignedDiff);
                        break;

                    case DifferenceType.Removed:
                        // Line exists only in file1 - empty placeholder in file2
                        alignedLines1.Add(diff.File1Content);
                        alignedLines2.Add("");
                        alignedDifferences.Add(alignedDiff);
                        break;

                    case DifferenceType.Modified:
                        // Lines are different in both files
                        alignedLines1.Add(diff.File1Content);
                        alignedLines2.Add(diff.File2Content);
                        alignedDifferences.Add(alignedDiff);
                        break;
                }

                displayLineNumber++;
            }

            return (string.Join(Environment.NewLine, alignedLines1),
                    string.Join(Environment.NewLine, alignedLines2),
                    alignedDifferences);
        }

        private void OnLeftScroll(object sender, EventArgs e)
        {
            if (!_isScrolling)
            {
                _isScrolling = true;
                SynchronizeScrollPosition(leftTextEditor, rightTextEditor);
                _isScrolling = false;
            }

            // Update trackbar to reflect current scroll position
            if (!_isTrackBarScrolling)
            {
                UpdateTrackBarFromScroll();
            }

            // Update location panel to show new viewport position
            locationPanel.Invalidate();
        }

        private void OnRightScroll(object sender, EventArgs e)
        {
            if (!_isScrolling)
            {
                _isScrolling = true;
                SynchronizeScrollPosition(rightTextEditor, leftTextEditor);
                _isScrolling = false;
            }

            // Update trackbar to reflect current scroll position
            if (!_isTrackBarScrolling)
            {
                UpdateTrackBarFromScroll();
            }

            // Update location panel to show new viewport position
            locationPanel.Invalidate();
        }

        private void OnLeftHScroll(object sender, EventArgs e)
        {
            if (!_isScrolling)
            {
                _isScrolling = true;
                SynchronizeHorizontalScrollPosition(leftTextEditor, rightTextEditor);
                _isScrolling = false;
            }
        }

        private void OnRightHScroll(object sender, EventArgs e)
        {
            if (!_isScrolling)
            {
                _isScrolling = true;
                SynchronizeHorizontalScrollPosition(rightTextEditor, leftTextEditor);
                _isScrolling = false;
            }
        }

        private void SynchronizeScrollPosition(TextEditorControl source, TextEditorControl target)
        {
            try
            {
                if (source.ActiveTextAreaControl.VScrollBar.Value <= target.ActiveTextAreaControl.VScrollBar.Maximum)
                {
                    target.ActiveTextAreaControl.VScrollBar.Value = source.ActiveTextAreaControl.VScrollBar.Value;
                    target.ActiveTextAreaControl.TextArea.Invalidate();
                }
            }
            catch
            {
                // Ignore synchronization errors
            }
        }

        private void SynchronizeHorizontalScrollPosition(TextEditorControl source, TextEditorControl target)
        {
            try
            {
                if (source.ActiveTextAreaControl.HScrollBar.Value <= target.ActiveTextAreaControl.HScrollBar.Maximum)
                {
                    target.ActiveTextAreaControl.HScrollBar.Value = source.ActiveTextAreaControl.HScrollBar.Value;
                    target.ActiveTextAreaControl.TextArea.Invalidate();
                }
            }
            catch
            {
                // Ignore synchronization errors
            }
        }

        private void HighlightDifferences(List<LineDifference> differences)
        {
            // Use WinMerge-like colors for better visibility
            Color addedColor = Color.FromArgb(180, 255, 180);      // Light green
            Color removedColor = Color.FromArgb(255, 160, 160);    // Light red/coral
            Color modifiedColor = Color.FromArgb(255, 255, 160);   // Light yellow
            Color modifiedCharColor = Color.FromArgb(255, 100, 100); // Red for changed characters (more visible)
            Color emptyLineColor = Color.FromArgb(240, 240, 240);  // Light gray for empty placeholder lines

            // First, mark all empty lines with gray background
            MarkEmptyLines(leftTextEditor, emptyLineColor);
            MarkEmptyLines(rightTextEditor, emptyLineColor);

            // Then highlight the differences
            foreach (var diff in differences)
            {
                Color highlightColor;

                switch (diff.Type)
                {
                    case DifferenceType.Added:
                        highlightColor = addedColor;
                        // Mark line in right editor (added)
                        if (diff.File2LineNumber > 0)
                        {
                            MarkLine(rightTextEditor, diff.File2LineNumber, highlightColor);
                        }
                        break;
                    case DifferenceType.Removed:
                        highlightColor = removedColor;
                        // Mark line in left editor (removed)
                        if (diff.File1LineNumber > 0)
                        {
                            MarkLine(leftTextEditor, diff.File1LineNumber, highlightColor);
                        }
                        break;
                    case DifferenceType.Modified:
                        highlightColor = modifiedColor;

                        // Check if either line is empty/whitespace to apply special highlighting
                        bool leftIsEmpty = string.IsNullOrWhiteSpace(diff.File1Content);
                        bool rightIsEmpty = string.IsNullOrWhiteSpace(diff.File2Content);

                        // Mark lines in both editors (modified) with base color
                        if (diff.File1LineNumber > 0)
                        {
                            // If left is empty but right has content, use a more visible color
                            Color leftColor = leftIsEmpty && !rightIsEmpty ? Color.FromArgb(255, 200, 200) : highlightColor;
                            MarkLine(leftTextEditor, diff.File1LineNumber, leftColor);
                        }
                        if (diff.File2LineNumber > 0)
                        {
                            // If right is empty but left has content, use a more visible color
                            Color rightColor = rightIsEmpty && !leftIsEmpty ? Color.FromArgb(200, 255, 200) : highlightColor;
                            MarkLine(rightTextEditor, diff.File2LineNumber, rightColor);
                        }

                        // Add character-level highlighting for the specific differences (only if both have content)
                        if (!leftIsEmpty && !rightIsEmpty)
                        {
                            HighlightCharacterDifferences(diff, modifiedCharColor);
                        }
                        break;
                    default:
                        continue;
                }
            }

            leftTextEditor.Refresh();
            rightTextEditor.Refresh();
        }

        private void MarkEmptyLines(TextEditorControl editor, Color color)
        {
            try
            {
                if (editor.Document == null) return;

                for (int i = 0; i < editor.Document.TotalNumberOfLines; i++)
                {
                    var lineSegment = editor.Document.GetLineSegment(i);
                    string lineText = editor.Document.GetText(lineSegment.Offset, lineSegment.Length);

                    // If line is empty or whitespace only, mark it with gray background
                    if (string.IsNullOrWhiteSpace(lineText))
                    {
                        var marker = new ICSharpCode.TextEditor.Document.TextMarker(
                            lineSegment.Offset,
                            Math.Max(1, lineSegment.Length),  // At least 1 to show background
                            ICSharpCode.TextEditor.Document.TextMarkerType.SolidBlock,
                            color
                        );
                        editor.Document.MarkerStrategy.AddMarker(marker);
                    }
                }
            }
            catch
            {
                // Ignore marking errors
            }
        }

        private void MarkLine(TextEditorControl editor, int lineNumber, Color color)
        {
            try
            {
                if (editor.Document != null && lineNumber > 0 && lineNumber <= editor.Document.TotalNumberOfLines)
                {
                    var lineSegment = editor.Document.GetLineSegment(lineNumber - 1);

                    // For empty lines, ensure we highlight at least a visible area
                    // by using the visible width of the editor
                    int markerLength = lineSegment.Length;
                    if (markerLength == 0 || string.IsNullOrWhiteSpace(editor.Document.GetText(lineSegment.Offset, lineSegment.Length)))
                    {
                        // For empty or whitespace-only lines, use a minimum visible width
                        // This ensures the highlighting is visible even on empty lines
                        markerLength = Math.Max(1, editor.ActiveTextAreaControl.TextArea.TextView.VisibleColumnCount);
                    }

                    var marker = new ICSharpCode.TextEditor.Document.TextMarker(
                        lineSegment.Offset,
                        markerLength,
                        ICSharpCode.TextEditor.Document.TextMarkerType.SolidBlock,
                        color
                    );
                    editor.Document.MarkerStrategy.AddMarker(marker);
                }
            }
            catch
            {
                // Ignore marking errors
            }
        }

        /// <summary>
        /// Highlights character-level differences within a modified line
        /// </summary>
        private void HighlightCharacterDifferences(LineDifference diff, Color charHighlightColor)
        {
            try
            {
                if (diff.File1LineNumber <= 0 || diff.File2LineNumber <= 0)
                    return;

                string line1 = diff.File1Content ?? "";
                string line2 = diff.File2Content ?? "";

                // Compute character-level differences
                var charDiffs = ComputeCharacterDifferences(line1, line2);

                // Apply highlights to left editor
                if (leftTextEditor.Document != null && diff.File1LineNumber <= leftTextEditor.Document.TotalNumberOfLines)
                {
                    var lineSegment = leftTextEditor.Document.GetLineSegment(diff.File1LineNumber - 1);
                    foreach (var range in charDiffs.LeftRanges)
                    {
                        if (range.Start >= 0 && range.Length > 0 && range.Start < line1.Length)
                        {
                            int length = Math.Min(range.Length, line1.Length - range.Start);
                            var marker = new ICSharpCode.TextEditor.Document.TextMarker(
                                lineSegment.Offset + range.Start,
                                length,
                                ICSharpCode.TextEditor.Document.TextMarkerType.SolidBlock,
                                charHighlightColor
                            );
                            leftTextEditor.Document.MarkerStrategy.AddMarker(marker);
                        }
                    }
                }

                // Apply highlights to right editor
                if (rightTextEditor.Document != null && diff.File2LineNumber <= rightTextEditor.Document.TotalNumberOfLines)
                {
                    var lineSegment = rightTextEditor.Document.GetLineSegment(diff.File2LineNumber - 1);
                    foreach (var range in charDiffs.RightRanges)
                    {
                        if (range.Start >= 0 && range.Length > 0 && range.Start < line2.Length)
                        {
                            int length = Math.Min(range.Length, line2.Length - range.Start);
                            var marker = new ICSharpCode.TextEditor.Document.TextMarker(
                                lineSegment.Offset + range.Start,
                                length,
                                ICSharpCode.TextEditor.Document.TextMarkerType.SolidBlock,
                                charHighlightColor
                            );
                            rightTextEditor.Document.MarkerStrategy.AddMarker(marker);
                        }
                    }
                }
            }
            catch
            {
                // Ignore character-level highlighting errors
            }
        }

        /// <summary>
        /// Computes character-level differences between two strings
        /// </summary>
        private (List<CharRange> LeftRanges, List<CharRange> RightRanges) ComputeCharacterDifferences(string s1, string s2)
        {
            var leftRanges = new List<CharRange>();
            var rightRanges = new List<CharRange>();

            if (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2))
                return (leftRanges, rightRanges);

            // Use a simple LCS-based approach for character-level diff
            var lcs = ComputeLCS(s1, s2);

            int i = 0, j = 0;
            int leftStart = -1, rightStart = -1;

            while (i < s1.Length || j < s2.Length)
            {
                // Check if current characters match (are in LCS)
                bool inLCS = i < s1.Length && j < s2.Length && lcs.Contains((i, j));

                if (inLCS)
                {
                    // Characters match - end any current difference ranges
                    if (leftStart >= 0)
                    {
                        leftRanges.Add(new CharRange { Start = leftStart, Length = i - leftStart });
                        leftStart = -1;
                    }
                    if (rightStart >= 0)
                    {
                        rightRanges.Add(new CharRange { Start = rightStart, Length = j - rightStart });
                        rightStart = -1;
                    }
                    i++;
                    j++;
                }
                else if (i < s1.Length && (j >= s2.Length || !lcs.Contains((i, j))))
                {
                    // Character in s1 but not in LCS (removed or changed)
                    if (leftStart < 0)
                        leftStart = i;
                    i++;
                }
                else if (j < s2.Length)
                {
                    // Character in s2 but not in LCS (added or changed)
                    if (rightStart < 0)
                        rightStart = j;
                    j++;
                }
            }

            // Close any remaining ranges
            if (leftStart >= 0)
                leftRanges.Add(new CharRange { Start = leftStart, Length = s1.Length - leftStart });
            if (rightStart >= 0)
                rightRanges.Add(new CharRange { Start = rightStart, Length = s2.Length - rightStart });

            return (leftRanges, rightRanges);
        }

        /// <summary>
        /// Computes longest common subsequence for character-level comparison
        /// </summary>
        private HashSet<(int, int)> ComputeLCS(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;
            int[,] dp = new int[m + 1, n + 1];

            // Build LCS table
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }

            // Backtrack to find LCS positions
            var result = new HashSet<(int, int)>();
            int x = m, y = n;

            while (x > 0 && y > 0)
            {
                if (s1[x - 1] == s2[y - 1])
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

        /// <summary>
        /// Helper class to represent a character range for highlighting
        /// </summary>
        private class CharRange
        {
            public int Start { get; set; }
            public int Length { get; set; }
        }

        public void ClearHighlights()
        {
            if (leftTextEditor.Document != null)
            {
                leftTextEditor.Document.MarkerStrategy.RemoveAll(m => true);
            }
            if (rightTextEditor.Document != null)
            {
                rightTextEditor.Document.MarkerStrategy.RemoveAll(m => true);
            }

            leftTextEditor.Refresh();
            rightTextEditor.Refresh();
        }

        public void SetSyntaxHighlighting(string fileExtension)
        {
            string syntax = GetSyntaxHighlighting(fileExtension);
            leftTextEditor.SetHighlighting(syntax);
            rightTextEditor.SetHighlighting(syntax);
        }

        private string GetSyntaxHighlighting(string fileExtension)
        {
            switch (fileExtension?.ToLower())
            {
                case ".cs":
                    return "C#";
                case ".js":
                    return "JavaScript";
                case ".html":
                case ".htm":
                    return "HTML";
                case ".xml":
                    return "XML";
                case ".json":
                    return "JavaScript";
                case ".css":
                    return "CSS";
                case ".sql":
                    return "SQL";
                default:
                    return "Default";
            }
        }

        #region Location Panel

        /// <summary>
        /// Paints the location panel with a visual overview of differences (split left/right)
        /// </summary>
        private void LocationPanel_Paint(object sender, PaintEventArgs e)
        {
            if (_currentDifferences == null || _totalLines == 0)
                return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int width = locationPanel.Width - 2;
            int height = locationPanel.Height - 2;
            int halfWidth = width / 2;

            // Draw background
            g.FillRectangle(Brushes.White, 0, 0, locationPanel.Width, locationPanel.Height);

            // Define colors matching the editor highlights
            Color addedColor = Color.FromArgb(180, 255, 180);
            Color removedColor = Color.FromArgb(255, 160, 160);
            Color modifiedColor = Color.FromArgb(255, 255, 160);

            // Draw each difference as a colored bar on the appropriate side
            foreach (var diff in _currentDifferences)
            {
                if (diff.Type == DifferenceType.None)
                    continue;

                // Calculate position based on line number
                float yPosition = ((float)diff.File1LineNumber / _totalLines) * height;
                float barHeight = Math.Max(2, height / (float)_totalLines * 3); // At least 2 pixels high

                Color color;
                float xPosition;
                float barWidth;

                switch (diff.Type)
                {
                    case DifferenceType.Added:
                        color = addedColor;
                        xPosition = halfWidth + 1; // Right side
                        barWidth = halfWidth - 1;
                        break;
                    case DifferenceType.Removed:
                        color = removedColor;
                        xPosition = 1; // Left side
                        barWidth = halfWidth - 1;
                        break;
                    case DifferenceType.Modified:
                        color = modifiedColor;
                        xPosition = 1; // Full width
                        barWidth = width - 1;
                        break;
                    default:
                        continue;
                }

                using (var brush = new SolidBrush(color))
                {
                    g.FillRectangle(brush, xPosition, yPosition, barWidth, barHeight);
                }

                // Draw border for better visibility
                using (var pen = new Pen(Color.FromArgb(Math.Max(0, color.R - 40), Math.Max(0, color.G - 40), Math.Max(0, color.B - 40))))
                {
                    g.DrawRectangle(pen, xPosition, yPosition, barWidth, barHeight);
                }
            }

            // Draw center divider line
            using (var pen = new Pen(Color.FromArgb(200, 200, 200), 1))
            {
                g.DrawLine(pen, halfWidth, 0, halfWidth, height);
            }

            // Draw viewport indicator (shows currently visible portion) - non-interactive
            DrawViewportIndicator(g, width, height);

            // Draw border
            g.DrawRectangle(Pens.LightGray, 0, 0, locationPanel.Width - 1, locationPanel.Height - 1);
        }

        /// <summary>
        /// Draws the viewport indicator showing the currently visible portion (read-only, non-interactive)
        /// </summary>
        private void DrawViewportIndicator(Graphics g, int width, int height)
        {
            try
            {
                if (leftTextEditor.Document == null || _totalLines == 0)
                    return;

                // Get current scroll position and visible line count
                int firstVisibleLine = leftTextEditor.ActiveTextAreaControl.TextArea.TextView.FirstVisibleLine;
                int visibleLineCount = leftTextEditor.ActiveTextAreaControl.TextArea.TextView.VisibleLineCount;
                int totalDocumentLines = leftTextEditor.Document.TotalNumberOfLines;

                if (totalDocumentLines == 0)
                    return;

                // Calculate viewport position and size
                float startRatio = (float)firstVisibleLine / _totalLines;
                float endRatio = (float)(firstVisibleLine + visibleLineCount) / _totalLines;

                // Clamp to valid range
                startRatio = Math.Max(0, Math.Min(1, startRatio));
                endRatio = Math.Max(0, Math.Min(1, endRatio));

                float viewportY = startRatio * height;
                float viewportHeight = (endRatio - startRatio) * height;

                // Ensure minimum height for visibility
                viewportHeight = Math.Max(15, viewportHeight);

                // Draw semi-transparent viewport indicator (non-interactive)
                using (var brush = new SolidBrush(Color.FromArgb(60, 100, 150, 255)))
                {
                    g.FillRectangle(brush, 1, viewportY, width, viewportHeight);
                }

                // Draw thin border for viewport indicator
                using (var pen = new Pen(Color.FromArgb(150, 50, 100, 200), 1))
                {
                    g.DrawRectangle(pen, 1, viewportY, width - 1, viewportHeight);
                }
            }
            catch
            {
                // Ignore errors in viewport drawing
            }
        }

        #endregion

        #region Navigation

        /// <summary>
        /// Navigates to the previous difference
        /// </summary>
        private void BtnPrevDiff_Click(object sender, EventArgs e)
        {
            if (_currentDifferences == null || _currentDifferences.Count == 0)
                return;

            // Get only actual differences (not "None" types)
            var actualDiffs = _currentDifferences.Where(d => d.Type != DifferenceType.None).ToList();
            if (actualDiffs.Count == 0)
                return;

            // Find previous difference
            if (_currentDiffIndex <= 0)
            {
                _currentDiffIndex = actualDiffs.Count - 1; // Wrap to last
            }
            else
            {
                _currentDiffIndex--;
            }

            // Navigate to it
            NavigateToDifference(actualDiffs[_currentDiffIndex]);
            UpdateNavigationButtons();
        }

        /// <summary>
        /// Navigates to the next difference
        /// </summary>
        private void BtnNextDiff_Click(object sender, EventArgs e)
        {
            if (_currentDifferences == null || _currentDifferences.Count == 0)
                return;

            // Get only actual differences (not "None" types)
            var actualDiffs = _currentDifferences.Where(d => d.Type != DifferenceType.None).ToList();
            if (actualDiffs.Count == 0)
                return;

            // Find next difference
            if (_currentDiffIndex < 0 || _currentDiffIndex >= actualDiffs.Count - 1)
            {
                _currentDiffIndex = 0; // Wrap to first
            }
            else
            {
                _currentDiffIndex++;
            }

            // Navigate to it
            NavigateToDifference(actualDiffs[_currentDiffIndex]);
            UpdateNavigationButtons();
        }

        /// <summary>
        /// Updates the state of navigation buttons
        /// </summary>
        private void UpdateNavigationButtons()
        {
            if (_currentDifferences == null)
            {
                btnPrevDiff.Enabled = false;
                btnNextDiff.Enabled = false;
                lblDiffCounter.Text = "0 / 0";
                return;
            }

            var actualDiffs = _currentDifferences.Where(d => d.Type != DifferenceType.None).ToList();
            int totalDiffs = actualDiffs.Count;

            btnPrevDiff.Enabled = totalDiffs > 0;
            btnNextDiff.Enabled = totalDiffs > 0;

            if (totalDiffs > 0 && _currentDiffIndex >= 0 && _currentDiffIndex < totalDiffs)
            {
                lblDiffCounter.Text = $"{_currentDiffIndex + 1} / {totalDiffs}";
            }
            else
            {
                lblDiffCounter.Text = $"0 / {totalDiffs}";
            }
        }

        /// <summary>
        /// Navigates to a specific difference
        /// </summary>
        private void NavigateToDifference(LineDifference diff)
        {
            NavigateToLine(diff.File1LineNumber);
        }

        /// <summary>
        /// Handles trackbar scroll event to navigate through the text
        /// </summary>
        private void NavigationTrackBar_Scroll(object sender, EventArgs e)
        {
            _isTrackBarScrolling = true;

            try
            {
                if (leftTextEditor.Document == null)
                    return;

                // Convert trackbar value to scroll position
                // Invert the trackbar: top (max) = start of file (0), bottom (0) = end of file (max)
                int maxScroll = leftTextEditor.ActiveTextAreaControl.VScrollBar.Maximum;
                int trackBarMax = navigationTrackBar.Maximum;

                if (trackBarMax > 0)
                {
                    // Invert: trackBarMax - current value
                    int invertedValue = trackBarMax - navigationTrackBar.Value;
                    int targetScroll = (int)((double)invertedValue / trackBarMax * maxScroll);
                    targetScroll = Math.Min(targetScroll, maxScroll);

                    leftTextEditor.ActiveTextAreaControl.VScrollBar.Value = targetScroll;
                    rightTextEditor.ActiveTextAreaControl.VScrollBar.Value = targetScroll;
                }
            }
            finally
            {
                _isTrackBarScrolling = false;
            }
        }

        /// <summary>
        /// Updates the trackbar based on current scroll position
        /// </summary>
        private void UpdateTrackBarFromScroll()
        {
            try
            {
                if (leftTextEditor.Document == null)
                    return;

                int currentScroll = leftTextEditor.ActiveTextAreaControl.VScrollBar.Value;
                int maxScroll = leftTextEditor.ActiveTextAreaControl.VScrollBar.Maximum;
                int trackBarMax = navigationTrackBar.Maximum;

                if (maxScroll > 0)
                {
                    // Calculate trackbar value from scroll position
                    int trackBarValue = (int)((double)currentScroll / maxScroll * trackBarMax);
                    trackBarValue = Math.Min(trackBarValue, trackBarMax);

                    // Invert: top (max) = start of file, bottom (0) = end of file
                    navigationTrackBar.Value = trackBarMax - trackBarValue;
                }
            }
            catch
            {
                // Ignore trackbar update errors
            }
        }

        /// <summary>
        /// Initializes or updates the trackbar range
        /// </summary>
        private void UpdateTrackBar()
        {
            try
            {
                if (leftTextEditor.Document == null || _totalLines == 0)
                {
                    navigationTrackBar.Enabled = false;
                    navigationTrackBar.Value = 0;
                    return;
                }

                navigationTrackBar.Enabled = true;
                navigationTrackBar.Maximum = Math.Max(100, _totalLines);
                navigationTrackBar.LargeChange = Math.Max(1, _totalLines / 10);
                navigationTrackBar.SmallChange = 1;

                // Set initial position
                UpdateTrackBarFromScroll();
            }
            catch
            {
                // Ignore trackbar initialization errors
            }
        }

        /// <summary>
        /// Navigates both text editors to the specified line
        /// </summary>
        private void NavigateToLine(int lineNumber)
        {
            try
            {
                if (lineNumber < 1)
                    return;

                // Calculate the desired scroll position
                // We want the line to be visible, preferably in the middle of the viewport
                int totalVisibleLines = leftTextEditor.ActiveTextAreaControl.TextArea.TextView.VisibleLineCount;
                int targetScrollLine = Math.Max(0, lineNumber - (totalVisibleLines / 2));

                // Set scroll position for both editors
                if (targetScrollLine <= leftTextEditor.ActiveTextAreaControl.VScrollBar.Maximum)
                {
                    leftTextEditor.ActiveTextAreaControl.VScrollBar.Value = targetScrollLine;
                    rightTextEditor.ActiveTextAreaControl.VScrollBar.Value = targetScrollLine;
                }

                // Set caret position to highlight the line
                if (lineNumber <= leftTextEditor.Document.TotalNumberOfLines)
                {
                    leftTextEditor.ActiveTextAreaControl.Caret.Line = lineNumber - 1;
                    leftTextEditor.ActiveTextAreaControl.Caret.Column = 0;
                }

                if (lineNumber <= rightTextEditor.Document.TotalNumberOfLines)
                {
                    rightTextEditor.ActiveTextAreaControl.Caret.Line = lineNumber - 1;
                    rightTextEditor.ActiveTextAreaControl.Caret.Column = 0;
                }

                // Refresh the editors
                leftTextEditor.Refresh();
                rightTextEditor.Refresh();
            }
            catch
            {
                // Ignore navigation errors
            }
        }

        #endregion
    }
}
