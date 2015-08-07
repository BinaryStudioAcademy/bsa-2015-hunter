(function () {
    'use strict';
    angular
        .module('hunter-app')
        .controller('loginController', loginController);

    loginController.$inject = ['$scope', '$location', 'AuthService', 'HttpHandler'];


    function loginController($scope, $location, AuthService, HttpHandler) {

        $scope.loginData = {
            userName: "",
            password: ""
        };

        $scope.message = "";

        $scope.login = function () {

            AuthService.login($scope.loginData).then(function (response) {

                $location.path('/');

            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };


    };
})();
