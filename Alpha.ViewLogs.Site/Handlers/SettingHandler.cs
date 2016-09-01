namespace Alpha.ViewLogs.Site.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AppResources;
    using Constants;
    using Fanex.Common;
    using Helpers;
    using MediatR;
    using Models;
    using Params;
    using Requests.Setting;
    using Services;
    using ViewLogs.Constants;
    using ViewLogs.Helpers;
    using ViewModels;
    using ViewModels.Setting;

    public class SettingHandler : BaseHandler, IRequestHandler<IndexQuery, IndexViewModel>
    {
        private readonly LogFormatter logFormatter;
        private readonly TrackingValueFormatter trackingValueFormatter;
        private readonly LogService logService;
        private readonly TransformService transformService;

        public SettingHandler(
            LogService logService,
            TransformService transformService,
            LogFormatter logFormatter,
            TrackingValueFormatter trackingValueFormatter)
        {
            this.logService = logService;
            this.logFormatter = logFormatter;
            this.trackingValueFormatter = trackingValueFormatter;
            this.transformService = transformService;
        }

        public IndexViewModel Handle(IndexQuery message)
        {
            var fromDate = GetFromDate(message);
            var toDate = GetToDate(message);
            var logsParam = new SettingParam
            {
                UplineId = message.UplineId,
                SubAccId = message.SubAccId,
                RoleId = message.RoleId,
                FromDate = fromDate,
                ToDate = toDate,
                Username = message.Username,
                IsInternal = message.IsInternal,

                PageSize = string.IsNullOrWhiteSpace(message.PageSize) ? DefaultVariables.PageSize : Converter.ToInt(message.PageSize),
                PageIndex = string.IsNullOrWhiteSpace(message.PageIndex) ? 0 : Converter.ToInt(message.PageIndex),

                Language = message.Language,
                HasViewLogPermission = message.HasViewLogPermission,
                ActionIdVisibilities = message.ActionIdVisibilities,

                MaxReportDate = message.MaxReportDate,
                MinReportDate = message.MinReportDate,
                IsShowOldValue = message.IsShowOldValue,
                ActionTypeId = string.IsNullOrWhiteSpace(message.ActionTypeId) ? ActionType.All : Converter.ToInt(message.ActionTypeId)
            };

            var pagingLogs = this.logService.GetPagingLogs(logsParam);

            var viewModel = new IndexViewModel(logsParam, pagingLogs.TotalRecords);
            viewModel.LogActionTypes = BuildLogActionTypeViewModels(message);
            viewModel.ActionTypeId = message.ActionTypeId;

            TransformLogs(pagingLogs, message, viewModel);

            return viewModel;
        }

        private IEnumerable<LogActionTypeViewModel> BuildLogActionTypeViewModels(IndexQuery message)
        {
            var logActionTypes = this.logService.GetLogActionTypes();

            var logActionTypeViewModels = logActionTypes
                .Select(logActionType => new LogActionTypeViewModel(logActionType, message.Language))
                .ToList();

            logActionTypeViewModels.Insert(0, new LogActionTypeViewModel(ActionType.All, Resources.All));

            return logActionTypeViewModels;
        }

        private void TransformLogs(
            PagingLogs<Log> pagingLogs,
            IndexQuery message,
            LogViewModel viewModel)
        {
            var transformParam = new TransformParam
            {
                IsShowOldValue = message.IsShowOldValue,
                TrackingValueTransformTemplatePath = message.TrackingValueTransformTemplatePath,
                CustStatuses = message.SportBetTypeVisibilities,
                IsShowMyanmarOdds = message.IsShowMyanmarOdds
            };

            var logItems = new List<LogItemViewModel>();

            foreach (var log in pagingLogs.Logs)
            {
                if (message.ActionIdVisibilities.ContainsKey(log.ActionId)
                    && !message.ActionIdVisibilities[log.ActionId])
                {
                    viewModel.TotalRecords -= 1;
                    continue;
                }

                var formattedTrackingValue = trackingValueFormatter.GetFormattedDiffTrackingValue(log, message.IsShowOldValue);
                var htmlTrackingValue = transformService.TransformTrackingValue(formattedTrackingValue, log.ActionId, transformParam);

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
                    TrackingValue = HttpUtility.HtmlDecode(htmlTrackingValue),
                    IP = this.logFormatter.FormatIP(log.IP)
                });
            }

            viewModel.LogItems = logItems;
        }
    }
}