namespace Alpha.ViewLogs.Site.Requests
{
    using Configurations;
    using MediatR;
    using Params.CustomerSetting;
    using Sunplus.Agent.Model.Entities.Permission;
    using System.Collections.Generic;
    using ViewModels.CustomerSetting;

    public class CustomerSettingQuery : IRequest<CustomerSettingViewModel>
    {
        public CustomerSettingQuery()
        {
            ActionIdVisibilities = new Dictionary<int, bool>();
            SetActionIdVisibilities();
        }

        public bool IsDebug { get; set; }

        public int SubAccId { get; set; }

        public string ActionTypeId { get; set; }

        public byte RoleId { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public bool HasViewLogPermission { get; set; }

        public string Language { get; set; }

        public Dictionary<int, bool> ActionIdVisibilities { get; set; }

        public GetLogsParam ConvertToGetLogsParam()
        {
            return new GetLogsParam
            {
                IsDebug = IsDebug,
                SubAccId = SubAccId,
                ActionTypeId = ActionTypeId,
                RoleId = RoleId,
                FromDate = FromDate,
                ToDate = ToDate,
                HasViewLogPermission = HasViewLogPermission,
                ActionIdVisibilities = ActionIdVisibilities
            };
        }

        private void SetActionIdVisibilities()
        {
            AppendActionIdVisibilities(AppSettings.ActionIds_Racing_151_31, UserPermission.IsShowRacing_151_31);
            AppendActionIdVisibilities(AppSettings.ActionIds_Racing_151_41, UserPermission.IsShowRacing_151_41);
            AppendActionIdVisibilities(AppSettings.ActionIds_Racing_151_51, UserPermission.IsShowRacing_151_51);
            AppendActionIdVisibilities(AppSettings.ActionIds_Racing_152_41, UserPermission.IsShowRacing_152_41);
            AppendActionIdVisibilities(AppSettings.ActionIds_Racing_152_51, UserPermission.IsShowRacing_152_51);
            AppendActionIdVisibilities(AppSettings.ActionIds_Racing_153_51, UserPermission.IsShowRacing_153_51);
        }

        private void AppendActionIdVisibilities(List<int> actionIds, bool visibility)
        {
            foreach (var actionId in actionIds)
            {
                if (!ActionIdVisibilities.ContainsKey(actionId))
                {
                    ActionIdVisibilities.Add(actionId, visibility);
                }
            }
        }
    }
}