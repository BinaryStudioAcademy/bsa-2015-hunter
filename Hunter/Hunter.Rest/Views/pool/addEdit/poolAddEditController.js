(function () {
    "use strict";

    angular
        .module("hunter-app")
        .controller("PoolAddEditController", PoolAddEditController)
        .service("PoolAddEditService", PoolAddEditService);

    PoolAddEditController.$inject = [
        "$location",
        "AuthService",
        "HttpHandler",
        "$routeParams",
        "PoolAddEditService"
    ];

    function PoolAddEditController($location, AuthService, HttpHandler, $routeParams, PoolAddEditService) {
        var vm = this;
        vm.pageConfig = {};
        vm.id = 0;
        vm.pool = {};
        vm.poolColors = {};
        vm.poolUrl = "";
        vm.addEditFlag = $routeParams.id;

        PoolAddEditService(addEditFlag);
    }

    function PoolAddEditService(addEditFlag) {
        //edit - put
        if (addEditFlag > 0) {
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
                    vm.poolColors = result.data.poolbackground;
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
                        $location.url("/pool");
                    },
                    errorCallback: function (result) { console.log(result); }
                });
            };

            vm.poolDelete = function () {
                HttpHandler.sendRequest({
                    url: vm.poolUrl,
                    verb: "DELETE",
                    successCallback: function (result) {
                        //console.log(result);
                        $location.url("/pool");
                    },
                    errorCallback: function (result) { console.log(result); }
                });
            };
        }
        // add - post
        else {
            vm.pageConfig.deleteButton = false;
            vm.pageConfig.pageTitle = "Add new pool";
            vm.pageConfig.postPutButtonValue = "Add Pool";

            vm.poolUrl = "/api/pool/";

            HttpHandler.sendRequest({
                url: "/api/pool",
                verb: "GET",
                successCallback: function (result) {
                    console.log(result.data);
                    vm.poolColors = result.data[0].poolbackground;
                },
                errorCallback: function (result) { console.log(result); }
            });

            vm.poolPostPut = function () {
                HttpHandler.sendRequest({
                    url: vm.poolUrl,
                    verb: "POST",
                    body: JSON.stringify(vm.pool),
                    successCallback: function (result) {
                        console.log(result);
                        $location.url('/pool');
                    },
                    errorCallback: function (result) { console.log(result); }
                });
            }
        }

        vm.setColor = function ($event, code) {
            vm.pool.color = code;

            vm.currentChild = $event.currentTarget;
            vm.parent = $event.currentTarget.parentNode;
            
            for (var i = 0; i < vm.parent.childNodes.length - 1; i++) {
                vm.parent.childNodes[i].className = "pool_colors_inactive";
            }
            
            vm.currentChild.className = "pool_colors_active";
        }
    }
})();