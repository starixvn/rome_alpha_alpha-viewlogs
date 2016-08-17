/// <reference path="../core.ts" />
/// <reference path="index-view.ts" />

'use strict';
/*global loginView, alphaViewLogs*/
declare var $: any;
declare var viewModel: any;

var loginController = {
    urls: {
        index: (<HTMLInputElement>document.getElementById('indexPath')).value
    },
    init: function () {
        loginView.initElements(viewModel);

        loginController.viewLogsParams = loginView.getParams();
    },
    viewLogsParams: null,
    viewLogs: function () {
        setVisible('hidden');
        loginView.showLoadingState();

        loginController.viewLogsParams = loginView.getParams();

        var url = loginController.urls.index + "?" + $.param(loginController.viewLogsParams);
        window.location.href = url;
    }
};

$(function () {
    loginController.init();
});
