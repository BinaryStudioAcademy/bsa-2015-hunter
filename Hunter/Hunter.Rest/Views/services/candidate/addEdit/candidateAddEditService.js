(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('CandidateAddEditService', CandidateAddEditService);

    CandidateAddEditService.$inject = [];

    function CandidateAddEditService() {
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
            if (!data.FirstName) {
                errorObject.message += ' First name; ';
                noErrors = false;
            }
            if (!data.LastName) {
                errorObject.message += ' Last name; ';
                noErrors = false;
            }

            if (!data.DateOfBirth) {            
                    errorObject.message += ' Date of birth; ';
                    noErrors = false;
            }

            if (!isNumeric(data.YearsOfExperience) && data.YearsOfExperience != undefined && data.YearsOfExperience != "") {            
                    errorObject.message += ' Years Of Experience; ';
                    noErrors = false;
            }
            if (!isNumeric(data.Salary) && data.Salary != undefined && data.Salary != "") {            
                    errorObject.message += ' Salary; ';
                    noErrors = false;
            }
            var phone = /^\d{3} \d{3} \d{2} \d{2}$/;
            if (!phone.test(data.Phone) && data.Phone != undefined && data.Phone != "") {            
                    errorObject.message += ' Phone; ';
                    noErrors = false;
            }

            var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

            if (!re.test(data.Email) && data.Email != undefined && data.Email != "") {            
                    errorObject.message += ' Email; ';
                    noErrors = false;
            }

            return noErrors;
        }

        return service;
    }
})();