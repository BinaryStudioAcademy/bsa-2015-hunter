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
            getCandidate: getCandidate,
            getCandidateList: getCandidateList
            addCandidate: addCandidate,
        };

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

        function getCandidate(id, successCallback) {
            httpHandler.sendRequest({
                verb: 'GET',
                url: '/api/Candidates/' + id,
//                body: body,
                successCallback: successCallback,
                errorMessageToDev: 'GET CANDIDATE INFO ERROR: ',
                errorMessageToUser: 'Failed'
            });
        }

        function getCandidateList(successCallback) {
            httpHandler.sendRequest({
                verb: 'GET',
                url: '/api/Candidates/',
//                body: body,
                successCallback: successCallback,
                errorMessageToDev: 'GET CANDIDATE INFO ERROR: ',
                errorMessageToUser: 'Failed'
            });
        }

        return service;
    }
})();