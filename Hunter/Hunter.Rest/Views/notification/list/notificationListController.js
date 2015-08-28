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
        vm.isModad = false;
        vm.deleteNotification = deleteNotification;

        vm.notifications = null;
        loadNotifications();

        function loadNotifications() {
            notificationHttpService.getNotifications().then(function(result) {
                vm.notifications = result;
            });
        }

        function deleteNotification(index) {
            if (confirm('Delete notification?')) {
                if (vm.notifications == null) return;
                if (index > -1) {
                    var id = vm.notifications[index].id;
                    vm.notifications.splice(index, 1);
                    notificationHttpService.deleteNotification(id);
                }
            }
            //alertify.confirm('Delete notification?', function () {
            //    if (vm.notifications == null) return;
            //    if (index > -1) {
            //        var id = vm.notifications[index].id;
            //        vm.notifications.splice(index, 1);
            //        notificationHttpService.deleteNotification(id);
            //    }
            //});
        }
    }
})();