namespace Alpha.ViewLogs.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PagingLogs<LogType> where LogType : Log
    {
        public PagingLogs()
        {
            Logs = Enumerable.Empty<LogType>();
        }

        public IEnumerable<LogType> Logs { get; set; }

        public int TotalRecords { get; set; }

        /// <summary>
        /// Equals: used for compare two object (Unit test)
        /// </summary>
        public override bool Equals(object obj)
        {
            var compareObject = (PagingLogs<LogType>)obj;
            if ((Logs == null && compareObject.Logs != null) ||
                (Logs != null && compareObject.Logs == null) ||
                (Logs.Count() != compareObject.Logs.Count()))
            {
                return false;
            }

            var isEqual = true;
            foreach (var log in Logs)
            {
                var compareLog = compareObject.Logs.Where(l => l.LogId == log.LogId).First();
                if (compareLog == null || !compareLog.Equals(log))
                {
                    isEqual = false;
                    break;
                }
            }

            isEqual &= TotalRecords == compareObject.TotalRecords;

            return isEqual;
        }

        public override int GetHashCode()
        {
            return Logs.GetHashCode() ^ TotalRecords.GetHashCode();
        }

        /// <summary>
        /// ToString: use for displaying compare result two object (Unit test)
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine("Data:");
            foreach (var log in Logs)
            {
                result.AppendLine("Log: " + log);
            }

            result.AppendLine();
            result.AppendLine("TotalRecords: " + TotalRecords);

            result.AppendLine(base.ToString());
            return result.ToString();
        }
    }
}
