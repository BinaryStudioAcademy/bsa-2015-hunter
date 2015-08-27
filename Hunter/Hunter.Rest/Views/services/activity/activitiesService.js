(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('ActivityService', ActivityService);

    ActivityService.$inject = [
        'HttpHandler',
        'EnumConstants'
    ];

    function ActivityService(httpHandler, EnumConstants) {
        var vm = this;

        vm.activityTags = EnumConstants.activityTags;

        var service = {
            getFilterUsers: getFilterUsers,
            getFilterTags: getFilterTags
        };

        vm.filterUsers = [];
        function getFilterUsers(f) {
            for (var i = 0; i < f.length; i++) {
                if (f[i].filterId === 1) {
                    vm.filterUsers.push(f[i]);
                }
            }

            return vm.filterUsers;
        }

        vm.filterTags = [];
        function getFilterTags(f) {
            for (var i = 0; i < f.length; i++) {
                if (f[i].filterId === 0) {
                    f[i].name = vm.activityTags[f[i].optionId];
                    vm.filterTags.push(f[i]);
                }
            }

            return vm.filterTags;
        }


        return service;
    }
})();