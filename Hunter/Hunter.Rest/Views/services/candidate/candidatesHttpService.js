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
            updateCandidate: updateCandidate
        };

        function updateCandidate(body, successCallback) {
            httpHandler({
                type: 'GET',
                url: '/api/candidate/',
                body: body,
                successCallback: successCallback,
                errorMessageToDev: 'UPDATE CANDIDATE INFO ERROR: ',
                errorMessageToUser: 'Update Failed'
            });
        }

        return service;
    }
})();