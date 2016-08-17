/// <reference path="../core.ts" />
/// <reference path="index-view.ts" />

'use strict';
/*global statusView, alphaViewLogs*/
declare var $: any;
declare var viewModel: any;

var statusController = {
    urls: {
        index: (<HTMLInputElement>document.getElementById('indexPath')).value
    },
    init: function () {
        statusView.initElements(viewModel);

        statusController.viewLogsParams = statusView.getParams();
    },
    viewLogsParams: null,
    viewLogs: function () {
        setVisible('hidden');
        statusView.showLoadingState();

        statusController.viewLogsParams = statusView.getParams();

        var url = statusController.urls.index + "?" + $.param(statusController.viewLogsParams);
        window.location.href = url;
    }
};

$(function () {
    statusController.init();
});
