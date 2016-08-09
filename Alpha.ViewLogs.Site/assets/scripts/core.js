'use strict';
var alphaViewLogs = {
    format: {
        monthFormat: 'MMMM yyyy',
        shortDateWithSlash: 'MM/dd/yyyy'
    },
    urls: {
        viewCustomerSettingLogs: './customersettinglogs/getlogs'
    },
    pageList: [1, 2, 5, 10],
    //pageList: [10, 50, 100, 500],
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
