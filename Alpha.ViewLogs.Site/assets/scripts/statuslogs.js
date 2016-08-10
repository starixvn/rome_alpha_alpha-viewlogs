function slideToggle(id) {
    var obj = null;
    if (typeof (id) == 'object') {
        var rowIndex = id.rowIndex;
        var p = id.parentNode ? id.parentNode : id.parentElement;
        var table = p.parentNode ? p.parentNode : p.parentElement;
        var cell = table.rows[rowIndex + 1].cells[0];
        obj = cell.firstElementChild ? cell.firstElementChild : cell.firstChild;
    }
    else {
        obj = document.getElementById(id);
    }
    if (obj != null) {
        if (obj.style.display == 'none') {
            obj.style.display = 'block';
        }
        else {
            obj.style.display = 'none';
        }
    }
}

'use strict';
var alphaViewLogs = {
    format: {
        monthFormat: 'MMMM yyyy',
        shortDateWithSlash: 'MM/dd/yyyy'
    },
    urls: {
        queryUsername: document.getElementById('usernameAutoSuggestionPath').value
    },
    keyCode: {
        enter: 13
    },
    pageList: [10, 50, 100, 500],
    callRequest: function (options) {
        $.ajax({
            type: "POST",
            url: options.url,
            data: options.data,
            responseType: "json",
            success: options.success
        });
    },
    cancelBubbleEvent: function (event) {
        if (alphaViewLogs.isIE()) {
            window.event.cancelBubble = true;
            window.event.returnValue = false;
        }
        else {
            event.preventDefault();
            event.stopPropagation();
        }
        return false;
    },
    isIE: function () {
        return /msie/i.test(navigator.userAgent) &&
            !/opera/i.test(navigator.userAgent);
    }
};

/// <reference path="../core.ts" />
/// <reference path="index-manager.ts" />
'use strict';
var indexViewManager_StatusLogs = {
    elelments: {
        btnSubmit: null,
        ddlMonth: null,
        iconLoading: null,
        chkOldValues: null,
        txtUsername: null,
        logTableContent: null,
        linkChangTabs: null,
        pagination: null,
        actionTypeId: null
    },
    showOldValuesCookieName: '',
    cacheElements: function () {
        indexViewManager_StatusLogs.elelments.btnSubmit = $("#btn-submit");
        indexViewManager_StatusLogs.elelments.ddlMonth = $('#ddlMonth');
        indexViewManager_StatusLogs.elelments.iconLoading = $('#icon-loading');
        indexViewManager_StatusLogs.elelments.chkOldValues = $('#chk-oldvalues');
        indexViewManager_StatusLogs.elelments.txtUsername = $('#txt-username');
        indexViewManager_StatusLogs.elelments.logTableContent = $('#DataLog tbody');
        indexViewManager_StatusLogs.elelments.linkChangTabs = $('.lnk-changetab');
        indexViewManager_StatusLogs.elelments.pagination = $('#pp');
        indexViewManager_StatusLogs.elelments.actionTypeId = $('#actionTypeId');
    },
    initElements: function (viewModel) {
        indexViewManager_StatusLogs.cacheElements();
        indexViewManager_StatusLogs.initMonthPicker(viewModel);
        indexViewManager_StatusLogs.elelments.btnSubmit.click(function () {
            indexManager.viewLogs();
        });
        indexViewManager_StatusLogs.elelments.chkOldValues.click(function () {
            var isShowOldValues = indexViewManager_StatusLogs.elelments.chkOldValues.prop("checked") ? 1 : 0;
            $.cookie(viewModel.showOldValuesCookieName, isShowOldValues);
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
            var url = $(this).data("tablink") + "?" +
                $.param(indexManager.viewLogsParams) +
                "&actiontypeid=" + indexViewManager_StatusLogs.elelments.actionTypeId.val();
            window.location.href = url;
        });
        indexViewManager_StatusLogs.renderPager({
            totalRecords: viewModel.totalRecords,
            pageSize: viewModel.pageSize,
            pageNumber: viewModel.pageIndex
        });
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
        indexViewManager_StatusLogs.elelments.logTableContent.empty();
        indexViewManager_StatusLogs.elelments.logTableContent.append(logContent);
        indexViewManager_StatusLogs.renderPager({
            totalRecords: result.TotalRecords,
            pageSize: result.PageSize,
            pageNumber: result.PageIndex
        });
    },
    renderPager: function (options) {
        if (options.totalRecords) {
            indexViewManager_StatusLogs.elelments.pagination.pagination({
                pageList: alphaViewLogs.pageList,
                total: options.totalRecords,
                pageSize: options.pageSize,
                pageNumber: options.pageIndex
            });
            indexViewManager_StatusLogs.elelments.pagination.removeClass("hide");
        }
        else {
            indexViewManager_StatusLogs.elelments.pagination.addClass("hide");
        }
    }
};

/// <reference path="../core.ts" />
/// <reference path="index-viewmanager-statuslogs.ts" />
'use strict';
var indexManager = {
    urls: {
        viewLogs: document.getElementById('viewLogsPath').value
    },
    init: function () {
        indexViewManager_StatusLogs.initElements(viewModel);
        indexManager.viewLogsParams = indexViewManager_StatusLogs.getParams();
    },
    viewLogsParams: null,
    viewLogs: function () {
        indexViewManager_StatusLogs.showLoadingState();
        indexManager.viewLogsParams = indexViewManager_StatusLogs.getParams();
        alphaViewLogs.callRequest({
            url: indexManager.urls.viewLogs,
            data: indexManager.viewLogsParams,
            success: function (result) {
                indexViewManager_StatusLogs.renderLogs(result);
                indexViewManager_StatusLogs.showCompleteLoadingState();
            }
        });
    }
};
$(function () {
    indexManager.init();
});