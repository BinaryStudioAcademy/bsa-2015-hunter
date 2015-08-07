(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('VacancyAddEditService', VacancyAddEditService);

    AddEditService.$inject = [
        'HttpHandler'
    ];

    function AddEditService(httpHandler) {
        var service = {
            validateData: validateData,
            addData: addData
        };

        function validateData(data, errorObject) {
            var noError = true;
            if (!data) {
                return false;
            }

            if (!data.someField) {
                errorObject.someField = true;
                noErrors = false;
            } else {
                errorObject.someField = false;
            }

            return noErrors;
        }

        function addData(data) {
            httpHandler.sendRequest({
                verb: 'POST',
                url: '/api/vacancy',
                body: data
            });
        }

        return service;
    }
})();