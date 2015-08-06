﻿(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('AddEditService', AddEditService);

    AddEditService.$inject = [];

    function AddEditService() {
        var serivce = {
            validateData: validateData
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

        return service;
    }
})();