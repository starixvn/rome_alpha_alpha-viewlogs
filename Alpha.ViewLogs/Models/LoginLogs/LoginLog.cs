namespace Alpha.ViewLogs.Models.LoginLogs
{
    public class LoginLog : BaseLog
    {
        public string ServerId { get; set; }

        public string Browser { get; set; }

        public string Domain { get; set; }

        public string Platform { get; set; }

        public string CountryCode { get; set; }
    }
}
