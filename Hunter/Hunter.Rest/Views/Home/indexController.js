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
        '$location',
        '$rootScope'
    ];

    function IndexController($scope, indexHttpService, $interval, notificationHttpService, $location, $rootScope) {
        var vm = this;
        vm.name = "Index";
        vm.amount = 0;
        $scope.radioModel = 'Home';
        $rootScope.notifications = null;
        $rootScope.clickedNotification = null;

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
                $rootScope.notifications = result;
                if ($rootScope.notifications != null) {
                    for (var i = 0; i < $rootScope.notifications.length; i++) {
                        alertNotification(i);
                    }
                }
            });
        }

        function alertNotification(index) {
            $rootScope.clickedNotification = $rootScope.notifications[index];
            var alertMessage = $rootScope.clickedNotification.notificationDate + ' ' + $rootScope.clickedNotification.message + '<a href="#/candidate/' + $rootScope.clickedNotification.candidateId + '"></a>';

            alertify.message('Click me to show a notification!', 180, function (isClicked) {
                if (isClicked) {
                    console.log($rootScope.clickedNotification);
                    notificationHttpService.notificationShown($rootScope.clickedNotification.id);
                    alertify.alert(alertMessage, function () {
                        $location.url('/candidate/' + $rootScope.clickedNotification.candidateId);
                        $rootScope.$apply();
                    });
                }
            });
        }
    }
})();