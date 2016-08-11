/// <reference path="../core.ts" />
/// <reference path="index-viewmanager-settinglogs.ts" />

'use strict';
/*global indexViewManager_SettingLogs, alphaViewLogs*/
declare var $: any;
declare var viewModel: any;

var indexManager = {
    urls: {
        index: (<HTMLInputElement>document.getElementById('indexPath')).value
    },
    init: function () {
        indexViewManager_SettingLogs.initElements(viewModel);

        indexManager.viewLogsParams = indexViewManager_SettingLogs.getParams();
    },
    viewLogsParams: null,
    viewLogs: function () {
        setVisible('hidden');
        indexViewManager_SettingLogs.showLoadingState();

        indexManager.viewLogsParams = indexViewManager_SettingLogs.getParams();

        var url = indexManager.urls.index + "?" + $.param(indexManager.viewLogsParams);
        window.location.href = url;
    }
};

$(function () {
    indexManager.init();
});
