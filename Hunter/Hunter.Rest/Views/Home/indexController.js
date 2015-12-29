﻿(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('IndexController', IndexController);

    IndexController.$inject = [
        '$scope',
        'IndexHttpService',
        '$interval',
        'NotificationHttpService',
        'UserHttpService',
        '$location',
        '$rootScope',
        '$cookies',
        'localStorageService'
    ];

    function IndexController($scope, indexHttpService, $interval, notificationHttpService, UserHttpService, $location, $rootScope, $cookies, localStorageService) {
        var vm = this;
        vm.name = "Index";
        vm.amount = 0;
        vm.countTests = 0;
        $scope.radioModel = 'Home';
        vm.setCountTests = setCountTests;
        $rootScope.notifications = null;
        $rootScope.clickedNotification = null;

        vm.logOut = logOut;

        callRefreshFunctions();

        $interval(callRefreshFunctions, 180000); //3 minutes


        function callRefreshFunctions() {
            getActivityAmount();
            getActiveNotifications();
            getCountTasksForCheck();
        }

        function getActivityAmount() {
            indexHttpService.getActivityAmount(function(response) {
                vm.amount = response.data;
            });
        }

        function getCountTasksForCheck() {
            indexHttpService.getTasksForCheck().then(function(result) {
                console.log("count = " + result);
                vm.countTests = result;
            });
        }

        function setCountTests(count) {
            vm.countTests = count;
        }

        function getActiveNotifications() {
            notificationHttpService.getActiveNotifications().then(function(result) {
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
            var alertMessage = $rootScope.clickedNotification.notificationDate + ' ' + $rootScope.clickedNotification.message + '<a href="./candidate/' + $rootScope.clickedNotification.candidateId + '"></a>';

            alertify.message('Click me to show a notification!', 180, function(isClicked) {
                if (isClicked) {
                    notificationHttpService.notificationShown($rootScope.clickedNotification.id);
                    alertify.alert("Notification", alertMessage, function () {
                        $location.url('/candidate/' + $rootScope.clickedNotification.candidateId);
                        $rootScope.$apply();
                    });
                }
            });
        }

        function logOut() {
            UserHttpService.logout().then(function (data) {
                $location.url('./');
            });
        }


        localStorageService.set('oldUrl', $location.url());
        $rootScope.$on('$locationChangeStart', function () {
            
            if (localStorageService.get('oldUrl') != $location.url()) {
                localStorageService.set('oldUrl', localStorageService.get('newUrl'));
                localStorageService.set('newUrl',$location.url());
            };
        });        

    }
})();