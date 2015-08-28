(function () {
    "use strict";

    angular
        .module("hunter-app")
        .controller("AddEditNotificationController", AddEditNotificationController);

    AddEditNotificationController.$inject = [
        "$location",
        "AuthService",
        "NotificationHttpService"
    ];

    function AddEditNotificationController($location, authService, notificationHttpService) {
        var vm = this;
        vm.controllerName = 'Add / Edit notification';
    }
})();