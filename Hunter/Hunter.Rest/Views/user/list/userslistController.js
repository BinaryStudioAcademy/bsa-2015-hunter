(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('UsersListController', usersListController);

    usersListController.$inject = [
        '$scope',
        '$location',
        '$routeParams',
        'AuthService',
        'UserProfileService', //change on your service
        'UserRoleHttpService'
    ];

    function usersListController($scope, $location, $routeParams, authService, userProfileService, userRoleHttpService) {
        var vm = this;

        vm.rolesList = [];
        vm.changeRole = changeRole;

        // Here we should write any functions we need, for example, body of user actions methods.
        var updatePage = function (newPageNum) {
            if (typeof newPageNum !== "number" || newPageNum <= 0) {
                newPageNum = 1;
            }
           vm.page = newPageNum;
        }

        vm.loadUsers = function () {
            $location.search('page', vm.page);
            userProfileService.getUserProfileList(vm.page, function (response) {
                vm.profilesList = response.data;
            });
        }

        $scope.$watch(function() {return vm.page;}, vm.loadUsers);

        //vm.watch('page', vm.loadUsers);
        
        vm.nextPage = function () {
            updatePage(vm.page + 1);
        }

        vm.prevPage = function () {
            updatePage(vm.page - 1);
        }

        updatePage($routeParams.page);

        userRoleHttpService.getUserRoles().then(function (result) {
                vm.rolesList = result.data;
            });

        function changeRole(user){
            userProfileService.updateUserProfile(user);
        }
    };
})();