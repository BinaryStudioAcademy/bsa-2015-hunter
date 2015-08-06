(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('AddEditService', AddEditService);

    AddEditService.$inject = [];

    function AddEditService() {
        var service = {
            validateData: validateData
        };

        function validateData(data, errorObject) {
            var noErrors = true;
            if (!data) {
                return false;
            }

//            if (!data.someField) {
//                errorObject.someField = true;
//                noErrors = false;
//            } else {
//                errorObject.someField = false;
//            }

            return noErrors;
        }

        return service;
    }
})();