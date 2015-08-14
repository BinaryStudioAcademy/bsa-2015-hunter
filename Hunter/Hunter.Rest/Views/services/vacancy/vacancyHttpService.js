(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('VacancyHttpService', VacancyHttpService);

    VacancyHttpService.$inject = [
        '$q',
        'HttpHandler'
    ];

    function VacancyHttpService($q, httpHandler) {
        var service = {
            getVacancies: getVacancies,
            getFilteredVacancies : getFilteredVacancies,
            getVacancy: getVacancy,
            addVacancy: addVacancy,
            validateVacancy: validateVacancy,
            updateVacancy: updateVacancy,
            deleteVacancy: deleteVacancy,
            getLongList: getLongList,
            getFilterInfo: getFilterInfo
        }

        function getVacancies(){
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/vacancy/all',
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get vacancies error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        function getFilteredVacancies(filter) {
            var requestUrl = '/api/vacancy/?' +
                'page=' + filter.page + '&' +
                'pageSize=' + filter.pageSize + '&' +
                'sortColumn=' + filter.sortColumn + '&' +
                'reverceSort=' + filter.reverceSort + '&' +
                'filter="' + filter.filter + '"&' +
                'pool="' + filter.pool.toString() + '"&' +
                'status="' + filter.status.toString() + '"&' +
                'addedBy="' + filter.addedBy.toString() + '"';
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: requestUrl,
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get vacancies error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        function getVacancy(id) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/vacancy/' + id,
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get vacancy error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        function addVacancy(data) {
            httpHandler.sendRequest({
                verb: 'POST',
                url: '/api/vacancy',
                body: data,
                errorCallback: function (status) {
                    console.log("Add vacancy error");
                    console.log(status);
                }
            });
        }

        function updateVacancy(data, id) {
            httpHandler.sendRequest({
                verb: 'PUT',
                url: '/api/vacancy/' + id,
                body: data,
                errorCallback: function (status) {
                    console.log("Update vacancy error");
                    console.log(status);
                }
            });
        }

        function deleteVacancy(id) {
            httpHandler.sendRequest({
                verb: 'DELETE',
                url: '/api/vacancy/' + id,
                errorCallback: function (status) {
                    console.log("Delete vacancy error");
                    console.log(status);
                }
            });
        }

        function validateVacancy(data) {
            //var noError = true;
            if (!data) {
                return false;
            }
            //if (!data.someField) {
            //    errorObject.someField = true;
            //    noErrors = false;
            //} else {
            //    errorObject.someField = false;
            //}
            //return noErrors;
            return true;
        }

        function getLongList(id)
        {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/vacancy/longlist/' + id,
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get vacancy long list error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        function getFilterInfo(roleName) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/vacancy/filterInfo/' + roleName,
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get filter data error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        return service;
    }
})();