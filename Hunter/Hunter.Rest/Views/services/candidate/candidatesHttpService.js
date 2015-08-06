(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('CandidateHttpService', CandidateHttpService);

    CandidateHttpService.$inject = [
        'HttpHandler'
    ];

    function CandidateHttpService(httpHandler) {
        var service = {
            updateCandidate: updateCandidate,
            addCandidate: addCandidate,
            getById: getById
        };

        function getById(successCallback, id) {
            httpHandler.sendRequest({
                verb: 'GET',
                url: '/api/candidates/' + id,
                successCallback: successCallback
            });
        }

        function updateCandidate(body, successCallback, id) {
            httpHandler.sendRequest({
                verb: 'PUT',
                url: '/api/candidates/'+id,
                body: body,
                successCallback: successCallback
            });
        }

        function addCandidate(body, successCallback) {
            httpHandler.sendRequest({
                verb: 'POST',
                url: 'api/candidates/',
                body: body,
                successCallback: successCallback
            });
        }

        return service;
    }
})();