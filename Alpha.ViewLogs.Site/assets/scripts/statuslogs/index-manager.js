/// <reference path="../core.ts" />
/// <reference path="index-viewmanager-statuslogs.ts" />
'use strict';
var indexManager = {
    urls: {
        index: document.getElementById('indexPath').value
    },
    init: function () {
        indexViewManager_StatusLogs.initElements(viewModel);
        indexManager.viewLogsParams = indexViewManager_StatusLogs.getParams();
    },
    viewLogsParams: null,
    viewLogs: function () {
        setVisible('hidden');
        indexViewManager_StatusLogs.showLoadingState();
        indexManager.viewLogsParams = indexViewManager_StatusLogs.getParams();
        var url = indexManager.urls.index + "?" + $.param(indexManager.viewLogsParams);
        window.location.href = url;
    }
};
$(function () {
    indexManager.init();
});
//# sourceMappingURL=index-manager.js.map