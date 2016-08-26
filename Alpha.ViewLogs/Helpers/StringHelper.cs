namespace Alpha.ViewLogs.Helpers
{
    using System;

    public static class StringHelper
    {
        /// <summary>
        /// Eg: 0.6000 => 0.6
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveTrailingZeros(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            var removedCommaValue = value.Replace(",", string.Empty);
            double numberValue;

            if (!double.TryParse(removedCommaValue, out numberValue))
            {
                return value;
            }

            var result = numberValue.ToString();

            var numberParts = removedCommaValue.Split('.');
            var realPart = numberParts[0];

            // Is number: 
            //    has real part (=> not accept: .xx) and
            //    (not start with 0/real part is "0")
            var isNumber = !string.IsNullOrWhiteSpace(realPart) &&
                (!value.StartsWith("0", StringComparison.OrdinalIgnoreCase) || string.Equals("0", realPart));
            if (isNumber)
            {
                return result;
            }

            return value;
        }
    }
}
