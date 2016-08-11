/// <reference path="../core.ts" />
/// <reference path="creditlogs-viewmanager.ts" />

'use strict';
/*global creditLogsViewManager, alphaViewLogs*/
declare var $: any;
declare var viewModel: any;

var creditLogsManager = {
    urls: {
        index: (<HTMLInputElement>document.getElementById('indexPath')).value
    },
    init: function () {
        creditLogsViewManager.initElements(viewModel);

        creditLogsManager.viewLogsParams = creditLogsViewManager.getParams();
    },
    viewLogsParams: null,
    viewLogs: function () {
        setVisible('hidden');
        creditLogsViewManager.showLoadingState();

        creditLogsManager.viewLogsParams = creditLogsViewManager.getParams();

        var url = creditLogsManager.urls.index + "?" + $.param(creditLogsManager.viewLogsParams);
        window.location.href = url;
    }
};

$(function () {
    creditLogsManager.init();
});
