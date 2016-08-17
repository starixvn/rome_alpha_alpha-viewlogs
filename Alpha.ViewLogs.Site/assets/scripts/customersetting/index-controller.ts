/// <reference path="../core.ts" />
/// <reference path="index-view.ts" />

'use strict';
/*global customerSettingView, alphaViewLogs*/
declare var $: any;
declare var viewModel: any;

var customerSettingController = {
    urls: {
        index: (<HTMLInputElement>document.getElementById('indexPath')).value
    },
    init: function () {
        customerSettingView.initElements(viewModel);

        customerSettingController.viewLogsParams = customerSettingView.getParams();
    },
    viewLogsParams: null,
    viewLogs: function () {
        setVisible('hidden');
        customerSettingView.showLoadingState();

        customerSettingController.viewLogsParams = customerSettingView.getParams();

        var url = customerSettingController.urls.index + "?" + $.param(customerSettingController.viewLogsParams);
        window.location.href = url;
    }
};

$(function () {
    customerSettingController.init();
});
