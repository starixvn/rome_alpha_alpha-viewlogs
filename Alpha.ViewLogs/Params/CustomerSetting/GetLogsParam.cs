namespace Alpha.ViewLogs.Params.CustomerSetting
{
    using System.Collections.Generic;

    public class GetLogsParam
    {
        public bool IsDebug { get; set; }

        public int SubAccId { get; set; }

        public string ActionTypeId { get; set; }

        public byte RoleId { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public bool HasViewLogPermission { get; set; }

        public Dictionary<int, bool> ActionIdVisibilities { get; set; }
    }
}
