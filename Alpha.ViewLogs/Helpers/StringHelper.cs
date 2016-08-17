namespace Alpha.ViewLogs.Helpers
{
    public static class StringHelper
    {
        public static string RemoveTrailingZeros(string stringValue)
        {
            double numberValue;
            if (double.TryParse(stringValue, out numberValue))
            {
                return numberValue.ToString();
            }

            return stringValue;
        }
    }
}
