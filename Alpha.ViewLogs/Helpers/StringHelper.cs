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
            if (!IsValidNumberValue(value))
            {
                return value;
            }

            var removedCommaValue = value.Replace(",", string.Empty);
            double numberValue;
            double.TryParse(removedCommaValue, out numberValue);

            var result = numberValue.ToString();
            return result;
        }

        public static string FormatThousandSeparate(decimal value, int decDigits)
        {
            var decPart = string.Join(string.Empty, Enumerable.Repeat("#", decDigits));
            return value.ToString($"#,##0.{decPart}");
        }

        /// <summary>
        /// String is considered number when
        ///     -Not NullOrWhiteSpace
        ///     -Can be parsed by double.TryParse
        ///     -Not start with . (Eg: .03)
        ///     -Real part not start with 0 (except real value is Zero)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsValidNumberValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var removedCommaValue = value.Replace(",", string.Empty);
            double numberValue;

            if (!double.TryParse(removedCommaValue, out numberValue) ||
                removedCommaValue.StartsWith("."))
            {
                return false;
            }

            var numberParts = removedCommaValue.Split('.');
            var realPart = numberParts[0];

            return !value.StartsWith("0", StringComparison.OrdinalIgnoreCase) || string.Equals("0", realPart);
        }
    }
}
