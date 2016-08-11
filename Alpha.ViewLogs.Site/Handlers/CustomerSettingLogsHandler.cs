namespace Alpha.ViewLogs.Site.Handlers
{
    using AppResources;
    using Helpers;
    using MediatR;
    using Models;
    using Params;
    using Requests.CustomerSettingLogs;
    using Services;
    using System.Collections.Generic;
    using System.Linq;
    using ViewLogs.Constants;
    using ViewLogs.Helpers;
    using ViewModels;
    using ViewModels.CustomerSettingLogs;

    public class CustomerSettingLogsHandler : BaseViewLogsHandler,
        IRequestHandler<IndexQuery, IndexViewModel>,
        IRequestHandler<GetLogsQuery, GetLogsViewModel>
    {
        private readonly CustomerSettingLogsService customerSettingLogsService;
        private readonly TransformTrackingValueToHtmlService transformTrackingValueToHtmlService;

        public CustomerSettingLogsHandler(
            CustomerSettingLogsService customerSettingLogsService,
            TransformTrackingValueToHtmlService transformTrackingValueToHtmlService)
        {
            this.customerSettingLogsService = customerSettingLogsService;
            this.transformTrackingValueToHtmlService = transformTrackingValueToHtmlService;
        }

        public IndexViewModel Handle(IndexQuery message)
        {
            var logsParam = message.ConvertToGetLogsParam();
            var pagingLogs = customerSettingLogsService.GetLogs(logsParam);

            var viewModel = new IndexViewModel(logsParam);
            viewModel.LogActionTypes = GetLogActionTypes(message);

            viewModel.GetLogsViewModel = new GetLogsViewModel(logsParam, pagingLogs);
            TransformLogsToHtml(pagingLogs, message, viewModel.GetLogsViewModel);

            viewModel.Tabs = GetTabs(viewModel.IsSubAccount, viewModel.HasViewLogPermission);

            return viewModel;
        }

        public GetLogsViewModel Handle(GetLogsQuery message)
        {
            var logsParam = message.ConvertToGetLogsParam();
            var pagingLogs = customerSettingLogsService.GetLogs(logsParam);

            var viewModel = new GetLogsViewModel(logsParam, pagingLogs);
            TransformLogsToHtml(pagingLogs, message, viewModel);

            return viewModel;
        }

        private List<LogActionTypeViewModel> GetLogActionTypes(IndexQuery message)
        {
            var logActionTypes = customerSettingLogsService.GetLogActionTypes();

            var logActionTypeViewModels = logActionTypes.Select(logActionType => new LogActionTypeViewModel(logActionType, message.Language))
                .ToList();
            logActionTypeViewModels.Insert(0, new LogActionTypeViewModel(ActionType.All, Resources.All));

            return logActionTypeViewModels;
        }

        private void TransformLogsToHtml(
            PagingLogs pagingLogs,
            GetLogsQuery message,
            GetLogsViewModel viewModel)
        {
            var transformParam = new TransformTrackingValueToHtmlParam
            {
                IsDebug = message.IsDebug,
                IsShowOldValue = message.IsShowOldValue,
                TrackingValueTransformTemplatePath = message.TrackingValueTransformTemplatePath,
                SportBetTypeVisibilities = message.SportBetTypeVisibilities,
                IsShowMyanmarOdds = message.IsShowMyanmarOdds
            };

            transformTrackingValueToHtmlService.InitCompiledTransformCache(transformParam);

            var htmlLogs = new List<BaseLogViewModel>();

            foreach (var log in pagingLogs.Logs)
            {
                if (message.ActionIdVisibilities.ContainsKey(log.ActionId) && !message.ActionIdVisibilities[log.ActionId])
                {
                    viewModel.TotalRecords -= 1;
                    continue;
                }

                var trackingValueWithDiff = BaseLogViewModelHelper.GetTrackingValueWithDiff(log, message.IsShowOldValue);
                var htmlTrackingValue = transformTrackingValueToHtmlService.TransformTrackingValueToHtml(trackingValueWithDiff, log, transformParam);

                htmlLogs.Add(new BaseLogViewModel
                {
                    ActionId = log.ActionId,
                    ActionName = log.ActionName,
                    ActionTypeId = log.ActionTypeId,
                    CustId = log.CustId,
                    CustName = log.CustName,
                    LogDate = DateTimeHelper.ToHtmlLongDateWithSlash(log.LogDate),
                    LogId = log.LogId,
                    SPName = log.SPName,
                    UserId = log.UserId,
                    Username = log.Username,
                    TrackingValue = htmlTrackingValue,
                    IP = log.IP
                });
            }

            viewModel.Logs = htmlLogs;
        }
    }
}