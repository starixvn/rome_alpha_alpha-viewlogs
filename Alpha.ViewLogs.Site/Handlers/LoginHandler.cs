namespace Alpha.ViewLogs.Site.Handlers
{
    using Alpha.ViewLogs.Extensions;
    using Fanex.Common;
    using MediatR;
    using Models;
    using Models.Login;
    using Requests.Login;
    using Services;
    using System.Collections.Generic;
    using ViewLogs.Helpers;
    using ViewModels.Login;

    public class LoginHandler : IRequestHandler<IndexQuery, IndexViewModel>
    {
        private readonly LoginService loginService;

        private readonly XsltLogFormatService xsltLogFormatService;

        public LoginHandler(
           LoginService loginService,
           XsltLogFormatService xsltLogFormatService)
        {
            this.loginService = loginService;
            this.xsltLogFormatService = xsltLogFormatService;
        }

        public IndexViewModel Handle(IndexQuery message)
        {
            var logsParam = message.ConvertToGetLogsParam();
            var pagingLogs = loginService.GetLogs(logsParam);

            var viewModel = new IndexViewModel(logsParam);
            viewModel.GetLogsViewModel = new GetLogsViewModel(logsParam, pagingLogs);
            viewModel.PreviousUsername = message.PreviousUsername;

            TransformLogsToHtml(pagingLogs, message, viewModel.GetLogsViewModel);

            viewModel.ActionTypeId = message.ActionTypeId;

            return viewModel;
        }

        private void TransformLogsToHtml(
            PagingLogs<LoginLog> pagingLogs,
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