'use strict';
var alphaViewLogs = {
    format: {
        monthFormat: 'MMMM yyyy',
        shortDateWithSlash: 'MM/dd/yyyy'
    },
    urls: {
        queryUsername: document.getElementById('usernameAutoSuggestionPath').value
    },
    keyCode: {
        enter: 13
    },
    pageList: [10, 50, 100, 500],
    callRequest: function (options) {
        $.ajax({
            type: "POST",
            url: options.url,
            data: options.data,
            responseType: "json",
            success: options.success
        });
    },
    cancelBubbleEvent: function (event) {
        if (alphaViewLogs.isIE()) {
            window.event.cancelBubble = true;
            window.event.returnValue = false;
        }
        else {
            event.preventDefault();
            event.stopPropagation();
        }
        return false;
    },
    isIE: function () {
        return /msie/i.test(navigator.userAgent) &&
            !/opera/i.test(navigator.userAgent);
    }
};
//# sourceMappingURL=core.js.map