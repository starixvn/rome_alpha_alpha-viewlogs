/// <reference path="index-viewmanager.ts" />
'use strict';
var indexManager = {
    init: function () {
        indexViewManager.initElements(viewModel);
    },
    viewLogs: function () {
        indexViewManager.showLoadingState();
        indexViewManager.showCompleteLoadingState();
    }
};
$(function () {
    indexManager.init();
});
//# sourceMappingURL=index-manager.js.map