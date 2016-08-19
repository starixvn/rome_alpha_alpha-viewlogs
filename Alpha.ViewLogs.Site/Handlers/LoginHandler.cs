namespace Alpha.ViewLogs.Site.Handlers
{
    using System.Collections.Generic;
    using Fanex.Common;
    using MediatR;
    using Models;
    using Models.Enumerations;
    using Requests.Login;
    using Services;
    using Sunplus.Agent.Utilities;
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
            var logsParam = CreateLogParam(message);
            var pagingLogs = this.LogService.GetLogs(logsParam);

            var viewModel = new IndexViewModel(logsParam, pagingLogs.TotalRecords);
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
            LogViewModel viewModel)
        {
            var htmlLogs = new List<LogItemViewModel>();

            foreach (LoginLog log in pagingLogs.Logs)
            {
                if (message.ActionIdVisibilities.ContainsKey(log.ActionId) && !message.ActionIdVisibilities[log.ActionId])
                {
                    viewModel.TotalRecords -= 1;
                    continue;
                }

                htmlLogs.Add(new LoginLogItemViewModel
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

                    TrackingValue = Formater.DecFormat(Converter.ToDecimal(log.TrackingValue), 2),
                    TrackingValueOld = Formater.DecFormat(Converter.ToDecimal(log.TrackingValueOld), 2),
                    Details = log.Details,
                    IP = this.XsltLogFormatter.FormatIP(log.IP),

                    Domain = log.Domain,
                    Browser = log.Browser,
                    Country = this.XsltLogFormatter.FormatCountryCode(log.IP, log.CountryCode)
                });
            }

            viewModel.LogItems = htmlLogs;
        }
    }
}