namespace Alpha.ViewLogs.Site.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AppResources;
    using Fanex.Common;
    using Helpers;
    using MediatR;
    using Models;
    using Params;
    using Requests.CustomerSetting;
    using Services;
    using ViewLogs.Constants;
    using ViewLogs.Helpers;
    using ViewModels;
    using ViewModels.CustomerSetting;

    public class CustomerSettingHandler : BaseHandler, IRequestHandler<IndexQuery, IndexViewModel>
    {
        private readonly CustomerSettingService customerSettingService;
        private readonly TransformService transformService;

        public CustomerSettingHandler(
            CustomerSettingService customerSettingService,
            TransformService transformService,
            XsltLogFormatter xsltLogFormatter) :
            base(xsltLogFormatter)
        {
            this.customerSettingService = customerSettingService;
            this.transformService = transformService;
        }

        public IndexViewModel Handle(IndexQuery message)
        {
            var fromDate = string.IsNullOrEmpty(message.FromDate) ? DateTime.Now.AddDays(-1) : Converter.ToDateTime(message.FromDate);
            var toDate = string.IsNullOrEmpty(message.ToDate) ? DateTime.Now : Converter.ToDateTime(message.ToDate);
            var logsParam = new CustomerSettingParam
            {
                UplineId = message.UplineId,
                SubAccId = message.SubAccId,
                RoleId = message.RoleId,
                FromDate = fromDate,
                ToDate = toDate,
                Username = message.Username,
                IsInternal = message.IsInternal,

                PageSize = string.IsNullOrEmpty(message.PageSize) ? Pager.DefaultPageSize : Converter.ToInt(message.PageSize),
                PageIndex = string.IsNullOrEmpty(message.PageIndex) ? 0 : Converter.ToInt(message.PageIndex),

                Language = message.Language,
                HasViewLogPermission = message.HasViewLogPermission,
                ActionIdVisibilities = message.ActionIdVisibilities,

                MaxReportDate = message.MaxReportDate,
                MinReportDate = message.MinReportDate,
                IsShowOldValue = message.IsShowOldValue,
                ActionTypeId = string.IsNullOrEmpty(message.ActionTypeId) ? ActionType.All : Converter.ToInt(message.ActionTypeId)
            };

            var pagingLogs = customerSettingService.GetLogs(logsParam);

            var viewModel = new IndexViewModel();
            AssignIndexViewModelValue(logsParam, pagingLogs, viewModel);
            viewModel.LogActionTypes = GetLogActionTypes(message);
            viewModel.ActionTypeId = message.ActionTypeId;

            TransformLogsToHtml(pagingLogs, message, viewModel);

            return viewModel;
        }

        private List<LogActionTypeViewModel> GetLogActionTypes(IndexQuery message)
        {
            var logActionTypes = customerSettingService.GetLogActionTypes();

            var logActionTypeViewModels = logActionTypes.Select(logActionType => new LogActionTypeViewModel(logActionType, message.Language))
                .ToList();
            logActionTypeViewModels.Insert(0, new LogActionTypeViewModel(ActionType.All, Resources.All));

            return logActionTypeViewModels;
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
                    IP = XsltLogFormatter.FormatIP(log.IP)
                });
            }

            viewModel.Logs = htmlLogs;
        }
    }
}