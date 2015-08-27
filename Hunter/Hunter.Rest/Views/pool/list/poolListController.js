(function () {
    "use strict";

    angular
        .module("hunter-app")
        .controller("PoolListController", PoolListController);

    PoolListController.$inject = [
        "$location",
        "AuthService",
        "HttpHandler",
        '$scope'
    ];

    function PoolListController($location, AuthService, HttpHandler, $scope) {
        var vm = this;

        vm.editPool = editPool;
        vm.choosePool = choosePool;

        (function() {
            HttpHandler.sendRequest({
                url: "/api/pool",
                verb: "GET",
                successCallback: function(result) {
                    console.log(result.data);
                    vm.poolsList = result.data;
                },
                errorCallback: function(result) { console.log(result); }
            });
        })();

        function editPool(pool) {
            $scope.generalCtrl.link = 'Views/pool/addEdit/addEdit.html';
            $scope.generalCtrl.selectedPool = pool;
        }

        function choosePool(pool) {
            $scope.generalCtrl.selectPool(pool);
        }
    }
})();