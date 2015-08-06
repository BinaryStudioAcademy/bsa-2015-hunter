(function() {
    angular.module('hunter-app')
        .factory('PoolsHttpService', PoolsHttpService);
    
    PoolsHttpService.$inject = [
    'HttpHandler'
    ];
    function PoolsHttpService(httpHandler) {
        var service = {
            getAllPools : getAllPools
        }

        function getAllPools(successCallback,body) {
            httpHandler.sendRequest({
                verb: 'GET',
                url: '/api/pool',
                body: body,
                successCallback: successCallback,
            });
        }

        return service;
    }
})();