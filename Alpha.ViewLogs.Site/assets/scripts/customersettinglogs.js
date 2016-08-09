'use strict';
var alphaViewLogs = {
    format: {
        monthFormat: 'MMMM yyyy',
        shortDateWithSlash: 'MM/dd/yyyy'
    },
    urls: {
        viewCustomerSettingLogs: './customersettinglogs/getlogs'
    },
    callRequest: function (options) {
        $.ajax({
            type: "POST",
            url: options.url,
            data: options.data,
            responseType: "json",
            success: options.success
        });
    }
};
//# sourceMappingURL=core.js.map
/// <reference path="../core.ts" />
/// <reference path="index-manager.ts" />
'use strict';
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
//# sourceMappingURL=index-viewmanager.js.map
/// <reference path="../core.ts" />
/// <reference path="index-viewmanager.ts" />
'use strict';
var indexManager = {
    init: function () {
        indexViewManager.initElements(viewModel);
    },
    viewLogs: function () {
        indexViewManager.showLoadingState();
        alphaViewLogs.callRequest({
            url: alphaViewLogs.urls.viewCustomerSettingLogs,
            data: indexViewManager.getParams(),
            success: function () {
                indexViewManager.showCompleteLoadingState();
            }
        });
    }
};
$(function () {
    indexManager.init();
});
//# sourceMappingURL=index-manager.js.map