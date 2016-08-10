/// <reference path="../core.ts" />
/// <reference path="index-viewmanager-settinglogs.ts" />

'use strict';
/*global indexViewManager_SettingLogs, alphaViewLogs*/
declare var $: any;
declare var viewModel: any;

var indexManager = {
    urls: {
        viewLogs: (<HTMLInputElement>document.getElementById('viewLogsPath')).value
    },
    init: function () {
        indexViewManager_SettingLogs.initElements(viewModel);
    },
    viewLogs: function () {
        indexViewManager_SettingLogs.showLoadingState();

        alphaViewLogs.callRequest({
            url: indexManager.urls.viewLogs,
            data: indexViewManager_SettingLogs.getParams(),
            success: function (result) {
                indexViewManager_SettingLogs.renderLogs(result);
                indexViewManager_SettingLogs.showCompleteLoadingState();
            }
        });
    }
};

$(function () {
    indexManager.init();
});
