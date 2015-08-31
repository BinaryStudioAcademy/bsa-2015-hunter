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
        'EnumConstants',
        '$scope'
    ];

    function NotificationListController($location, authService, notificationHttpService, $routeParams, enumConstants, $scope) {
        var vm = this;
        vm.controllerName = 'Notification list';
        vm.notificationTypes = enumConstants.notificationTypes;
        vm.filter = notificationHttpService.convertRouteParamsToFilter($routeParams);
        vm.isModal = false;
        vm.deleteNotification = deleteNotification;
        vm.notify = notify;
        vm.updateData = updateData;
        vm.showSpinner = false;
        var candidateId = $routeParams["cid"];
        if (candidateId) {
            vm.isModal = true;
        }
        vm.notifications = null;
        loadNotifications();

        function loadNotifications(candidateId) {
            if (candidateId == undefined) {
                var filter = notificationHttpService.convertRouteParamsToFilter($routeParams);
                notificationHttpService.getNotificationsByFilter(filter).then(function (result) {
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

        function notify()
        {
            notificationHttpService.notify();
        }

        function updateData() {
            $location.search(vm.filter);
        }

        $scope.$on('$routeUpdate', function () {
            vm.filter = notificationHttpService.convertRouteParamsToFilter($routeParams);
            notificationHttpService.getNotificationsByFilter(vm.filter).then(function (result) {
                vm.notifications = result;
            });
        });
    }
})();