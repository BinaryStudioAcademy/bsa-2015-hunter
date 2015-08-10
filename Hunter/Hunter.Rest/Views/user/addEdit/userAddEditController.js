(function () {
    "use strict";

    angular
        .module("hunter-app")
        .controller("UserAddEditController", userAddEditController);

    userAddEditController.$inject = [
        "$location",
        "AuthService",
        "UserProfileService",
        "$routeParams"
    ];

    function userAddEditController($location, authService, service, $routeParams) {
        var vm = this;
        vm.editingMode = (typeof $routeParams.id == 'string');
        vm.entityId = 0;
        if (vm.editingMode) {
            vm.entityId = $routeParams.id;
        };
        vm.entity = {}; //{ login: "antonv@bs.com", alias: "AV", position: ".Net Developer", added: "01.12.2013" };

        vm.loadUser = function () {
            service.getUserProfile(vm.entityId, function (response) {
                vm.entity = response.data;
            });
        }

        if (vm.editingMode) {
            vm.loadUser();
        }
    }
})();