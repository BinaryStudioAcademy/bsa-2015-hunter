(function () {
    'use strict';
    angular.module('hunter-app')
        .controller('loginController', ['$scope', '$location', 'authService', 'HttpHandler', function ($scope, $location, authService, HttpHandler) {

            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.message = "";

            $scope.login = function () {

                authService.login($scope.loginData).then(function (response) {

                    $location.path('/');

                },
                 function (err) {
                     $scope.message = err.error_description;
                 });
            };

         
        }]);
})();
