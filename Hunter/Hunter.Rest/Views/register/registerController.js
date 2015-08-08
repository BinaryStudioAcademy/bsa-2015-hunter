(function () {
    'use strict';
    angular
        .module('hunter-app')
        .controller('registerController', registerController);

    registerController.$inject = ['$scope', 'AuthService', 'UserRoleHttpService'];


    function registerController($scope, AuthService, UserRoleHttpService) {

       // $scope.roles = [];

        $scope.states = [
            { id: 0, name: 'State1' },
            { id: 1, name: 'State2' }
        ];

        UserRoleHttpService.getUserRoles().then(function (result) {
            $scope.roles = result;
        });


        $scope.newUser = {
            email: '',
            password: '',
            confirmPassword: '',
            state: 0,
            roleId: 0
        }

        $scope.register = function () {
            console.log('asada');
            AuthService.saveRegistration($scope.newUser);
        }

    };


})();
