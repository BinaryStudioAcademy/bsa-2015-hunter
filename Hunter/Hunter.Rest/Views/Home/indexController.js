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
        vm.clickedNotification = null;

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
            notificationHttpService.getActiveNotifications().then(function (result) {
                vm.notifications = result;
                if (vm.notifications != null) {
                    for (var i = 0; i < vm.notifications.length; i++) {
                        alertNotification(i);
                    }
                }
            });
        }

        function alertNotification(index) {
            vm.clickedNotification = vm.notifications[index];
            var alertMessage = vm.clickedNotification.pending + ' ' + vm.clickedNotification.message + '<a href="#/candidate/' + vm.clickedNotification.candidateId + '"></a>';

            alertify.success('Click me to show a notification!', 6000, function (isClicked) {
                if (isClicked) {
                    console.log(vm.clickedNotification);
                    notificationHttpService.notificationShown(vm.clickedNotification.id);
                    alertify.alert(alertMessage);
                    $location.url('/candidate/' + vm.clickedNotification.candidateId);
                }
            });
        }
    }
})();