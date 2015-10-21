(function () {
    'use strict';
    angular
        .module('hunter-app')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$scope', '$location', 'AuthService'];


    function LoginController($scope, $location, AuthService) {
        var vm = this;
        vm.loginData = {
            userName: "",
            password: ""
        };

        vm.message = "";

        vm.login = function() {
            AuthService.login(vm.loginData);


        }
    };
})();
