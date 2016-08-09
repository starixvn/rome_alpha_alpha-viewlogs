/// <reference path="../core.ts" />
/// <reference path="index-viewmanager.ts" />
'use strict';
var indexManager = {
    init: function () {
        indexViewManager.initElements(viewModel);
    },
    viewLogs: function () {
        indexViewManager.showLoadingState();
        alphaViewLogs.callRequest({
            url: alphaViewLogs.urls.viewCustomerSettingLogs,
            data: indexViewManager.getParams(),
            success: function () {
                indexViewManager.showCompleteLoadingState();
            }
        });
    }
};
$(function () {
    indexManager.init();
});
//# sourceMappingURL=index-manager.js.map