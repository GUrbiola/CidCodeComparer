using ICSharpCode.TextEditor;
using System;
using System.Linq;
using System.Linq.Expressions;

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
                var caret = editor.ActiveTextAreaControl.Caret;

                // Ensure the line number is within valid bounds
                int maxLine = Math.Max(0, editor.Document.TotalNumberOfLines - 1);
                lineNumber = Math.Max(0, Math.Min(lineNumber, maxLine));

                //make sure you are scrolling from the top
                textArea.ScrollTo(1);

                // Calculate the line to center the target line in the view
                int visibleLines = textArea.TextView.VisibleLineCount;
                int targetLine = Math.Max(0, lineNumber + visibleLines);

                // Scroll to the calculated target line
                textArea.ScrollTo(targetLine);

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
                    textArea.ScrollTo(1);

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

    public static class Extensions
    {
        /// <summary>
        /// Private function that "does all the work" to create the IOrderedQueryable object
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="source">Source</param>
        /// <param name="propertyName">Name of the property/field to be used for a sort definition</param>
        /// <param name="descending">Boolean that defines the sorting direction, descending if true, other false ascending</param>
        /// <param name="anotherLevel">Boolean that defines if the sort is the first applied or is second or more sort defined</param>
        /// <returns>IOrderedQueryableObject with the sort</returns>
        private static IOrderedQueryable<T> OrderingHelper<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), string.Empty); // I don't care about some naming
            MemberExpression property = Expression.PropertyOrField(param, propertyName);
            LambdaExpression sort = Expression.Lambda(property, param);

            MethodCallExpression call = Expression.Call(
            typeof(Queryable),
            (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
            new[] { typeof(T), property.Type },
            source.Expression,
            Expression.Quote(sort));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }

        /// <summary>
        /// Defines an ascending sort for a IQueryable, by property name.
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="source">Source</param>
        /// <param name="propertyName">Name of the property/field to be used for a sort definition</param>
        /// <returns>IOrderedQueryableObject with the sort</returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, false, false);
        }

        /// <summary>
        /// Defines a descending sort for a IQueryable, by property name.
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="source">Source</param>
        /// <param name="propertyName">Name of the property/field to be used for a sort definition</param>
        /// <returns>IOrderedQueryableObject with the sort</returns>
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, true, false);
        }

        /// <summary>
        /// Defines a second sort(or more), ascending sort for a IQueryable, by property name.
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="source">Source</param>
        /// <param name="propertyName">Name of the property/field to be used for a sort definition</param>
        /// <returns>IOrderedQueryableObject with the sort</returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, false, true);
        }

        /// <summary>
        /// Defines a second sort(or more), descednding sort for a IQueryable, by property name.
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="source">Source</param>
        /// <param name="propertyName">Name of the property/field to be used for a sort definition</param>
        /// <returns>IOrderedQueryableObject with the sort</returns>
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, true, true);
        }

        /// <summary>
        /// Defines a sort for a IQueryable, by property name as a string and the direction is also a string.
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="source">Source</param>
        /// <param name="propertyName">Name of the property/field to be used for a sort definition</param>
        /// <param name="direction">Defines the direction of the sorting, if the value of this parameter is "desc"(case insensitive), defines a descending sort
        /// otherwise an ascending sort</param>
        /// <returns>IOrderedQueryableObject with the sort</returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, string direction)
        {
            return OrderingHelper(source, propertyName, direction.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? true : false, false);
        }

        /// <summary>
        /// Defines a secon(or more) sort for a IQueryable, by property name as a string and the direction is also a string.
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="source">Source</param>
        /// <param name="propertyName">Name of the property/field to be used for a sort definition</param>
        /// <param name="direction">Defines the direction of the sorting, if the value of this parameter is "desc"(case insensitive), defines a descending sort
        /// otherwise an ascending sort</param>
        /// <returns>IOrderedQueryableObject with the sort</returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName, string direction)
        {
            return OrderingHelper(source, propertyName, direction.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? true : false, true);
        }
    }
}
