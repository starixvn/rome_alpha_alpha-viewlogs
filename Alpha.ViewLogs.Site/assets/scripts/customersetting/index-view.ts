//# sourceMappingURL=core.js.map
/// <reference path="../core.ts" />
/// <reference path="index-controller.ts" />

'use strict';
/*global customerSettingController, alphaViewLogs, viewModel*/
declare var $: any;
declare var initAutoComplete: any;

var customerSettingView = {
    elelments: {
        ddlLogActionType: null,
        btnSubmit: null,
        ddlMonth: null,
        iconLoading: null,
        chkOldValues: null,
        txtUsername: null,
        pagination: null,
        longMonthsInYear: null
    },
    showOldValuesCookieName: '',
    pageNumber: 0,
    pageSize: null,
    cacheElements: function () {
        customerSettingView.elelments.ddlLogActionType = $('#ddl-logactiontype');
        customerSettingView.elelments.btnSubmit = $("#btn-submit");
        customerSettingView.elelments.ddlMonth = $('#ddlMonth');
        customerSettingView.elelments.iconLoading = $('#icon-loading');
        customerSettingView.elelments.chkOldValues = $('#chk-oldvalues');
        customerSettingView.elelments.txtUsername = $('#txt-username');
        customerSettingView.elelments.pagination = $('#pagination');
        customerSettingView.elelments.longMonthsInYear = $('#longMonthsInYear');
    },
    initElements: function (viewModel) {
        customerSettingView.cacheElements();

        customerSettingView.elelments.ddlLogActionType.change(function () {
            customerSettingView.pageNumber = 0;
            customerSettingView.pageSize = '';

            customerSettingController.viewLogs();
        });

        customerSettingView.initMonthPicker(viewModel);

        customerSettingView.elelments.btnSubmit.click(function () {
            customerSettingView.pageNumber = 0;
            customerSettingView.pageSize = '';

            customerSettingController.viewLogs();
        });

        customerSettingView.elelments.chkOldValues.click(function () {
            var isShowOldValues = customerSettingView.elelments.chkOldValues.prop("checked") ? 1 : 0;
            $.cookie(viewModel.showOldValuesCookieName, isShowOldValues, { path: '/' });
            customerSettingController.viewLogs();
        });

        customerSettingView.elelments.txtUsername.keypress(function (e) {
            if (!e) {
                e = window.event;
            }

            var key = (e.keyCode) ? e.keyCode : e.which;
            if (key == alphaViewLogs.keyCode.enter) {
                customerSettingView.pageNumber = 0;
                customerSettingView.pageSize = '';
                customerSettingController.viewLogs();

                return alphaViewLogs.cancelBubbleEvent(e);
            }
        });

        var queryUsernameURL = alphaViewLogs.urls.queryUsername + "?" +
            $.param({
                issub: 0,
                ismember: 0
            });

        initAutoComplete($('#txt-username')[0], queryUsernameURL);

        customerSettingView.renderPager({
            totalRecords: viewModel.totalRecords,
            pageSize: viewModel.pageSize,
            pageIndex: viewModel.pageIndex + 1
        });

        customerSettingView.pageNumber = viewModel.pageIndex;
        customerSettingView.pageSize = viewModel.pageSize;
    },
    initMonthPicker: function (viewModel) {
        customerSettingView.elelments.ddlMonth.monPickr({
            longMonthsInYear: customerSettingView.getLongMonthsInYear(),
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
        customerSettingView.elelments.btnSubmit.attr('disabled', 'disabled');
        customerSettingView.elelments.iconLoading.removeClass('hide');
    },
    showCompleteLoadingState: function () {
        customerSettingView.elelments.iconLoading.addClass('hide');
        customerSettingView.elelments.btnSubmit.attr('disabled', null);
    },

    getParams: function () {
        return {
            PageIndex: customerSettingView.pageNumber,
            PageSize: customerSettingView.pageSize,
            Username: customerSettingView.elelments.txtUsername.val(),
            FromDate: customerSettingView.elelments.ddlMonth.monPickr("getFromDate", alphaViewLogs.format.shortDateWithSlash),
            ToDate: customerSettingView.elelments.ddlMonth.monPickr("getToDate", alphaViewLogs.format.shortDateWithSlash),
            ActionTypeId: customerSettingView.elelments.ddlLogActionType.val()
        };
    },
    renderPager: function (options) {
        if (alphaViewLogs.pageList[0] <= options.totalRecords) {
            customerSettingView.elelments.pagination.pagination({
                pageList: alphaViewLogs.pageList,
                total: options.totalRecords,
                pageSize: options.pageSize,
                pageNumber: options.pageIndex,

                showRefresh: false,
                onSelectPage: function (pageNumber, pageSize) {
                    customerSettingView.pageNumber = pageNumber - 1;
                    customerSettingView.pageSize = pageSize;

                    customerSettingController.viewLogs();
                },
                beforePageText: viewModel.Page,
                afterPageText: viewModel.of + ' {pages}',
                displayMsg: viewModel.Pager_DisplayItems
            });

            customerSettingView.elelments.pagination.removeClass("hide");
        } else {
            customerSettingView.elelments.pagination.addClass("hide");
        }
    },
    getLongMonthsInYear: function () {
        var longMonthsInYear = eval(customerSettingView.elelments.longMonthsInYear.val());
        return longMonthsInYear;
    }
};