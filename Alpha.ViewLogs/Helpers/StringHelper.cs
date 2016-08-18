namespace Alpha.ViewLogs.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Eg: 0.6000 => 0.6
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveTrailingZeros(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            double numberValue;

            if (double.TryParse(value, out numberValue))
            {
                return numberValue.ToString();
            }

            return value;
        }
    }
}
