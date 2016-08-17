namespace Alpha.ViewLogs.Helpers
{
    public static class StringHelper
    {
        public static string RemoveTrailingZeros(string value)
        {
            double numberValue;
            if (double.TryParse(value, out numberValue))
            {
                return numberValue.ToString();
            }

            return value;
        }
    }
}
