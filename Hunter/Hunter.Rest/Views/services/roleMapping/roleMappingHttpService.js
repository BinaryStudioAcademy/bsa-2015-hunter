(function() {
    angular.module('hunter-app')
        .factory('RoleMappingHttpService', RoleMappingHttpService);
    
    RoleMappingHttpService.$inject = [
    'HttpHandler',
    '$q'
    ];
    function RoleMappingHttpService(httpHandler, $q) {
        var service = {
            getAllRoleMapping: getAllRoleMapping,
            updatePool: updatePool
        }

        function getAllRoleMapping() {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                verb: 'GET',
                url: './api/roleMapping',
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

        
        function updatePool(body) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                verb: 'PUT',
                url: './api/roleMapping',
                body: body,
                successMessageToUser: 'Permission Updated.',
                successCallback: function(result) {
                    deferred.resolve(result);
                },
                errorCallback: function(status) {
                    alertify.error("Permission doesn't Updated")
                }
            });
            return deferred.promise;
        }

        
        return service;
    }
})();