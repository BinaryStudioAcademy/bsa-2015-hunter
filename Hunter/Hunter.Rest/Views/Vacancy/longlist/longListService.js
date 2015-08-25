(function () {
    'use strict';

    angular.module('hunter-app')
        .factory('LonglistService', LonglistService);

    LonglistService.$inject = [
        'HttpHandler',
        '$q',
	    '$location'
    ];

    function LonglistService(httpHandler, $q, $location) {
        var service = {
            changeTemplate: changeTemplate,
            
        }

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

        return service;
    }
})();