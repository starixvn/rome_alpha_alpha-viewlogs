/// <reference path="../core.ts" />
/// <reference path="index-viewmanager.ts" />

'use strict';
/*global indexViewManager, alphaViewLogs*/
declare var $: any;
declare var viewModel: any;

var indexManager = {
    init: function () {
        indexViewManager.initElements(viewModel);
    },
    viewLogs: function () {
        indexViewManager.showLoadingState();

        alphaViewLogs.callRequest({
            url: alphaViewLogs.urls.viewCustomerSettingLogs,
            data: indexViewManager.getParams(),
            success: function (result) {
                indexViewManager.renderLogs(result);
                indexViewManager.showCompleteLoadingState();
            }
        });
    }
};

$(function () {
    indexManager.init();
});
