/// <reference path="../core.ts" />
/// <reference path="../tab/index.ts" />
/// <reference path="index-controller.ts" />

'use strict';
/*global creditController, alphaViewLogs, viewModel, tabController*/
declare var $: any;
declare var initAutoComplete: any;

var creditView = {
    elelments: {
        btnSubmit: null,
        ddlMonth: null,
        iconLoading: null,
        txtUsername: null,
        logTableContent: null,
        pagination: null,
        longMonthsInYear: null
    },
    showOldValuesCookieName: '',
    pageNumber: 0,
    pageSize: null,
    cacheElements: function () {
        creditView.elelments.btnSubmit = $("#btn-submit");
        creditView.elelments.ddlMonth = $('#ddlMonth');
        creditView.elelments.iconLoading = $('#icon-loading');
        creditView.elelments.txtUsername = $('#txt-username');
        creditView.elelments.pagination = $('#pagination');
        creditView.elelments.longMonthsInYear = $('#longMonthsInYear');
    },
    initElements: function (viewModel) {
        creditView.cacheElements();

        creditView.initMonthPicker(viewModel);

        creditView.elelments.btnSubmit.click(function () {
            creditController.viewLogs();
        });

        creditView.elelments.txtUsername.keypress(function (e) {
            if (!e) {
                e = window.event;
            }

            var key = (e.keyCode) ? e.keyCode : e.which;
            if (key == alphaViewLogs.keyCode.enter) {
                creditController.viewLogs();

                return alphaViewLogs.cancelBubbleEvent(e);
            }
        });

        var queryUsernameURL = alphaViewLogs.urls.queryUsername + "?" +
            $.param({
                issub: 0,
                ismember: 0
            });

        initAutoComplete($('#txt-username')[0], queryUsernameURL);

        creditView.renderPager({
            totalRecords: viewModel.totalRecords,
            pageSize: viewModel.pageSize,
            pageIndex: viewModel.pageIndex + 1
        });

        creditView.pageNumber = viewModel.pageIndex;
        creditView.pageSize = viewModel.pageSize;
    },
    initMonthPicker: function (viewModel) {
        creditView.elelments.ddlMonth.monPickr({
            longMonthsInYear: creditView.getLongMonthsInYear(),
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
        creditView.elelments.btnSubmit.attr('disabled', 'disabled');
        creditView.elelments.iconLoading.removeClass('hide');
    },
    showCompleteLoadingState: function () {
        creditView.elelments.iconLoading.addClass('hide');
        creditView.elelments.btnSubmit.attr('disabled', null);
    },

    getParams: function () {
        return {
            ActionTypeId: viewModel.actionTypeId,
            PageIndex: creditView.pageNumber,
            PageSize: creditView.pageSize,
            Username: creditView.elelments.txtUsername.val(),
            FromDate: creditView.elelments.ddlMonth.monPickr("getFromDate", alphaViewLogs.format.shortDateWithSlash),
            ToDate: creditView.elelments.ddlMonth.monPickr("getToDate", alphaViewLogs.format.shortDateWithSlash)
        };
    },
    renderPager: function (options) {
        if (options.pageSize <= options.totalRecords) {
            creditView.elelments.pagination.pagination({
                pageList: alphaViewLogs.pageList,
                total: options.totalRecords,
                pageSize: options.pageSize,
                pageNumber: options.pageIndex,

                showRefresh: false,
                onSelectPage: function (pageNumber, pageSize) {
                    creditView.pageNumber = pageNumber - 1;
                    creditView.pageSize = pageSize;

                    creditController.viewLogs();
                },
                beforePageText: viewModel.Page,
                afterPageText: viewModel.of + ' {pages}',
                displayMsg: viewModel.Pager_DisplayItems
            });

            creditView.elelments.pagination.removeClass("hide");
        } else {
            creditView.elelments.pagination.addClass("hide");
        }
    },
    getLongMonthsInYear: function () {
        var longMonthsInYear = eval(creditView.elelments.longMonthsInYear.val());
        return longMonthsInYear;
    }
};