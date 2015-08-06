(function() {
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
        };

        function updateCandidate(body, successCallback) {
            httpHandler.sendRequest({
                verb: 'GET',
                url: '/api/candidate/',
                body: body,
                successCallback: successCallback,
                errorMessageToDev: 'UPDATE CANDIDATE INFO ERROR: ',
                errorMessageToUser: 'Update Failed'
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