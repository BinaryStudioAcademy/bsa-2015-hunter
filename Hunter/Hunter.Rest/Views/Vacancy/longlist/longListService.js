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
            changeTemplate: changeTemplate
            
        }

        function changeTemplate(templateRoute) {
            switch (templateRoute.toLowerCase()) {
                case 'overview':
                    return viewsPath + 'cardOverview.html';
                case 'specialnotes':
                    return viewsPath + 'cardSpecialNotes.html';
                case 'appresults':
                    return viewsPath + 'applicationResults.html';

                default:
                    return '';
            }
        }
        
        return service;
    }
})();