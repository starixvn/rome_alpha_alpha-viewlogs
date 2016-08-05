namespace Alpha.ViewLogs.Site.Handlers
{
    using AppResources;
    using Constants;
    using Helpers;
    using MediatR;
    using Models;
    using Params;
    using Requests;
    using Services;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels;
    using ViewModels.CustomerSettingLogs;

    public class CustomerSettingLogsHandler : BaseViewLogsHandler, IRequestHandler<CustomerSettingLogsQuery, CustomerSettingLogsViewModel>
    {
        private readonly CustomerSettingLogsService customerSettingLogsService;
        private readonly TransformTrackingValueToHtmlService transformTrackingValueToHtmlService;

        public CustomerSettingLogsHandler(
            CustomerSettingLogsService customerSettingService,
            TransformTrackingValueToHtmlService transformTrackingValueToHtmlService)
        {
            this.customerSettingLogsService = customerSettingService;
            this.transformTrackingValueToHtmlService = transformTrackingValueToHtmlService;
        }

        public CustomerSettingLogsViewModel Handle(CustomerSettingLogsQuery customerSettingLogsQuery)
        {
            var logsParam = customerSettingLogsQuery.ConvertToGetLogsParam();
            var pagingLogs = customerSettingLogsService.GetLogs(logsParam);

            var viewModel = new CustomerSettingLogsViewModel(logsParam, pagingLogs);
            viewModel.LogActionTypes = GetLogActionTypes(customerSettingLogsQuery);
            viewModel.Logs = TransformLogsToHtml(pagingLogs, customerSettingLogsQuery);
            viewModel.Tabs = GetTabs(viewModel.IsSubAccount, viewModel.HasViewLogPermission);
            viewModel.SelectedTab = ViewLogType.CustomerSetting;

            return viewModel;
        }

        private List<LogActionTypeViewModel> GetLogActionTypes(CustomerSettingLogsQuery customerSettingQuery)
        {
            var logActionTypes = customerSettingLogsService.GetLogActionTypes();

            var logActionTypeViewModels = logActionTypes.Select(logActionType => new LogActionTypeViewModel(logActionType, customerSettingQuery.Language))
                .ToList();
            logActionTypeViewModels.Insert(0, new LogActionTypeViewModel(-1, Resources.All));

            return logActionTypeViewModels;
        }

        private List<BaseLogViewModel> TransformLogsToHtml(PagingLogs pagingLogs, CustomerSettingLogsQuery customerSettingLogsQuery)
        {
            var transformParam = new TransformTrackingValueToHtmlParam
            {
                IsDebug = customerSettingLogsQuery.IsDebug,
                IsShowOldValue = customerSettingLogsQuery.IsShowOldValue,
                TrackingValueTransformTemplatePath = customerSettingLogsQuery.TrackingValueTransformTemplatePath,
                SportBetTypeVisibilities = customerSettingLogsQuery.SportBetTypeVisibilities,
                IsShowMyanmarOdds = customerSettingLogsQuery.IsShowMyanmarOdds
            };

            transformTrackingValueToHtmlService.InitCompiledTransformCache(transformParam);

            var htmlLogs = new List<BaseLogViewModel>();

            foreach (var log in pagingLogs.Logs)
            {
                var trackingValueWithDiff = BaseLogViewModelHelper.GetTrackingValueWithDiff(log, customerSettingLogsQuery.IsShowOldValue);
                var htmlTrackingValue = transformTrackingValueToHtmlService.TransformTrackingValueToHtml(trackingValueWithDiff, log, transformParam);

                htmlLogs.Add(new BaseLogViewModel
                {
                    ActionId = log.ActionId,
                    ActionName = log.ActionName,
                    ActionTypeId = log.ActionTypeId,
                    CustId = log.CustId,
                    CustName = log.CustName,
                    LogDate = log.LogDate,
                    LogId = log.LogId,
                    SPName = log.SPName,
                    UserId = log.UserId,
                    Username = log.Username,
                    TrackingValue = htmlTrackingValue,
                    IP = log.IP
                });
            }

            return htmlLogs;
        }
    }
}