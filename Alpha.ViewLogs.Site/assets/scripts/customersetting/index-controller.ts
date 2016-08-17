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

        customerSettingController.viewLogsParams = customerSettingView.getParams();
        tabController.viewLogsParams = customerSettingController.viewLogsParams;
    },
    viewLogsParams: null,
    viewLogs: function () {
        setVisible('hidden');
        customerSettingView.showLoadingState();

        customerSettingController.viewLogsParams = customerSettingView.getParams();
        tabController.viewLogsParams = customerSettingController.viewLogsParams;

        var url = customerSettingController.urls.index + "?" + $.param(customerSettingController.viewLogsParams);
        window.location.href = url;
    }
};

$(function () {
    customerSettingController.init();
});
