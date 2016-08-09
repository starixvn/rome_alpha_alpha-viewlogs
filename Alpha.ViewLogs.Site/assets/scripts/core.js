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