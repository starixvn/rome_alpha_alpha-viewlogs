/// <reference path="../core.ts" />
/// <reference path="loginlogs-viewmanager.ts" />

'use strict';
/*global loginLogsViewManager, alphaViewLogs*/
declare var $: any;
declare var viewModel: any;

var loginLogsManager = {
    urls: {
        index: (<HTMLInputElement>document.getElementById('indexPath')).value
    },
    init: function () {
        loginLogsViewManager.initElements(viewModel);

        loginLogsManager.viewLogsParams = loginLogsViewManager.getParams();
    },
    viewLogsParams: null,
    viewLogs: function () {
        setVisible('hidden');
        loginLogsViewManager.showLoadingState();

        loginLogsManager.viewLogsParams = loginLogsViewManager.getParams();

        var url = loginLogsManager.urls.index + "?" + $.param(loginLogsManager.viewLogsParams);
        window.location.href = url;
    }
};

$(function () {
    loginLogsManager.init();
});
