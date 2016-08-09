/// <reference path="../core.ts" />
/// <reference path="index-manager.ts" />

'use strict';
/*global indexManager, alphaViewLogs*/
declare var $: any;

var indexViewManager = {
    elelments: {
        ddlLogActionType: null,
        btnSubmit: null,
        ddlMonth: null,
        iconLoading: null,
        chkOldValues: null,
        txtUsername: null,

    },
    showOldValuesCookieName: '',
    cacheElements: function () {
        indexViewManager.elelments.ddlLogActionType = $('#ddl-logactiontype');
        indexViewManager.elelments.btnSubmit = $("#btn-submit");
        indexViewManager.elelments.ddlMonth = $('#ddlMonth');
        indexViewManager.elelments.iconLoading = $('#icon-loading');
        indexViewManager.elelments.chkOldValues = $('#chk-oldvalues');
        indexViewManager.elelments.txtUsername = $('txt-username');

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
        indexViewManager.elelments.iconLoading.show();
    },
    showCompleteLoadingState: function () {
        indexViewManager.elelments.iconLoading.hide();
        indexViewManager.elelments.btnSubmit.attr('disabled', null);
    },
    getParams: function () {
        return {
            Username: indexViewManager.elelments.txtUsername.val(),
            FromDate: indexViewManager.elelments.ddlMonth.monPickr("getFromDate", alphaViewLogs.format.shortDateWithSlash),
            ToDate: indexViewManager.elelments.ddlMonth.monPickr("getToDate", alphaViewLogs.format.shortDateWithSlash),
            ActionTypeId: indexViewManager.elelments.ddlLogActionType.val()
        };
    }
};
