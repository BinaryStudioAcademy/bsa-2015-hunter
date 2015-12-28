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
        'RoleMappingHttpService',
        'UserRoleHttpService'
    ];

    function rolesListController($scope, $location, $routeParams, authService, service, roleMappingHttpService, userRoleHttpService) {
        var vm = this;

        vm.roleMapping = {};
        vm.rolesList = {};
        vm.changeRole = changeRole;

        roleMappingHttpService.getAllRoleMapping().then(function (response) {
                vm.roleMapping = response.data;
            });

        userRoleHttpService.getUserRoles().then(function (result) {
                vm.rolesList = result.data;
            });

        function changeRole(roleMapping){
            roleMappingHttpService.updateRoleMapping(roleMapping);
        }

    };

})();