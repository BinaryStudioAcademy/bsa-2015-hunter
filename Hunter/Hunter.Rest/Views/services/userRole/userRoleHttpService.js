(function() {
    'use strict';

    angular
        .module('hunter-app')
        .factory('UserRoleHttpService', UserRoleHttpService);

    UserRoleHttpService.$inject = [
        '$q',
        'HttpHandler'
    ];

    function UserRoleHttpService($q, HttpHandler) {
        var service = {
            getUserRoles: getUserRoles
        }

        function getUserRoles() {
            var deferred = $q.defer();
            HttpHandler.sendRequest({
                url: './api/userRole',
                verb: 'get',
                successCallback: function (result) {
                    deferred.resolve(result);
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
} )();