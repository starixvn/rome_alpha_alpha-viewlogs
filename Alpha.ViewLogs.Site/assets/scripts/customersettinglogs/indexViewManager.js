'use strict';
/*global viewModel*/
var indexViewManager = {
    initMothPicker: function () {
        $('#ddl-month').monPickr({
            toDay: viewModel,
            fromDay: fromDay,
            maxDate: today,
            minDate: minHistoryReportDate,
            showDateRange: true,
            monthFormat: 'MMMM yyyy',
            fromDayId: 'ddlFromDay',
            toDayId: 'ddlToDay',
            fromDayLabel: viewModel.labelFrom,
            toDayLabel: viewModel.labelTo,
            sortDesc: true,
            log: function (msg) {
                console.warn(msg);
            }
        });
    }
};
