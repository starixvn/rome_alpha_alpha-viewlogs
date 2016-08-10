/// <reference path="../core.ts" />
/// <reference path="index-viewmanager-statuslogs.ts" />

'use strict';
/*global indexViewManager_StatusLogs, alphaViewLogs*/
declare var $: any;
declare var viewModel: any;

var indexManager = {
    urls: {
        viewLogs: (<HTMLInputElement>document.getElementById('viewLogsPath')).value
    },
    init: function () {
        indexViewManager_StatusLogs.initElements(viewModel);

        indexManager.viewLogsParams = indexViewManager_StatusLogs.getParams();
    },
    viewLogsParams: null,
    viewLogs: function () {
        indexViewManager_StatusLogs.showLoadingState();

        indexManager.viewLogsParams = indexViewManager_StatusLogs.getParams();

        alphaViewLogs.callRequest({
            url: indexManager.urls.viewLogs,
            data: indexManager.viewLogsParams,
            success: function (result) {
                indexViewManager_StatusLogs.renderLogs(result);
                indexViewManager_StatusLogs.showCompleteLoadingState();
            }
        });
    }
};

$(function () {
    indexManager.init();
});
