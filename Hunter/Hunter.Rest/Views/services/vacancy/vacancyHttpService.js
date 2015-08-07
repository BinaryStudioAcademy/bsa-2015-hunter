(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('VacancyHttpService', VacancyHttpService);

    VacancyHttpService.$inject = [
        'HttpHandler'
    ];

    function VacancyHttpService(httpHandler) {
        var service = {
            updateVacancy: updateVacancy
        };

        function updateVacancy(body, successCallback) {
            httpHandler.sendRequest({
                type: 'GET',
                url: '/api/vacancy/',
                body: body,
                successCallback: successCallback,
                errorMessageToDev: 'UPDATE VACANCY INFO ERROR: ',
                errorMessageToUser: 'Update Failed'
            });
        }

        return service;
    }
})();