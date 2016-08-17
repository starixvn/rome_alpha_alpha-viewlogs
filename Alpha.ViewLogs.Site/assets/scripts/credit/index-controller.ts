/// <reference path="../core.ts" />
/// <reference path="index-view.ts" />

'use strict';
/*global creditView, alphaViewLogs*/
declare var $: any;
declare var viewModel: any;

var creditController = {
    urls: {
        index: (<HTMLInputElement>document.getElementById('indexPath')).value
    },
    init: function () {
        creditView.initElements(viewModel);

        creditController.viewLogsParams = creditView.getParams();
    },
    viewLogsParams: null,
    viewLogs: function () {
        setVisible('hidden');
        creditView.showLoadingState();

        creditController.viewLogsParams = creditView.getParams();

        var url = creditController.urls.index + "?" + $.param(creditController.viewLogsParams);
        window.location.href = url;
    }
};

$(function () {
    creditController.init();
});
