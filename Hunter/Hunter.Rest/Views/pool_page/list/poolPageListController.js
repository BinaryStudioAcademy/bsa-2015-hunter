(function () {
    "use strict";

    angular
        .module("hunter-app")
        .controller("PoolPageListController", PoolPageListController);

    PoolPageListController.$inject = [
        "$location",
        "PoolsHttpService",
        "AuthService",
        "HttpHandler",
        '$scope'
    ];

    function PoolPageListController($location,PoolsHttpService, AuthService, HttpHandler, $scope) {
        var vm = this;

        PoolsHttpService.getAllPools().then(function (response) {
            vm.poolsList = response.data;
        });

        //(function() {
        //    HttpHandler.sendRequest({
        //        url: "./api/pool",
        //        verb: "GET",
        //        successCallback: function(result) {
        //            console.log(result.data);
        //            vm.poolsList = result.data;

        //        },
        //        errorCallback: function(result) { console.log(result); }
        //    });
        //})();

    }
})();