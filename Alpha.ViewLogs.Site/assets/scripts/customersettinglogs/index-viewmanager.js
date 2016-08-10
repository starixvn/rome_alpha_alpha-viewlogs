/// <reference path="../core.ts" />
/// <reference path="index-manager.ts" />
'use strict';
var indexViewManager = {
    elelments: {
        ddlLogActionType: null,
        btnSubmit: null,
        ddlMonth: null,
        iconLoading: null,
        chkOldValues: null,
        txtUsername: null,
        logTableContent: null
    },
    showOldValuesCookieName: '',
    cacheElements: function () {
        indexViewManager.elelments.ddlLogActionType = $('#ddl-logactiontype');
        indexViewManager.elelments.btnSubmit = $("#btn-submit");
        indexViewManager.elelments.ddlMonth = $('#ddlMonth');
        indexViewManager.elelments.iconLoading = $('#icon-loading');
        indexViewManager.elelments.chkOldValues = $('#chk-oldvalues');
        indexViewManager.elelments.txtUsername = $('#txt-username');
        indexViewManager.elelments.logTableContent = $('#DataLog tbody');
    },
    initElements: function (viewModel) {
        indexViewManager.cacheElements();
        indexViewManager.elelments.ddlLogActionType.change(function () {
            indexManager.viewLogs();
        });
        indexViewManager.initMonthPicker(viewModel);
        indexViewManager.elelments.btnSubmit.click(function () {
            indexManager.viewLogs();
        });
        indexViewManager.elelments.chkOldValues.click(function () {
            var isShowOldValues = indexViewManager.elelments.chkOldValues.prop("checked") ? 1 : 0;
            $.cookie(viewModel.showOldValuesCookieName, isShowOldValues);
            indexManager.viewLogs();
        });
        indexViewManager.elelments.txtUsername.keypress(function (e) {
            if (!e) {
                e = window.event;
            }
            var key = (e.keyCode) ? e.keyCode : e.which;
            if (key == alphaViewLogs.keyCode.enter) {
                indexManager.viewLogs();
                return alphaViewLogs.cancelBubbleEvent(e);
            }
        });
        var queryUsernameURL = alphaViewLogs.urls.queryUsername + "?" +
            $.param({
                issub: 0,
                ismember: 0
            });
        initAutoComplete($('#txt-username')[0], queryUsernameURL);
    },
    initMonthPicker: function (viewModel) {
        indexViewManager.elelments.ddlMonth.monPickr({
            maxDate: viewModel.maxReportDate,
            minDate: viewModel.minReportDate,
            month: new Date(viewModel.dayInMonth),
            fromDay: viewModel.fromDate,
            toDay: viewModel.toDate,
            showDateRange: true,
            monthFormat: alphaViewLogs.format.monthFormat,
            fromDayId: 'ddl-fromday',
            toDayId: 'ddl-today',
            fromDayLabel: viewModel.labelFrom,
            toDayLabel: viewModel.labelTo,
            sortDesc: true,
            log: function (msg) {
                console.warn(msg);
            }
        });
    },
    showLoadingState: function () {
        indexViewManager.elelments.btnSubmit.attr('disabled', 'disabled');
        indexViewManager.elelments.iconLoading.removeClass('hide');
    },
    showCompleteLoadingState: function () {
        indexViewManager.elelments.iconLoading.addClass('hide');
        indexViewManager.elelments.btnSubmit.attr('disabled', null);
    },
    getParams: function () {
        return {
            Username: indexViewManager.elelments.txtUsername.val(),
            FromDate: indexViewManager.elelments.ddlMonth.monPickr("getFromDate", alphaViewLogs.format.shortDateWithSlash),
            ToDate: indexViewManager.elelments.ddlMonth.monPickr("getToDate", alphaViewLogs.format.shortDateWithSlash),
            ActionTypeId: indexViewManager.elelments.ddlLogActionType.val()
        };
    },
    renderLogs: function (result) {
        var logs = result.Logs;
        var logsCount = logs ? logs.length : 0;
        var logContent = '';
        if (logsCount) {
            for (var i = 0; i < logsCount; i++) {
                var log = logs[i];
                logContent += '<tr>\
                                <td align="center">' + log.LogId + '</td>\
                                <td align="center">' + log.LogDate + '</td>\
                                <td>' + log.Username + '</td>\
                                <td>' + log.ActionName + '</td>\
                                <td>' + log.TrackingValue + '</td>\
                              </tr>';
            }
        }
        else {
            logContent = '<tr><td colspan="5">' + viewModel.noInfo + '</td></tr>';
        }
        indexViewManager.elelments.logTableContent.empty();
        indexViewManager.elelments.logTableContent.append(logContent);
        indexViewManager.renderPager(result);
    },
    renderPager: function (result) {
        $('#pp').pagination({
            pageList: alphaViewLogs.pageList,
            total: result.TotalRecords,
            pageSize: result.PageSize,
            pageNumber: result.PageIndex
        });
    }
};
