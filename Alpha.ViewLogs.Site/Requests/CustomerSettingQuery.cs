namespace Alpha.ViewLogs.Site.Requests
{
    using MediatR;
    using ViewModels.CustomerSetting;

    public class CustomerSettingQuery : IRequest<CustomerSettingViewModel>
    {
        public bool IsDebug { get; set; }

        public int SubAccId { get; set; }

        public string ActionTypeId { get; set; }

        public byte RoleId { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }
    }
}