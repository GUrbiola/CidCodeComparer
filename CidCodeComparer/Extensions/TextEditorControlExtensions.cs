using ICSharpCode.TextEditor;
using System;

namespace CidCodeComparer.Extensions
{
    public static class TextEditorControlExtensions
    {
        /// <summary>
        /// Gets the first visible physical line in the text editor
        /// </summary>
        public static int GetFirstPhysicalLineVisible(this TextEditorControl editor)
        {
            try
            {
                var textArea = editor.ActiveTextAreaControl.TextArea;
                return textArea.TextView.FirstPhysicalLine;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Sets the first visible physical line in the text editor
        /// </summary>
        public static void SetFirstPhysicalLineVisible(this TextEditorControl editor, int lineNumber)
        {
            try
            {
                var textArea = editor.ActiveTextAreaControl.TextArea;

                // Ensure the line number is within valid bounds
                int maxLine = Math.Max(0, editor.Document.TotalNumberOfLines - 1);
                lineNumber = Math.Max(0, Math.Min(lineNumber, maxLine));

                // Use ScrollTo to scroll to the desired line
                textArea.ScrollTo(lineNumber);
            }
            catch
            {
                // Silently fail if scrolling is not possible
            }
        }

        /// <summary>
        /// Scrolls the editor to make the specified line visible and optionally centered
        /// </summary>
        public static void ScrollToLine(this TextEditorControl editor, int lineNumber, bool centerInView = true)
        {
            try
            {
                var textArea = editor.ActiveTextAreaControl.TextArea;
                var caret = editor.ActiveTextAreaControl.Caret;

                // Ensure the line number is within valid bounds
                int maxLine = Math.Max(0, editor.Document.TotalNumberOfLines - 1);
                lineNumber = Math.Max(0, Math.Min(lineNumber, maxLine));

                if (centerInView)
                {
                    // Calculate the line to center the target line in the view
                    int visibleLines = textArea.TextView.VisibleLineCount;
                    int targetLine = Math.Max(0, lineNumber - (visibleLines / 2));

                    // Scroll to the calculated target line
                    textArea.ScrollTo(targetLine);
                }
                else
                {
                    textArea.ScrollTo(lineNumber);
                }

                // Set caret position to the desired line
                caret.Line = lineNumber;
                caret.Column = 0;

                // Update the caret position visually
                caret.UpdateCaretPosition();

                // Force refresh
                editor.ActiveTextAreaControl.TextArea.Invalidate();
                editor.Refresh();
            }
            catch
            {
                // Silently fail
            }
        }
    }
}
