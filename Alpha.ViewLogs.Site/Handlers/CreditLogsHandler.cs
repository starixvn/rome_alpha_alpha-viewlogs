namespace Alpha.ViewLogs.Site.Handlers
{
    using Alpha.ViewLogs.Extensions;
    using Fanex.Common;
    using MediatR;
    using Models;
    using Requests.CreditLogs;
    using Services;
    using System.Collections.Generic;
    using ViewLogs.Helpers;
    using ViewModels;
    using ViewModels.CreditLogs;

    public class CreditLogsHandler : BaseViewLogsHandler, IRequestHandler<IndexQuery, IndexViewModel>
    {
        private readonly CreditLogsService creditLogsService;

        private readonly XsltLogFormatService xsltLogFormatService;

        public CreditLogsHandler(
            CreditLogsService creditLogsService,
            XsltLogFormatService xsltLogFormatService)
        {
            this.creditLogsService = creditLogsService;
            this.xsltLogFormatService = xsltLogFormatService;
        }

        public IndexViewModel Handle(IndexQuery message)
        {
            var logsParam = message.ConvertToGetLogsParam();
            var pagingLogs = creditLogsService.GetLogs(logsParam);

            var viewModel = new IndexViewModel(logsParam);
            viewModel.GetLogsViewModel = new GetLogsViewModel(logsParam, pagingLogs);
            TransformLogsToHtml(pagingLogs, message, viewModel.GetLogsViewModel);

            viewModel.ActionTypeId = message.ActionTypeId;

            viewModel.Tabs = GetTabs(viewModel.IsSubAccount, viewModel.HasViewLogPermission);

            return viewModel;
        }

        private void TransformLogsToHtml(
            PagingLogs pagingLogs,
            IndexQuery message,
            GetLogsViewModel viewModel)
        {
            var htmlLogs = new List<BaseLogViewModel>();

            foreach (var log in pagingLogs.Logs)
            {
                if (message.ActionIdVisibilities.ContainsKey(log.ActionId) && !message.ActionIdVisibilities[log.ActionId])
                {
                    viewModel.TotalRecords -= 1;
                    continue;
                }

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

                    TrackingValue = Converter.ToDecimal(log.TrackingValue).Format(2),
                    TrackingValueOld = Converter.ToDecimal(log.TrackingValueOld).Format(2),
                    Details = log.Details,
                    IP = xsltLogFormatService.FormatIP(log.IP)
                });
            }

            viewModel.Logs = htmlLogs;
        }
    }
}