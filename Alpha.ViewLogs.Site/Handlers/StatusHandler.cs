namespace Alpha.ViewLogs.Site.Handlers
{
    using System.Collections.Generic;
    using Helpers;
    using MediatR;
    using Models;
    using Params;
    using Requests.Status;
    using Services;
    using ViewLogs.Helpers;
    using ViewModels;
    using ViewModels.Status;

    public class StatusHandler : BaseHandler, IRequestHandler<IndexQuery, IndexViewModel>
    {
        private readonly StatusService statusService;
        private readonly TransformTrackingValueToHtmlService transformTrackingValueToHtmlService;

        public StatusHandler(
            StatusService statusService,
            TransformTrackingValueToHtmlService transformTrackingValueToHtmlService,
            XsltLogFormatter xsltLogFormatter) :
            base(xsltLogFormatter)
        {
            this.statusService = statusService;
            this.transformTrackingValueToHtmlService = transformTrackingValueToHtmlService;
        }

        public IndexViewModel Handle(IndexQuery message)
        {
            var logsParam = GetLogsParam(message);
            var pagingLogs = statusService.GetLogs(logsParam);

            var viewModel = new IndexViewModel(logsParam);
            viewModel.ActionTypeId = message.ActionTypeId;

            viewModel.GetLogsViewModel = new GetLogsViewModel(logsParam, pagingLogs);
            TransformLogsToHtml(pagingLogs, message, viewModel.GetLogsViewModel);

            return viewModel;
        }

        private void TransformLogsToHtml(
            PagingLogs<Log> pagingLogs,
            IndexQuery message,
            GetLogsViewModel viewModel)
        {
            var transformParam = new TransformTrackingValueToHtmlParam
            {
                IsShowOldValue = message.IsShowOldValue,
                TrackingValueTransformTemplatePath = message.TrackingValueTransformTemplatePath,
                SportBetTypeVisibilities = message.SportBetTypeVisibilities,
                IsShowMyanmarOdds = message.IsShowMyanmarOdds
            };

            transformTrackingValueToHtmlService.InitCompiledTransformCache(transformParam);

            var htmlLogs = new List<LogViewModel>();

            foreach (var log in pagingLogs.Logs)
            {
                if (message.ActionIdVisibilities.ContainsKey(log.ActionId) && !message.ActionIdVisibilities[log.ActionId])
                {
                    viewModel.TotalRecords -= 1;
                    continue;
                }

                var trackingValueWithDiff = TrackingValueHelper.GetTrackingValueWithDiff(log, message.IsShowOldValue);
                var htmlTrackingValue = transformTrackingValueToHtmlService.TransformTrackingValueToHtml(trackingValueWithDiff, log, transformParam);

                htmlLogs.Add(new LogViewModel
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
                    IP = XsltLogFormatter.FormatIP(log.IP)
                });
            }

            viewModel.Logs = htmlLogs;
        }
    }
}