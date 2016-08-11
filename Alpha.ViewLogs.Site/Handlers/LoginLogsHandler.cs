namespace Alpha.ViewLogs.Site.Handlers
{
    using Alpha.ViewLogs.Extensions;
    using Fanex.Common;
    using MediatR;
    using Models.LoginLogs;
    using Requests.LoginLogs;
    using Services;
    using System.Collections.Generic;
    using ViewLogs.Helpers;
    using ViewModels.LoginLogs;

    public class LoginLogsHandler : BaseViewLogsHandler, IRequestHandler<IndexQuery, IndexViewModel>
    {
        private readonly LoginLogsService loginLogsService;

        private readonly XsltLogFormatService xsltLogFormatService;

        public LoginLogsHandler(
           LoginLogsService loginLogsService,
           XsltLogFormatService xsltLogFormatService)
        {
            this.loginLogsService = loginLogsService;
            this.xsltLogFormatService = xsltLogFormatService;
        }

        public IndexViewModel Handle(IndexQuery message)
        {
            var logsParam = message.ConvertToGetLogsParam();
            var pagingLogs = loginLogsService.GetLogs(logsParam);

            var viewModel = new IndexViewModel(logsParam);
            viewModel.GetLogsViewModel = new GetLogsViewModel(logsParam, pagingLogs);
            TransformLogsToHtml(pagingLogs, message, viewModel.GetLogsViewModel);

            viewModel.ActionTypeId = message.ActionTypeId;

            viewModel.Tabs = GetTabs(viewModel.IsSubAccount, viewModel.HasViewLogPermission);

            return viewModel;
        }

        private void TransformLogsToHtml(
            PagingLoginLogs pagingLogs,
            IndexQuery message,
            GetLogsViewModel viewModel)
        {
            var htmlLogs = new List<LoginLogViewModel>();

            foreach (var log in pagingLogs.Logs)
            {
                if (message.ActionIdVisibilities.ContainsKey(log.ActionId) && !message.ActionIdVisibilities[log.ActionId])
                {
                    viewModel.TotalRecords -= 1;
                    continue;
                }

                htmlLogs.Add(new LoginLogViewModel
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

                    TrackingValue = Converter.ToDecimal(log.TrackingValue).Format(2),
                    TrackingValueOld = Converter.ToDecimal(log.TrackingValueOld).Format(2),
                    Details = log.Details,
                    IP = xsltLogFormatService.FormatIP(log.IP),

                    Domain = log.Domain,
                    Browser = log.Browser,
                    Country = xsltLogFormatService.FormatCountryCode(log.IP, log.CountryCode)
                });
            }

            viewModel.Logs = htmlLogs;
        }
    }
}