/// <reference path="index-manager.ts" />

'use strict';
/*global indexManager*/
declare var $: any;

var indexViewManager = {
    elelments: {
        ddlLogActionType: null,
        btnSubmit: null,
        ddlMonth: null,
        iconLoading: null,
        chkOldValues: null
    },
    showOldValuesCookieName: '',
    cacheElements: function () {
        indexViewManager.elelments.ddlLogActionType = $('#ddl-logactiontype');
        indexViewManager.elelments.btnSubmit = $("#btn-submit");
        indexViewManager.elelments.ddlMonth = $('#ddlMonth');
        indexViewManager.elelments.iconLoading = $('#icon-loading');
        indexViewManager.elelments.chkOldValues = $('#chk-oldvalues');
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
            monthFormat: 'MMMM yyyy',
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
    }
};
