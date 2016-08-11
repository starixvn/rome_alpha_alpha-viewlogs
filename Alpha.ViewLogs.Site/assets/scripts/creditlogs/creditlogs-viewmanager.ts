/// <reference path="../core.ts" />
/// <reference path="creditlogs-manager.ts" />

'use strict';
/*global creditLogsManager, alphaViewLogs, viewModel*/
declare var $: any;
declare var initAutoComplete: any;

var creditLogsViewManager = {
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
        creditLogsViewManager.elelments.btnSubmit = $("#btn-submit");
        creditLogsViewManager.elelments.ddlMonth = $('#ddlMonth');
        creditLogsViewManager.elelments.iconLoading = $('#icon-loading');
        creditLogsViewManager.elelments.txtUsername = $('#txt-username');
        creditLogsViewManager.elelments.linkChangTabs = $('.lnk-changetab');
        creditLogsViewManager.elelments.pagination = $('#pagination');
        creditLogsViewManager.elelments.longMonthsInYear = $('#longMonthsInYear');
    },
    initElements: function (viewModel) {
        creditLogsViewManager.cacheElements();

        creditLogsViewManager.initMonthPicker(viewModel);

        creditLogsViewManager.elelments.btnSubmit.click(function () {
            creditLogsManager.viewLogs();
        });

        creditLogsViewManager.elelments.txtUsername.keypress(function (e) {
            if (!e) {
                e = window.event;
            }

            var key = (e.keyCode) ? e.keyCode : e.which;
            if (key == alphaViewLogs.keyCode.enter) {
                creditLogsManager.viewLogs();

                return alphaViewLogs.cancelBubbleEvent(e);
            }
        });

        var queryUsernameURL = alphaViewLogs.urls.queryUsername + "?" +
            $.param({
                issub: 0,
                ismember: 0
            });

        initAutoComplete($('#txt-username')[0], queryUsernameURL);

        creditLogsViewManager.elelments.linkChangTabs.click(function () {
            var url = $(this).data("tablink") + "?" + $.param(creditLogsManager.viewLogsParams);
            window.location.href = url;
        });

        creditLogsViewManager.renderPager({
            totalRecords: viewModel.totalRecords,
            pageSize: viewModel.pageSize,
            pageIndex: viewModel.pageIndex + 1
        });

        creditLogsViewManager.pageNumber = viewModel.pageIndex;
        creditLogsViewManager.pageSize = viewModel.pageSize;
    },
    initMonthPicker: function (viewModel) {
        creditLogsViewManager.elelments.ddlMonth.monPickr({
            longMonthsInYear: creditLogsViewManager.getLongMonthsInYear(),
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
        creditLogsViewManager.elelments.btnSubmit.attr('disabled', 'disabled');
        creditLogsViewManager.elelments.iconLoading.removeClass('hide');
    },
    showCompleteLoadingState: function () {
        creditLogsViewManager.elelments.iconLoading.addClass('hide');
        creditLogsViewManager.elelments.btnSubmit.attr('disabled', null);
    },

    getParams: function () {
        return {
            ActionTypeId: viewModel.actionTypeId,
            PageIndex: creditLogsViewManager.pageNumber,
            PageSize: creditLogsViewManager.pageSize,
            Username: creditLogsViewManager.elelments.txtUsername.val(),
            FromDate: creditLogsViewManager.elelments.ddlMonth.monPickr("getFromDate", alphaViewLogs.format.shortDateWithSlash),
            ToDate: creditLogsViewManager.elelments.ddlMonth.monPickr("getToDate", alphaViewLogs.format.shortDateWithSlash)
        };
    },
    renderPager: function (options) {
        if (options.pageSize <= options.totalRecords) {
            creditLogsViewManager.elelments.pagination.pagination({
                pageList: alphaViewLogs.pageList,
                total: options.totalRecords,
                pageSize: options.pageSize,
                pageNumber: options.pageIndex,

                showRefresh: false,
                onSelectPage: function (pageNumber, pageSize) {
                    creditLogsViewManager.pageNumber = pageNumber - 1;
                    creditLogsViewManager.pageSize = pageSize;

                    creditLogsManager.viewLogs();
                },
                beforePageText: viewModel.Page,
                afterPageText: viewModel.of + ' {pages}',
                displayMsg: viewModel.Pager_DisplayItems
            });

            creditLogsViewManager.elelments.pagination.removeClass("hide");
        } else {
            creditLogsViewManager.elelments.pagination.addClass("hide");
        }
    },
    getLongMonthsInYear: function () {
        var longMonthsInYear = eval(creditLogsViewManager.elelments.longMonthsInYear.val());
        return longMonthsInYear;
    }
};