/// <reference path="../core.ts" />
/// <reference path="index-controller.ts" />

'use strict';
/*global loginController, alphaViewLogs, viewModel*/
declare var $: any;
declare var initAutoComplete: any;

var loginView = {
    elelments: {
        btnSubmit: null,
        ddlMonth: null,
        iconLoading: null,
        txtUsername: null,
        pagination: null,
        longMonthsInYear: null
    },
    showOldValuesCookieName: '',
    pageNumber: 0,
    pageSize: null,
    cacheElements: function () {
        loginView.elelments.btnSubmit = $("#btn-submit");
        loginView.elelments.ddlMonth = $('#ddlMonth');
        loginView.elelments.iconLoading = $('#icon-loading');
        loginView.elelments.txtUsername = $('#txt-username');
        loginView.elelments.pagination = $('#pagination');
        loginView.elelments.longMonthsInYear = $('#longMonthsInYear');
    },
    initElements: function (viewModel) {
        loginView.cacheElements();

        loginView.initMonthPicker(viewModel);

        loginView.elelments.btnSubmit.click(function () {
            loginView.pageNumber = 0;
            loginView.pageSize = '';

            loginController.viewLogs();
        });

        loginView.elelments.txtUsername.keypress(function (e) {
            if (!e) {
                e = window.event;
            }

            var key = (e.keyCode) ? e.keyCode : e.which;
            if (key == alphaViewLogs.keyCode.enter) {
                loginView.pageNumber = 0;
                loginView.pageSize = '';
                loginController.viewLogs();

                return alphaViewLogs.cancelBubbleEvent(e);
            }
        });

        var queryUsernameURL = alphaViewLogs.urls.queryUsername + "?" +
            $.param({
                issub: 0,
                ismember: 0
            });

        initAutoComplete($('#txt-username')[0], queryUsernameURL);

        loginView.renderPager({
            totalRecords: viewModel.totalRecords,
            pageSize: viewModel.pageSize,
            pageIndex: viewModel.pageIndex + 1
        });

        loginView.pageNumber = viewModel.pageIndex;
        loginView.pageSize = viewModel.pageSize;
    },
    initMonthPicker: function (viewModel) {
        loginView.elelments.ddlMonth.monPickr({
            longMonthsInYear: loginView.getLongMonthsInYear(),
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
        loginView.elelments.btnSubmit.attr('disabled', 'disabled');
        loginView.elelments.iconLoading.removeClass('hide');
    },
    showCompleteLoadingState: function () {
        loginView.elelments.iconLoading.addClass('hide');
        loginView.elelments.btnSubmit.attr('disabled', null);
    },

    getParams: function () {
        return {
            ActionTypeId: viewModel.actionTypeId,
            PageIndex: loginView.pageNumber,
            PageSize: loginView.pageSize,
            Username: loginView.elelments.txtUsername.val(),
            FromDate: loginView.elelments.ddlMonth.monPickr("getFromDate", alphaViewLogs.format.shortDateWithSlash),
            ToDate: loginView.elelments.ddlMonth.monPickr("getToDate", alphaViewLogs.format.shortDateWithSlash)
        };
    },
    renderPager: function (options) {
        if (alphaViewLogs.pageList[0] <= options.totalRecords) {
            loginView.elelments.pagination.pagination({
                pageList: alphaViewLogs.pageList,
                total: options.totalRecords,
                pageSize: options.pageSize,
                pageNumber: options.pageIndex,

                showRefresh: false,
                onSelectPage: function (pageNumber, pageSize) {
                    loginView.pageNumber = pageNumber - 1;
                    loginView.pageSize = pageSize;

                    loginController.viewLogs();
                },
                beforePageText: viewModel.Page,
                afterPageText: viewModel.of + ' {pages}',
                displayMsg: viewModel.Pager_DisplayItems
            });

            loginView.elelments.pagination.removeClass("hide");
        } else {
            loginView.elelments.pagination.addClass("hide");
        }
    },
    getLongMonthsInYear: function () {
        var longMonthsInYear = eval(loginView.elelments.longMonthsInYear.val());
        return longMonthsInYear;
    }
};