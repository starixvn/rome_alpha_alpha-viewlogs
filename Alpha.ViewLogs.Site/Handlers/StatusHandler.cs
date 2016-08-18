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
        private readonly TransformService transformService;

        public StatusHandler(
            LogService logService,
            TransformService transformService,
            XsltLogFormatter xsltLogFormatter) :
            base(logService, xsltLogFormatter)
        {
            this.transformService = transformService;
        }

        public IndexViewModel Handle(IndexQuery message)
        {
            var logsParam = GetLogsParam(message);
            var pagingLogs = this.logService.GetLogs(logsParam);

            var viewModel = new IndexViewModel();
            AssignIndexViewModelValue(logsParam, pagingLogs, viewModel);
            viewModel.ActionTypeId = message.ActionTypeId;

            TransformLogsToHtml(pagingLogs, message, viewModel);

            return viewModel;
        }

        private void TransformLogsToHtml(
            PagingLogs<Log> pagingLogs,
            IndexQuery message,
            BaseIndexViewModel viewModel)
        {
            var transformParam = new TransformTrackingValueToHtmlParam
            {
                IsShowOldValue = message.IsShowOldValue,
                TrackingValueTransformTemplatePath = message.TrackingValueTransformTemplatePath,
                SportBetTypeVisibilities = message.SportBetTypeVisibilities,
                IsShowMyanmarOdds = message.IsShowMyanmarOdds
            };

            transformService.InitCompiledTransformCache(transformParam);

            var htmlLogs = new List<LogViewModel>();

            foreach (var log in pagingLogs.Logs)
            {
                if (message.ActionIdVisibilities.ContainsKey(log.ActionId) && !message.ActionIdVisibilities[log.ActionId])
                {
                    viewModel.TotalRecords -= 1;
                    continue;
                }

                var trackingValueWithDiff = TrackingValueHelper.GetTrackingValueWithDiff(log, message.IsShowOldValue);
                var htmlTrackingValue = transformService.TransformTrackingValueToHtml(trackingValueWithDiff, log, transformParam);

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
                    IP = this.xsltLogFormatter.FormatIP(log.IP)
                });
            }

            viewModel.Logs = htmlLogs;
        }
    }
}