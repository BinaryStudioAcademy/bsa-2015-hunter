(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('VacancyHttpService', VacancyHttpService);

    VacancyHttpService.$inject = [
        '$q',
        'HttpHandler'
    ];

    function VacancyHttpService($q,HttpHandler) {
        var service = {
            getVacancies: getVacancies
        }

        function getVacancies(){
            var deferred = $q.defer();
            HttpHandler.sendRequest({
                url: '/api/vacancy',
                verb: 'get',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("error");
                    console.log(status);
                }
            });
            return deferred.promise;
            
        }

        return service;
    }
})();