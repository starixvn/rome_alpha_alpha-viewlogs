/// <reference path="../core.ts" />
/// <reference path="index-viewmanager-settinglogs.ts" />
'use strict';
var indexManager = {
    urls: {
        viewLogs: document.getElementById('viewLogsPath').value
    },
    init: function () {
        indexViewManager_SettingLogs.initElements(viewModel);
        indexManager.viewLogsParams = indexViewManager_SettingLogs.getParams();
    },
    viewLogsParams: null,
    viewLogs: function () {
        indexViewManager_SettingLogs.showLoadingState();
        indexManager.viewLogsParams = indexViewManager_SettingLogs.getParams();
        alphaViewLogs.callRequest({
            url: indexManager.urls.viewLogs,
            data: indexManager.viewLogsParams,
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