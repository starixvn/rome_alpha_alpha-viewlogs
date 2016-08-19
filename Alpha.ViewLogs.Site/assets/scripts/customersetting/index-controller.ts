/// <reference path="../core.ts" />
/// <reference path="../tab/index.ts" />
/// <reference path="index-view.ts" />

'use strict';
/*global customerSettingView, alphaViewLogs, tabController*/
declare var $: any;
declare var viewModel: any;

var customerSettingController = {
    urls: {
        index: (<HTMLInputElement>document.getElementById('indexPath')).value
    },
    init: function () {
        customerSettingView.initElements(viewModel);

        tabController.viewLogsParams = customerSettingView.getParams();
    },
    viewLogs: function () {
        setVisible('hidden');
        customerSettingView.showLoadingState();

        var viewLogsParams = customerSettingView.getParams();
        tabController.viewLogsParams = viewLogsParams;

        var url = customerSettingController.urls.index + "?" + $.param(viewLogsParams);
        window.location.href = url;
    }
};

$(function () {
    customerSettingController.init();
});
