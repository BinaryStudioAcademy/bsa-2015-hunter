(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('ActivityListController', ActivityListController);

    ActivityListController.$inject = [
        '$location',
        '$scope',
        'AuthService',
        'ActivityHttpService',
        'localStorageService'


    ];

    function ActivityListController($location, $scope, authService, activityHttpService, localStorageService) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        vm.name = "Activity";

        $scope.IndexCtrl.amount = 0;

        

        //(function() {
        //    // This is function for initialization actions, for example checking auth
        //    if (authService.isLoggedIn()) {
        //    // Can Make Here Any Actions For Data Initialization, for example, http queries, etc.
        //    } else {
        //        $location.url('/login');
        //    }
        //})();

        vm.activityList;
        // Here we should write any functions we need, for example, body of user actions methods.
        activityHttpService.getActivityList(function (data) {
            vm.activityList = data.data;
            console.log(data.data);

            if(isNewActivityPresent(vm.activityList)){
                saveLastViewdActivityId(vm.activityList);
            }

        });

        function saveLastViewdActivityId(activityList) {
            if (activityList == null || activityList.length == 0) {
                return;
            }

            activityList.sort(sortFunc);

            var lastViewdId = activityList[0].id;

            localStorageService.set('lastViewdActivityId', lastViewdId);

            activityHttpService.saveLastActivityId(function(response) {
                console.log(response.data);
            }, 
            lastViewdId);
        }

        function isNewActivityPresent(activityList) {
            if (activityList == null || activityList.length == 0) {
                return;
            }

            activityList.sort(sortFunc);

            var lastId = localStorageService.get('lastViewdActivityId');

            if (lastId != activityList[0].id) {
                return true;
            } else {
                return false;
            }
        }
    }

    function sortFunc(a, b) {
        var aDate = new Date(a.time);
        var bDate = new Date(b.time);

        if (aDate > bDate) {
            return -1;
        } else if (aDate < bDate) {
            return 1;
        } else {
            return 0;
        }
    }

})();