namespace Alpha.ViewLogs.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PagingLogs<Log>
    {
        public PagingLogs()
        {
            Logs = Enumerable.Empty<Log>();
        }

        public IEnumerable<Log> Logs { get; set; }

        public int TotalRecords { get; set; }

        /// <summary>
        /// Equals: used for compare two object (Unit test)
        /// </summary>
        public override bool Equals(object obj)
        {
            var compareViewModel = (PagingLogs<Log>)obj;
            if ((Logs == null && compareViewModel.Logs != null) ||
                (Logs != null && compareViewModel.Logs == null) ||
                (Logs.Count() != compareViewModel.Logs.Count()))
            {
                return false;
            }

            var isEqual = !Logs.Except(compareViewModel.Logs).Any() &&
                TotalRecords == compareViewModel.TotalRecords;
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
