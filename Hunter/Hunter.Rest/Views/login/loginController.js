(function () {
    'use strict';
    angular
        .module('hunter-app')
        .controller('loginController', loginController);

    loginController.$inject = ['$scope', '$location', 'AuthService'];


    function loginController($scope, $location, AuthService) {
        var vm = this;
        vm.loginData = {
            userName: "",
            password: ""
        };

        vm.message = "";

        vm.login = function () {
            AuthService.login(vm.loginData).then(function (response) {

                $location.path('/');

            },
             function (err) {
                 vm.message = err.error_description;
             });
        };


    };
})();
