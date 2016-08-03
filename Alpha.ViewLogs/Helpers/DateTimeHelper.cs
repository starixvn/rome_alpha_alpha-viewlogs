namespace Alpha.ViewLogs.Helpers
{
    using AppResources;
    using System;

    public class DateTimeHelper
    {
        public static string[] DaysInWeek()
        {
            return new string[] {
                Resources.Sun,
                Resources.Mon,
                Resources.Tue,
                Resources.Wed,
                Resources.Thurs,
                Resources.Fri,
                Resources.Sat
            };
        }

        public static DateTime GetBeginningOfDate(DateTime dateTime)
        {
            var beginningDateTime = dateTime.ToShortDateString() + " 00:00:00 AM";

            return Convert.ToDateTime(beginningDateTime);
        }

        public static DateTime GetEndingOfDate(DateTime dateTime)
        {
            var endingDateTime = dateTime.ToShortDateString() + " 23:59:59 PM";

            return Convert.ToDateTime(endingDateTime);
        }
    }
}
