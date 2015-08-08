(function () {
    "use strict";

    angular
        .module("hunter-app")
        .controller("PoolAddEditController", PoolAddEditController);

    PoolAddEditController.$inject = [
        "$location",
        "AuthService",
        "HttpHandler",
        "$routeParams"
    ];

    function PoolAddEditController($location, AuthService, HttpHandler, $routeParams) {
        var vm = this;
        vm.pageConfig = {};

        if ($routeParams.id > 0) {
            vm.pageConfig.deleteButton = true;
            vm.pageConfig.pageTitle = "Edit a pool";
            vm.pageConfig.postPutButtonValue = "Edit Pool";

            vm.poolUrl = "/api/pool/" + $routeParams.id;
            HttpHandler.sendRequest({
                url: vm.poolUrl,
                verb: "GET",
                successCallback: function (result) {
                    console.log(result.data);
                    vm.pool = result.data;
                },
                errorCallback: function (result) { console.log(result); }
            });

            vm.poolPostPut = function () {
                HttpHandler.sendRequest({
                    url: vm.poolUrl,
                    verb: "PUT",
                    body: JSON.stringify(vm.pool),
                    successCallback: function (result) {
                        //console.log(result);
                    },
                    errorCallback: function (result) { console.log(result); }
                });

                $location.url("/pool");
            };

            vm.poolDelete = function () {
                HttpHandler.sendRequest({
                    url: vm.poolUrl,
                    verb: "DELETE",
                    successCallback: function (result) {
                        //console.log(result);
                    },
                    errorCallback: function (result) { console.log(result); }
                });

                $location.url("/pool");
            };
        }
        else {
            vm.pageConfig.deleteButton = false;
            vm.pageConfig.pageTitle = "Add new pool";
            vm.pageConfig.postPutButtonValue = "Add Pool";

            vm.poolPostPut = function () {
                vm.poolUrl = "/api/pool/";
                HttpHandler.sendRequest({
                    url: vm.poolUrl,
                    verb: "POST",
                    body: JSON.stringify(vm.pool),
                    successCallback: function (result) {
                        //console.log(result);
                    },
                    errorCallback: function (result) { console.log(result); }
                });

                //vm.RedirectToPool();
                $location.url("/pool");
            }
        }
    }
})();