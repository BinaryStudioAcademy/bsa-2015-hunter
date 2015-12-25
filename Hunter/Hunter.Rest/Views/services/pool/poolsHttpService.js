(function() {
    angular.module('hunter-app')
        .factory('PoolsHttpService', PoolsHttpService);
    
    PoolsHttpService.$inject = [
    'HttpHandler',
    '$q'
    ];
    function PoolsHttpService(httpHandler, $q) {
        var service = {
            getAllPools: getAllPools,
            getPool: getPool,
            updatePool: updatePool,
            addPool: addPool,
            removePool: removePool

        }

        function getAllPools() {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                verb: 'GET',
                url: './api/pool',
                successCallback: function(result) {
                    deferred.resolve(result);
                },
                errorCallback:function(status) {
                    console.log("get pools error ");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        function getPool(id) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                verb: 'GET',
                url: './api/pool/' + id,
                //                body: body,
                successCallback: function (response) {
                    deferred.resolve(response);
                },
                errorCallback: function (status) {
                    console.log("getting pool error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        function updatePool(body, successCallback, id) {
            httpHandler.sendRequest({
                verb: 'PUT',
                url: './api/pool/' + id,
                body: body,
                successCallback: successCallback
            });
        }

        function addPool(body, successCallback) {
            httpHandler.sendRequest({
                verb: 'POST',
                url: './api/pool/',
                body: body,
                successCallback: successCallback
            });
        }

        function removePool(id, successCallback) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                verb: 'DELETE',
                url: './api/pool/' + id,
                //                body: body,
                successCallback: successCallback,
                errorCallback: function (status) {
                    console.log("delete pool error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        return service;
    }
})();