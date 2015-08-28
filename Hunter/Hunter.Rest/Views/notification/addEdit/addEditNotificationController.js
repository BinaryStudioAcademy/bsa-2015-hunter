(function () {
    "use strict";

    angular
        .module("hunter-app")
        .controller("AddEditNotificationController", AddEditNotificationController);

    AddEditNotificationController.$inject = [
        "$location",
        "AuthService",
        "NotificationHttpService",
        '$routeParams'
    ];

    function AddEditNotificationController($location, authService, notificationHttpService, $routeParams) {
        var vm = this;
        vm.controllerName = 'Add / Edit notification';
        vm.addNotification = addNotification;
        vm.newNotification = {
            id: 0,
            candidateId: $routeParams.cid,
            pending: new Date(),
            message: 'Call to candidate',
            isSent: false,
            isShown: false,
            userLogin: authService.login
        };

        function addNotification() {
            notificationHttpService.addNotification(vm.newNotification);
        };
    };
})();