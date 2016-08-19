namespace Alpha.ViewLogs.Site.Handlers
{
    using System.Collections.Generic;
    using Fanex.Common;
    using Helpers;
    using MediatR;
    using Models;
    using Requests;
    using Requests.Credit;
    using Services;
    using Sunplus.Agent.Utilities;
    using ViewLogs.Helpers;
    using ViewModels;

    public class CreditHandler : BaseHandler, IRequestHandler<IndexQuery, LogViewModel>
    {
        private readonly LogFormatter logFormatter;
        private readonly LogService logService;

        public CreditHandler(
            LogService logService,
            LogFormatter logFormatter)
        {
            this.logService = logService;
            this.logFormatter = logFormatter;
        }

        public LogViewModel Handle(IndexQuery message)
        {
            var logsParam = CreateLogParam(message);
            var pagingLogs = this.logService.GetPagingLogs(logsParam);
            var viewModel = new LogViewModel(logsParam, pagingLogs.TotalRecords);

            TransformLogs(pagingLogs, message, viewModel);

            viewModel.ActionTypeId = message.ActionTypeId;

            return viewModel;
        }

        public void TransformLogs(
            PagingLogs<Log> pagingLogs,
            LogQuery message,
            LogViewModel viewModel)
        {
            var logItems = new List<LogItemViewModel>();

            foreach (var log in pagingLogs.Logs)
            {
                if (message.ActionIdVisibilities.ContainsKey(log.ActionId) 
                    && !message.ActionIdVisibilities[log.ActionId])
                {
                    viewModel.TotalRecords -= 1;
                    continue;
                }

                logItems.Add(new LogItemViewModel
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

                    TrackingValue = Formater.DecFormat(Converter.ToDecimal(log.TrackingValue), 2),
                    TrackingValueOld = Formater.DecFormat(Converter.ToDecimal(log.TrackingValueOld), 2),
                    Details = log.Details,
                    IP = this.logFormatter.FormatIP(log.IP)
                });
            }

            viewModel.LogItems = logItems;
        }
    }
}