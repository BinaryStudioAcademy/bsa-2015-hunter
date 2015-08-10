(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('ActivityListController', ActivityListController);

    ActivityListController.$inject = [
        '$location',
        'AuthService',
        'ActivityHttpService'


    ];

    function ActivityListController($location, authService, activityHttpService) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        vm.name = "Activity";

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
        });
    }
})();