(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('IndexController', IndexController);

    IndexController.$inject = [
        '$scope',
        'IndexHttpService',
        '$interval',
        'NotificationHttpService',
        '$location'
    ];

    function IndexController($scope, indexHttpService, $interval, notificationHttpService, $location) {
        var vm = this;
        vm.name = "Index";
        vm.amount = 0;
        $scope.radioModel = 'Home';
        vm.notifications = null;

        callRefreshFunctions();

        $interval(callRefreshFunctions, 180000);   //3 minutes

        function callRefreshFunctions() {
            getActivityAmount();
            getActiveNotifications();
        }

        function getActivityAmount() {
            indexHttpService.getActivityAmount(function (response) {
                vm.amount = response.data;
            });
        }

        function getActiveNotifications() {
            console.log('getActiveNotifications');
            notificationHttpService.getActiveNotifications().then(function (result) {
                vm.notifications = result;
                if (vm.notifications != null) {
                    vm.notifications.forEach(function (notification, i, notifications) {
                        alertNotification(notification);
                    });
                }
            });
        }

        function alertNotification(notification) {
            var alertMessage = notification.pending + ' ' + notification.message + '<a href="#/candidate/' + notification.candidateId + '"></a>';

            alertify.success('Click me to show a notification!', 6000, function (isClicked) {
                if (isClicked) {
                    notificationHttpService.notificationShown(notification.id);
                    alertify.alert(alertMessage);
                    $location.url('/candidate/' + notification.candidateId);
                }
            });
        }
    }
})();