(function () {
    "use strict";

    angular
        .module("hunter-app")
        .controller("NotificationListController", NotificationListController);

    NotificationListController.$inject = [
        "$location",
        "AuthService",
        "NotificationHttpService"
    ];

    function NotificationListController($location, authService, notificationHttpService) {
        var vm = this;
        vm.controllerName = 'Notification list';
    }
})();