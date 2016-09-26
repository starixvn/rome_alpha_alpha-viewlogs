namespace Alpha.ViewLogs.Helpers
{
    using System;
    using System.Linq;

    public static class StringHelper
    {
        /// <summary>
        /// Eg: 0.6000 => 0.6
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveTrailingZeros(string value)
        {
            double numberValue;

            if (string.IsNullOrWhiteSpace(value)
                || value.StartsWith(".", StringComparison.OrdinalIgnoreCase)
                || !double.TryParse(value, out numberValue))
            {
                return value;
            }

            return numberValue.ToString();
        }

        public static string FormatThousandSeparate(decimal value, int decDigits)
        {
            var decPart = string.Join(string.Empty, Enumerable.Repeat("#", decDigits));
            return value.ToString($"#,##0.{decPart}");
        }
    }
}
