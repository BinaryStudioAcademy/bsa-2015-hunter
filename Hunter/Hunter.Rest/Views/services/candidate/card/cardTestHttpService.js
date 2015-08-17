(function() {
    'use strict';

    angular
        .module('hunter-app')
        .factory('CardTestHttpService', CardTestHttpService);

    CardTestHttpService.$inject = [
        'HttpHandler'
    ];

    function CardTestHttpService(HttpHandler) {
        var service = {
            'getTest': getTest,
            'sendTest': uploadTest
        };

        function getTest(vacancyId, candidateId, success) {
            HttpHandler.sendRequest({
                'verb': 'GET',
                'url': 'api/test?vacancyId=' + vacancyId + '&candidateId=' + candidateId,
                'successCallback': success,
                'errorMessageToDev': 'GET TEST ERROR',
                'errorMessageToUser': 'Failed'
            });
        }

        function uploadTest(body, success) {
            HttpHandler.sendRequest({
                'verb': 'POST',
                'url': '/api/test/add',
                'body': body,
                'successCallback': success,
                'errorMessageToDev': 'POST TEST ERROR',
                'errorMessageToUser': 'uploading failed'
            });
        }

        return service;
    }

})();