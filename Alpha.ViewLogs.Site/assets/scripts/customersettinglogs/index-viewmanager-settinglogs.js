/// <reference path="../core.ts" />
/// <reference path="index-manager.ts" />
'use strict';
var indexViewManager_SettingLogs = {
    elelments: {
        ddlLogActionType: null,
        btnSubmit: null,
        ddlMonth: null,
        iconLoading: null,
        chkOldValues: null,
        txtUsername: null,
        logTableContent: null,
        linkChangTabs: null,
        pagination: null
    },
    showOldValuesCookieName: '',
    cacheElements: function () {
        indexViewManager_SettingLogs.elelments.ddlLogActionType = $('#ddl-logactiontype');
        indexViewManager_SettingLogs.elelments.btnSubmit = $("#btn-submit");
        indexViewManager_SettingLogs.elelments.ddlMonth = $('#ddlMonth');
        indexViewManager_SettingLogs.elelments.iconLoading = $('#icon-loading');
        indexViewManager_SettingLogs.elelments.chkOldValues = $('#chk-oldvalues');
        indexViewManager_SettingLogs.elelments.txtUsername = $('#txt-username');
        indexViewManager_SettingLogs.elelments.logTableContent = $('#DataLog tbody');
        indexViewManager_SettingLogs.elelments.linkChangTabs = $('.lnk-changetab');
        indexViewManager_SettingLogs.elelments.pagination = $('#pp');
    },
    initElements: function (viewModel) {
        indexViewManager_SettingLogs.cacheElements();
        indexViewManager_SettingLogs.elelments.ddlLogActionType.change(function () {
            indexManager.viewLogs();
        });
        indexViewManager_SettingLogs.initMonthPicker(viewModel);
        indexViewManager_SettingLogs.elelments.btnSubmit.click(function () {
            indexManager.viewLogs();
        });
        indexViewManager_SettingLogs.elelments.chkOldValues.click(function () {
            var isShowOldValues = indexViewManager_SettingLogs.elelments.chkOldValues.prop("checked") ? 1 : 0;
            $.cookie(viewModel.showOldValuesCookieName, isShowOldValues);
            indexManager.viewLogs();
        });
        indexViewManager_SettingLogs.elelments.txtUsername.keypress(function (e) {
            if (!e) {
                e = window.event;
            }
            var key = (e.keyCode) ? e.keyCode : e.which;
            if (key == alphaViewLogs.keyCode.enter) {
                setVisible('hidden');
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
        indexViewManager_SettingLogs.elelments.linkChangTabs.click(function () {
            var url = $(this).data("tablink") + "?" + $.param(indexManager.viewLogsParams);
            window.location.href = url;
        });
        indexViewManager_SettingLogs.renderPager({
            totalRecords: viewModel.totalRecords,
            pageSize: viewModel.pageSize,
            pageNumber: viewModel.pageIndex
        });
    },
    initMonthPicker: function (viewModel) {
        indexViewManager_SettingLogs.elelments.ddlMonth.monPickr({
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
        indexViewManager_SettingLogs.elelments.btnSubmit.attr('disabled', 'disabled');
        indexViewManager_SettingLogs.elelments.iconLoading.removeClass('hide');
    },
    showCompleteLoadingState: function () {
        indexViewManager_SettingLogs.elelments.iconLoading.addClass('hide');
        indexViewManager_SettingLogs.elelments.btnSubmit.attr('disabled', null);
    },
    getParams: function () {
        return {
            Username: indexViewManager_SettingLogs.elelments.txtUsername.val(),
            FromDate: indexViewManager_SettingLogs.elelments.ddlMonth.monPickr("getFromDate", alphaViewLogs.format.shortDateWithSlash),
            ToDate: indexViewManager_SettingLogs.elelments.ddlMonth.monPickr("getToDate", alphaViewLogs.format.shortDateWithSlash),
            ActionTypeId: indexViewManager_SettingLogs.elelments.ddlLogActionType.val()
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
            logContent = '<tr><td colspan="5" align="center">' + viewModel.noInfo + '</td></tr>';
        }
        indexViewManager_SettingLogs.elelments.logTableContent.empty();
        indexViewManager_SettingLogs.elelments.logTableContent.append(logContent);
        indexViewManager_SettingLogs.renderPager({
            totalRecords: result.TotalRecords,
            pageSize: result.PageSize,
            pageNumber: result.PageIndex
        });
    },
    renderPager: function (options) {
        if (options.totalRecords) {
            indexViewManager_SettingLogs.elelments.pagination.pagination({
                pageList: alphaViewLogs.pageList,
                total: options.totalRecords,
                pageSize: options.pageSize,
                pageNumber: options.pageIndex
            });
            indexViewManager_SettingLogs.elelments.pagination.removeClass("hide");
        }
        else {
            indexViewManager_SettingLogs.elelments.pagination.addClass("hide");
        }
    }
};
