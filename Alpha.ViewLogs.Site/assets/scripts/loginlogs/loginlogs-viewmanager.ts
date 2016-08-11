/// <reference path="../core.ts" />
/// <reference path="loginlogs-manager.ts" />

'use strict';
/*global loginLogsManager, alphaViewLogs, viewModel*/
declare var $: any;
declare var initAutoComplete: any;

var loginLogsViewManager = {
    elelments: {
        btnSubmit: null,
        ddlMonth: null,
        iconLoading: null,
        txtUsername: null,
        logTableContent: null,
        linkChangTabs: null,
        pagination: null,
        longMonthsInYear: null
    },
    showOldValuesCookieName: '',
    pageNumber: 0,
    pageSize: null,
    cacheElements: function () {
        loginLogsViewManager.elelments.btnSubmit = $("#btn-submit");
        loginLogsViewManager.elelments.ddlMonth = $('#ddlMonth');
        loginLogsViewManager.elelments.iconLoading = $('#icon-loading');
        loginLogsViewManager.elelments.txtUsername = $('#txt-username');
        loginLogsViewManager.elelments.linkChangTabs = $('.lnk-changetab');
        loginLogsViewManager.elelments.pagination = $('#pagination');
        loginLogsViewManager.elelments.longMonthsInYear = $('#longMonthsInYear');
    },
    initElements: function (viewModel) {
        loginLogsViewManager.cacheElements();

        loginLogsViewManager.initMonthPicker(viewModel);

        loginLogsViewManager.elelments.btnSubmit.click(function () {
            loginLogsManager.viewLogs();
        });

        loginLogsViewManager.elelments.txtUsername.keypress(function (e) {
            if (!e) {
                e = window.event;
            }

            var key = (e.keyCode) ? e.keyCode : e.which;
            if (key == alphaViewLogs.keyCode.enter) {
                loginLogsManager.viewLogs();

                return alphaViewLogs.cancelBubbleEvent(e);
            }
        });

        var queryUsernameURL = alphaViewLogs.urls.queryUsername + "?" +
            $.param({
                issub: 0,
                ismember: 0
            });

        initAutoComplete($('#txt-username')[0], queryUsernameURL);

        loginLogsViewManager.elelments.linkChangTabs.click(function () {
            var url = $(this).data("tablink") + "?" +
                $.param(loginLogsManager.viewLogsParams) +
                "&actiontypeid=" + viewModel.actionTypeId;
            window.location.href = url;
        });

        loginLogsViewManager.renderPager({
            totalRecords: viewModel.totalRecords,
            pageSize: viewModel.pageSize,
            pageIndex: viewModel.pageIndex + 1
        });

        loginLogsViewManager.pageNumber = viewModel.pageIndex;
        loginLogsViewManager.pageSize = viewModel.pageSize;
    },
    initMonthPicker: function (viewModel) {
        loginLogsViewManager.elelments.ddlMonth.monPickr({
            longMonthsInYear: loginLogsViewManager.getLongMonthsInYear(),
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
        loginLogsViewManager.elelments.btnSubmit.attr('disabled', 'disabled');
        loginLogsViewManager.elelments.iconLoading.removeClass('hide');
    },
    showCompleteLoadingState: function () {
        loginLogsViewManager.elelments.iconLoading.addClass('hide');
        loginLogsViewManager.elelments.btnSubmit.attr('disabled', null);
    },

    getParams: function () {
        return {
            ActionTypeId: viewModel.actionTypeId,
            PageIndex: loginLogsViewManager.pageNumber,
            PageSize: loginLogsViewManager.pageSize,
            Username: loginLogsViewManager.elelments.txtUsername.val(),
            FromDate: loginLogsViewManager.elelments.ddlMonth.monPickr("getFromDate", alphaViewLogs.format.shortDateWithSlash),
            ToDate: loginLogsViewManager.elelments.ddlMonth.monPickr("getToDate", alphaViewLogs.format.shortDateWithSlash)
        };
    },
    renderPager: function (options) {
        if (options.pageSize <= options.totalRecords) {
            loginLogsViewManager.elelments.pagination.pagination({
                pageList: alphaViewLogs.pageList,
                total: options.totalRecords,
                pageSize: options.pageSize,
                pageNumber: options.pageIndex,

                showRefresh: false,
                onSelectPage: function (pageNumber, pageSize) {
                    loginLogsViewManager.pageNumber = pageNumber - 1;
                    loginLogsViewManager.pageSize = pageSize;

                    loginLogsManager.viewLogs();
                },
                beforePageText: viewModel.Page,
                afterPageText: viewModel.of + ' {pages}',
                displayMsg: viewModel.Pager_DisplayItems
            });

            loginLogsViewManager.elelments.pagination.removeClass("hide");
        } else {
            loginLogsViewManager.elelments.pagination.addClass("hide");
        }
    },
    getLongMonthsInYear: function () {
        var longMonthsInYear = eval(loginLogsViewManager.elelments.longMonthsInYear.val());
        return longMonthsInYear;
    }
};