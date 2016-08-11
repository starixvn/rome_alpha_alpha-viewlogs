//# sourceMappingURL=core.js.map
/// <reference path="../core.ts" />
/// <reference path="index-manager.ts" />

'use strict';
/*global indexManager, alphaViewLogs*/
declare var $: any;
declare var initAutoComplete: any;
declare var setVisible: any;

var indexViewManager_StatusLogs = {
    elelments: {
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
    pageNumber: 0,
    pageSize: null,
    cacheElements: function () {
        indexViewManager_StatusLogs.elelments.btnSubmit = $("#btn-submit");
        indexViewManager_StatusLogs.elelments.ddlMonth = $('#ddlMonth');
        indexViewManager_StatusLogs.elelments.iconLoading = $('#icon-loading');
        indexViewManager_StatusLogs.elelments.chkOldValues = $('#chk-oldvalues');
        indexViewManager_StatusLogs.elelments.txtUsername = $('#txt-username');
        indexViewManager_StatusLogs.elelments.logTableContent = $('#DataLog tbody');
        indexViewManager_StatusLogs.elelments.linkChangTabs = $('.lnk-changetab');
        indexViewManager_StatusLogs.elelments.pagination = $('#pagination');
    },
    initElements: function (viewModel) {
        indexViewManager_StatusLogs.cacheElements();

        indexViewManager_StatusLogs.initMonthPicker(viewModel);

        indexViewManager_StatusLogs.elelments.btnSubmit.click(function () {
            indexManager.viewLogs();
        });

        indexViewManager_StatusLogs.elelments.chkOldValues.click(function () {
            var isShowOldValues = indexViewManager_StatusLogs.elelments.chkOldValues.prop("checked") ? 1 : 0;
            $.cookie(viewModel.showOldValuesCookieName, isShowOldValues, { path: '/' });
            indexManager.viewLogs();
        });

        indexViewManager_StatusLogs.elelments.txtUsername.keypress(function (e) {
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

        indexViewManager_StatusLogs.elelments.linkChangTabs.click(function () {
            var url = $(this).data("tablink") + "?" + $.param(indexManager.viewLogsParams);
            window.location.href = url;
        });

        indexViewManager_StatusLogs.renderPager({
            totalRecords: viewModel.totalRecords,
            pageSize: viewModel.pageSize,
            pageIndex: viewModel.pageIndex + 1
        });

        indexViewManager_StatusLogs.pageNumber = viewModel.pageIndex;
        indexViewManager_StatusLogs.pageSize = viewModel.pageSize;
    },
    initMonthPicker: function (viewModel) {
        indexViewManager_StatusLogs.elelments.ddlMonth.monPickr({
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
        indexViewManager_StatusLogs.elelments.btnSubmit.attr('disabled', 'disabled');
        indexViewManager_StatusLogs.elelments.iconLoading.removeClass('hide');
    },
    showCompleteLoadingState: function () {
        indexViewManager_StatusLogs.elelments.iconLoading.addClass('hide');
        indexViewManager_StatusLogs.elelments.btnSubmit.attr('disabled', null);
    },
    getParams: function () {
        return {
            ActionTypeId: viewModel.actionTypeId,
            PageIndex: indexViewManager_StatusLogs.pageNumber,
            PageSize: indexViewManager_StatusLogs.pageSize,
            Username: indexViewManager_StatusLogs.elelments.txtUsername.val(),
            FromDate: indexViewManager_StatusLogs.elelments.ddlMonth.monPickr("getFromDate", alphaViewLogs.format.shortDateWithSlash),
            ToDate: indexViewManager_StatusLogs.elelments.ddlMonth.monPickr("getToDate", alphaViewLogs.format.shortDateWithSlash)
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
                                <td align="center">'+ log.LogId + '</td>\
                                <td align="center">'+ log.LogDate + '</td>\
                                <td>'+ log.Username + '</td>\
                                <td>'+ log.ActionName + '</td>\
                                <td>'+ log.TrackingValue + '</td>\
                              </tr>';
            }
        } else {
            logContent = '<tr><td colspan="5" align="center">' + viewModel.noInfo + '</td></tr>';
        }

        indexViewManager_StatusLogs.elelments.logTableContent.empty();
        indexViewManager_StatusLogs.elelments.logTableContent.append(logContent);

        indexViewManager_StatusLogs.renderPager({
            totalRecords: result.TotalRecords,
            pageSize: result.PageSize,
            pageNumber: result.PageIndex
        });
    },
    renderPager: function (options) {
        if (options.pageSize <= options.totalRecords) {
            indexViewManager_StatusLogs.elelments.pagination.pagination({
                pageList: alphaViewLogs.pageList,
                total: options.totalRecords,
                pageSize: options.pageSize,
                pageNumber: options.pageIndex,

                showRefresh: false,
                onSelectPage: function (pageNumber, pageSize) {
                    indexViewManager_StatusLogs.pageNumber = pageNumber - 1;
                    indexViewManager_StatusLogs.pageSize = pageSize;

                    indexManager.viewLogs();
                },
                beforePageText: viewModel.Page,
                afterPageText: viewModel.of + ' {pages}',
                displayMsg: viewModel.Pager_DisplayItems
            });

            indexViewManager_StatusLogs.elelments.pagination.removeClass("hide");
        } else {
            indexViewManager_StatusLogs.elelments.pagination.addClass("hide");
        }
    }
};
