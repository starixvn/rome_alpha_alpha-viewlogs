namespace Alpha.ViewLogs.Site.Handlers
{
    using System.Collections.Generic;
    using Alpha.ViewLogs.Extensions;
    using Fanex.Common;
    using MediatR;
    using Models;
    using Models.Enumerations;
    using Requests.Login;
    using Services;
    using ViewLogs.Helpers;
    using ViewModels;
    using ViewModels.Login;

    public class LoginHandler : BaseHandler, IRequestHandler<IndexQuery, IndexViewModel>
    {
        public LoginHandler(LogService logService, XsltLogFormatter xsltLogFormatter) :
            base(logService, xsltLogFormatter)
        {
        }

        public IndexViewModel Handle(IndexQuery message)
        {
            var logsParam = GetLogsParam(message);
            var pagingLogs = this.logService.GetLogs(logsParam);

            var viewModel = new IndexViewModel();
            AssignIndexViewModelValue(logsParam, pagingLogs, viewModel);
            TransformLogsToHtml(pagingLogs, message, viewModel);

            viewModel.IsAgent = viewModel.RoleId == UserLevel.Agent.Value;
            viewModel.PreviousUsername = message.PreviousUsername;
            viewModel.ActionTypeId = message.ActionTypeId;
            viewModel.Username = viewModel.RoleId == UserLevel.Agent.Value ? viewModel.PreviousUsername : viewModel.Username;

            return viewModel;
        }

        private void TransformLogsToHtml(
            PagingLogs<Log> pagingLogs,
            IndexQuery message,
            BaseIndexViewModel viewModel)
        {
            var htmlLogs = new List<LogViewModel>();

            foreach (LoginLog log in pagingLogs.Logs)
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
                    IP = this.xsltLogFormatter.FormatIP(log.IP),

                    Domain = log.Domain,
                    Browser = log.Browser,
                    Country = this.xsltLogFormatter.FormatCountryCode(log.IP, log.CountryCode)
                });
            }

            viewModel.Logs = htmlLogs;
        }
    }
}