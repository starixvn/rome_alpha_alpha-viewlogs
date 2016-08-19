namespace Alpha.ViewLogs.Site.Handlers
{
    using System.Collections.Generic;
    using System.Web;
    using Helpers;
    using MediatR;
    using Models;
    using Params;
    using Requests.Status;
    using Services;
    using ViewLogs.Helpers;
    using ViewModels;

    public class StatusHandler : BaseHandler, IRequestHandler<IndexQuery, LogViewModel>
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

        public LogViewModel Handle(IndexQuery message)
        {
            var logsParam = CreateLogParam(message);
            var pagingLogs = this.LogService.GetLogs(logsParam);

            var viewModel = new LogViewModel(logsParam, pagingLogs.TotalRecords);
            viewModel.ActionTypeId = message.ActionTypeId;

            TransformLogsToHtml(pagingLogs, message, viewModel);

            return viewModel;
        }

        private void TransformLogsToHtml(
            PagingLogs<Log> pagingLogs,
            IndexQuery message,
            LogViewModel viewModel)
        {
            var transformParam = new TransformParam
            {
                IsShowOldValue = message.IsShowOldValue,
                TrackingValueTransformTemplatePath = message.TrackingValueTransformTemplatePath,
                SportBetTypeVisibilities = message.SportBetTypeVisibilities,
                IsShowMyanmarOdds = message.IsShowMyanmarOdds
            };

            var htmlLogs = new List<LogItemViewModel>();

            foreach (var log in pagingLogs.Logs)
            {
                if (message.ActionIdVisibilities.ContainsKey(log.ActionId) 
                    && !message.ActionIdVisibilities[log.ActionId])
                {
                    viewModel.TotalRecords -= 1;
                    continue;
                }

                var trackingValueWithDiff = TrackingValueHelper.GetFormattedDiffTrackingValue(log, message.IsShowOldValue);
                var htmlTrackingValue = transformService.TransformTrackingValueToHtml(trackingValueWithDiff, log.ActionId, transformParam);

                htmlLogs.Add(
                    new LogItemViewModel
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
                        TrackingValue = HttpUtility.HtmlDecode(htmlTrackingValue),
                        IP = this.XsltLogFormatter.FormatIP(log.IP)
                    });
            }

            viewModel.LogItems = htmlLogs;
        }
    }
}