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
        $scope.notifications = null;
        $scope.clickedNotification = null;

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
                $scope.notifications = result;
                if ($scope.notifications != null) {
                    for (var i = 0; i < $scope.notifications.length; i++) {
                        alertNotification(i);
                    }
                }
            });
        }

        function alertNotification(index) {
            $scope.clickedNotification = $scope.notifications[index];
            var alertMessage = $scope.clickedNotification.pending + ' ' + $scope.clickedNotification.message + '<a href="#/candidate/' + $scope.clickedNotification.candidateId + '"></a>';

            alertify.message('Click me to show a notification!', 180, function (isClicked) {
                if (isClicked) {
                    console.log($scope.clickedNotification);
                    notificationHttpService.notificationShown($scope.clickedNotification.id);
                    alertify.alert(alertMessage, function () {
                        $location.url('/candidate/' + $scope.clickedNotification.candidateId);
                        $scope.$apply();
                    });
                }
            });
        }
    }
})();