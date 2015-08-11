(function() {
    'use strict';

    angular
        .module('hunter-app')
        .factory('IndexHttpService', IndexHttpService);

    IndexHttpService.$inject = [
        'HttpHandler'
    ];

    function IndexHttpService(HttpHandler) {
        var service = {
            'getActivityAmount': getActivityAmount
        };

        function getActivityAmount(successCallback) {
            HttpHandler.sendRequest({
                'verb': 'GET',
                'url': '/api/activities/amount',
                'successCallback': successCallback,
                'errorMessageToDev': 'GET ACTIVITY INFO ERROR: ',
                'errorMessageToUser': 'Failed'
            });
        }

        return service;
    }

})();