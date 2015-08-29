(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('NotificationListController', NotificationListController);

    NotificationListController.$inject = [
        '$location',
        'AuthService',
        'NotificationHttpService',
        '$routeParams',
        'EnumConstants'
    ];

    function NotificationListController($location, authService, notificationHttpService, $routeParams, enumConstants) {
        var vm = this;
        vm.controllerName = 'Notification list';
        vm.notificationTypes = enumConstants.notificationTypes;
        vm.isModal = false;
        vm.deleteNotification = deleteNotification;
        var candidateId = $routeParams["cid"];
        if (candidateId) {
            vm.isModal = true;
        }

        vm.notifications = null;
        loadNotifications(candidateId);

        function loadNotifications(candidateId) {
            if (candidateId == undefined) {
                notificationHttpService.getNotifications().then(function(result) {
                    vm.notifications = result;
                });
            } else {
                notificationHttpService.getCandidateNotifications(candidateId).then(function (result) {
                    vm.notifications = result;
                });
            }
        }

        function deleteNotification(index) {
            alertify.confirm('Delete notification?', function () {
                if (vm.notifications == null) return;
                if (index > -1) {
                    var id = vm.notifications[index].id;
                    vm.notifications.splice(index, 1);
                    notificationHttpService.deleteNotification(id);
                }
            });
        }
    }
})();