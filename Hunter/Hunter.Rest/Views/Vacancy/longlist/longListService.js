(function () {
    'use strict';

    angular.module('hunter-app')
        .factory('LonglistService', LonglistService);

    LonglistService.$inject = [
        'HttpHandler',
        '$q',
	    '$location',
        '$rootScope'
    ];

    function LonglistService(httpHandler, $q, $location, $rootScope) {
        var service = {
            changeTemplate: changeTemplate,
            isCurrentTabEmpty: isCurrentTabEmpty
        }

        var vm = this;
        // click on candidate item shows candidates preview

        var viewsPath = '/Views/vacancy/longlist/';

        function changeTemplate(templateRoute) {
            switch (templateRoute.toLowerCase()) {
                case 'overview':
                    return viewsPath + 'previewOverview.html';
                case 'specialnotes':
                    return viewsPath + 'previewSpecialNotes.html';
                case 'appresults':
                    return viewsPath + 'previewAppResults.html';

                default:
                    return '';
            }
        }

        function isCurrentTabEmpty(route, overviews, notes) {
            switch (route.toLowerCase()) {
                case 'overview':
                    return overviews.length > 0 ? true : false;
                case 'specialnotes':
                    return notes.length > 0 ? true : false;
                case 'appresults':
                    return true;               
            default:
                return false;
            }
        }

        return service;
    }
})();