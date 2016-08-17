'use strict';

var tabController = {
    viewLogsParams: null,
    init: function () {
        $('.lnk-changetab').click(function () {
            var url = $(this).data("tablink") + "?" + $.param(tabController.viewLogsParams);
            window.location.href = url;
        });
    }
};

$(function () {
    tabController.init();
});