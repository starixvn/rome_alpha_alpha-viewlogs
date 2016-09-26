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
            double numberValue;

            if (double.TryParse(value, out numberValue))
            {
                return numberValue == 0 ? value : numberValue.ToString();
            }

            return value;            
        }
    }
}
