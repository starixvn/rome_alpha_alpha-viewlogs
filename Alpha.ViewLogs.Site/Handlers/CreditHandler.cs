namespace Alpha.ViewLogs.Site.Handlers
{
    using System.Collections.Generic;
    using Alpha.ViewLogs.Extensions;
    using Fanex.Common;
    using MediatR;
    using Models;
    using Requests;
    using Requests.Credit;
    using Services;
    using ViewLogs.Helpers;
    using ViewModels;
    using ViewModels.Credit;

    public class CreditHandler : BaseHandler, IRequestHandler<IndexQuery, IndexViewModel>
    {
        private readonly CreditService creditService;

        public CreditHandler(CreditService creditService, XsltLogFormatter xsltLogFormatter) :
            base(xsltLogFormatter)
        {
            this.creditService = creditService;
        }

        public IndexViewModel Handle(IndexQuery message)
        {
            var logsParam = GetLogsParam(message);
            var pagingLogs = creditService.GetLogs(logsParam);

            var viewModel = new IndexViewModel(logsParam);
            viewModel.GetLogsViewModel = new GetLogsViewModel(logsParam, pagingLogs);
            TransformLogsToHtml(pagingLogs, message, viewModel.GetLogsViewModel);

            viewModel.ActionTypeId = message.ActionTypeId;

            return viewModel;
        }

        public void TransformLogsToHtml(
            PagingLogs<Log> pagingLogs,
            BaseQuery message,
            GetLogsViewModel viewModel)
        {
            var htmlLogs = new List<LogViewModel>();

            foreach (var log in pagingLogs.Logs)
            {
                if (message.ActionIdVisibilities.ContainsKey(log.ActionId) && !message.ActionIdVisibilities[log.ActionId])
                {
                    viewModel.TotalRecords -= 1;
                    continue;
                }

                htmlLogs.Add(new LogViewModel
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

                    TrackingValue = Converter.ToDecimal(log.TrackingValue).Format(2),
                    TrackingValueOld = Converter.ToDecimal(log.TrackingValueOld).Format(2),
                    Details = log.Details,
                    IP = XsltLogFormatter.FormatIP(log.IP)
                });
            }

            viewModel.Logs = htmlLogs;
        }
    }
}