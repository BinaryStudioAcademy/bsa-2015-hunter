(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('RolesListController', rolesListController);

    rolesListController.$inject = [
        '$scope',
        '$location',
        '$routeParams',
        'AuthService',
        'UserProfileService', //change on your service
        'UserRoleHttpService'
    ];

    function rolesListController($scope, $location, $routeParams, authService, service, userRoleHttpService) {
        var vm = this;


        userRoleHttpService.getUserRoles().then(function (result) {
                vm.rolesList = result.data;
            });
    };

})();