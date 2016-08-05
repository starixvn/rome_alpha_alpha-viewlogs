/// <reference path="index-viewmanager.ts" />

'use strict';
/*global indexViewManager*/
declare var $: any;
declare var viewModel: any;

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
