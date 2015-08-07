(function () {
    "use strict";

    angular
        .module("hunter-app")
        .controller("PoolAddEditController", PoolAddEditController);

    PoolAddEditController.$inject = [
        "$location",
        "AuthService",
        "HttpHandler",
        "$routeParams",
    ];

    function PoolAddEditController($location, AuthService, HttpHandler, $routeParams) {
        var vm = this;
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

        vm.PoolEdit = function () {
            //console.log("bingo" + vm.poolUrl + " " + vm.pool.name);
            HttpHandler.sendRequest({
                url: vm.poolUrl,
                verb: "PUT",
                body: JSON.stringify(vm.pool),
                //body: { "id": 1, "name": "test1" }
                successCallback: function (result) {
                    console.log(result);
                    //vm.pool = result.data;
                },
                errorCallback: function (result) { console.log(result); }
            });

            // vm.RedirectToPool();
        };

        //vm.PoolEdit();

        vm.RedirectToPool = function() {
            $location.path("http://localhost:53147/#/pool");
        }

        
    }


})();