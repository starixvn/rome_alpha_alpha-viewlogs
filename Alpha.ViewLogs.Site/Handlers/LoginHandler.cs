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
        private readonly LogFormatter logFormatter;
        private readonly LogService logService;

        public LoginHandler(
            LogService logService,
            LogFormatter logFormatter)
        {
            this.logService = logService;
            this.logFormatter = logFormatter;
        }

        public IndexViewModel Handle(IndexQuery message)
        {
            var logsParam = CreateLogParam(message);
            var pagingLogs = this.logService.GetLogs(logsParam);

            var viewModel = new IndexViewModel(logsParam, pagingLogs.TotalRecords);

            TransformLogs(pagingLogs, message, viewModel);

            viewModel.IsAgent = viewModel.RoleId == UserLevel.Agent.Value;
            viewModel.PreviousUsername = message.PreviousUsername;
            viewModel.ActionTypeId = message.ActionTypeId;
            viewModel.Username = viewModel.RoleId == UserLevel.Agent.Value ? viewModel.PreviousUsername : viewModel.Username;

            return viewModel;
        }

        private void TransformLogs(
            PagingLogs<Log> pagingLogs,
            IndexQuery message,
            LogViewModel viewModel)
        {
            var logItems = new List<LogItemViewModel>();

            foreach (LoginLog log in pagingLogs.Logs)
            {
                if (message.ActionIdVisibilities.ContainsKey(log.ActionId) && !message.ActionIdVisibilities[log.ActionId])
                {
                    viewModel.TotalRecords -= 1;
                    continue;
                }

                logItems.Add(new LoginLogItemViewModel
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
                    IP = this.logFormatter.FormatIP(log.IP),

                    Domain = log.Domain,
                    Browser = log.Browser,
                    Country = this.logFormatter.FormatCountryCode(log.IP, log.CountryCode)
                });
            }

            viewModel.LogItems = logItems;
        }
    }
}