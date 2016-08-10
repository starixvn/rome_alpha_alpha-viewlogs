namespace Alpha.ViewLogs.Site.Handlers
{
    using Helpers;
    using MediatR;
    using Models;
    using Params;
    using Requests.StatusLogs;
    using Services;
    using System.Collections.Generic;
    using ViewLogs.Helpers;
    using ViewModels;
    using ViewModels.StatusLogs;

    public class StatusLogsHandler : BaseViewLogsHandler,
        IRequestHandler<IndexQuery, IndexViewModel>,
        IRequestHandler<GetLogsQuery, GetLogsViewModel>
    {
        private readonly StatusLogsService statusLogsService;
        private readonly TransformTrackingValueToHtmlService transformTrackingValueToHtmlService;

        public StatusLogsHandler(
            StatusLogsService statusLogsService,
            TransformTrackingValueToHtmlService transformTrackingValueToHtmlService)
        {
            this.statusLogsService = statusLogsService;
            this.transformTrackingValueToHtmlService = transformTrackingValueToHtmlService;
        }

        public IndexViewModel Handle(IndexQuery message)
        {
            var logsParam = message.ConvertToGetLogsParam();
            var pagingLogs = statusLogsService.GetLogs(logsParam);

            var viewModel = new IndexViewModel(logsParam);

            viewModel.GetLogsViewModel = new GetLogsViewModel(logsParam, pagingLogs);
            TransformLogsToHtml(pagingLogs, message, viewModel.GetLogsViewModel);

            viewModel.Tabs = GetTabs(viewModel.IsSubAccount, viewModel.HasViewLogPermission);

            return viewModel;
        }

        public GetLogsViewModel Handle(GetLogsQuery message)
        {
            var logsParam = message.ConvertToGetLogsParam();
            var pagingLogs = statusLogsService.GetLogs(logsParam);

            var viewModel = new GetLogsViewModel(logsParam, pagingLogs);
            TransformLogsToHtml(pagingLogs, message, viewModel);

            return viewModel;
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
                if (!message.ActionIdVisibilities[log.ActionId])
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
                    LogDate = DateTimeHelper.ToLongDateWithSlash(log.LogDate),
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