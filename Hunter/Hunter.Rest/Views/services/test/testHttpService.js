(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('TestHttpService', TestHttpService);

    TestHttpService.$inject = [
        'HttpHandler',
        '$q'
    ];

    function TestHttpService(httpHandler, $q) {
        var service = {
            getTestsNotChecked: getTestsNotChecked
        }

        function getTestsNotChecked() {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/test/notChecked',
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get tests for check error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        return service;

    }
})();