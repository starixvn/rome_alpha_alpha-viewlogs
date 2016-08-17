//# sourceMappingURL=core.js.map
/// <reference path="../core.ts" />
/// <reference path="index-controller.ts" />

'use strict';
/*global statusController, alphaViewLogs*/
declare var $: any;
declare var initAutoComplete: any;
declare var setVisible: any;

var statusView = {
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
        statusView.elelments.btnSubmit = $("#btn-submit");
        statusView.elelments.ddlMonth = $('#ddlMonth');
        statusView.elelments.iconLoading = $('#icon-loading');
        statusView.elelments.chkOldValues = $('#chk-oldvalues');
        statusView.elelments.txtUsername = $('#txt-username');
        statusView.elelments.logTableContent = $('#DataLog tbody');
        statusView.elelments.pagination = $('#pagination');
    },
    initElements: function (viewModel) {
        statusView.cacheElements();

        statusView.initMonthPicker(viewModel);

        statusView.elelments.btnSubmit.click(function () {
            statusController.viewLogs();
        });

        statusView.elelments.chkOldValues.click(function () {
            var isShowOldValues = statusView.elelments.chkOldValues.prop("checked") ? 1 : 0;
            $.cookie(viewModel.showOldValuesCookieName, isShowOldValues, { path: '/' });
            statusController.viewLogs();
        });

        statusView.elelments.txtUsername.keypress(function (e) {
            if (!e) {
                e = window.event;
            }

            var key = (e.keyCode) ? e.keyCode : e.which;
            if (key == alphaViewLogs.keyCode.enter) {
                statusController.viewLogs();

                return alphaViewLogs.cancelBubbleEvent(e);
            }
        });

        var queryUsernameURL = alphaViewLogs.urls.queryUsername + "?" +
            $.param({
                issub: 0,
                ismember: 0
            });

        initAutoComplete($('#txt-username')[0], queryUsernameURL);

        statusView.renderPager({
            totalRecords: viewModel.totalRecords,
            pageSize: viewModel.pageSize,
            pageIndex: viewModel.pageIndex + 1
        });

        statusView.pageNumber = viewModel.pageIndex;
        statusView.pageSize = viewModel.pageSize;
    },
    initMonthPicker: function (viewModel) {
        statusView.elelments.ddlMonth.monPickr({
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
        statusView.elelments.btnSubmit.attr('disabled', 'disabled');
        statusView.elelments.iconLoading.removeClass('hide');
    },
    showCompleteLoadingState: function () {
        statusView.elelments.iconLoading.addClass('hide');
        statusView.elelments.btnSubmit.attr('disabled', null);
    },
    getParams: function () {
        return {
            ActionTypeId: viewModel.actionTypeId,
            PageIndex: statusView.pageNumber,
            PageSize: statusView.pageSize,
            Username: statusView.elelments.txtUsername.val(),
            FromDate: statusView.elelments.ddlMonth.monPickr("getFromDate", alphaViewLogs.format.shortDateWithSlash),
            ToDate: statusView.elelments.ddlMonth.monPickr("getToDate", alphaViewLogs.format.shortDateWithSlash)
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

        statusView.elelments.logTableContent.empty();
        statusView.elelments.logTableContent.append(logContent);

        statusView.renderPager({
            totalRecords: result.TotalRecords,
            pageSize: result.PageSize,
            pageNumber: result.PageIndex
        });
    },
    renderPager: function (options) {
        if (options.pageSize <= options.totalRecords) {
            statusView.elelments.pagination.pagination({
                pageList: alphaViewLogs.pageList,
                total: options.totalRecords,
                pageSize: options.pageSize,
                pageNumber: options.pageIndex,

                showRefresh: false,
                onSelectPage: function (pageNumber, pageSize) {
                    statusView.pageNumber = pageNumber - 1;
                    statusView.pageSize = pageSize;

                    statusController.viewLogs();
                },
                beforePageText: viewModel.Page,
                afterPageText: viewModel.of + ' {pages}',
                displayMsg: viewModel.Pager_DisplayItems
            });

            statusView.elelments.pagination.removeClass("hide");
        } else {
            statusView.elelments.pagination.addClass("hide");
        }
    }
};
