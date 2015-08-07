(function () {
    "use strict";

    angular
        .module("hunter-app")
        .controller("PoolListController", PoolListController);

    PoolListController.$inject = [
        "$location",
        "AuthService",
        "HttpHandler"
    ];

    function PoolListController($location, AuthService, HttpHandler) {
        var vm = this;
        
        HttpHandler.sendRequest({
                    url: '/api/pool',
                    verb: 'GET',
                    successCallback: function(result) {
                        //console.log(result.data);
                        vm.poolsList = result.data;
                    },
                    errorCallback: function (result) { console.log(result); }
                });
    }
})();