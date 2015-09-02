(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('AuthService', AuthService);

    AuthService.$inject = ['$http', '$q', 'localStorageService'];

    function AuthService($http, $q, localStorageService) {

        var authServiceFactory = {};

        var _authentication = {
            isAuth: false,
            userName: ""
        };

        var _saveRegistration = function (registration) {

            _logOut();

            return $http.post('/api/account/register', registration).then(function (response) {
                return response;
            });

        };

        var _login = function (loginData) {

            var data = {email: loginData.userName ,password: loginData.password}

            var deferred = $q.defer();

            $http.defaults.useXDomain = true;
            $http({
                    method: 'POST',
                    url: 'http://team.binary-studio.com/auth/api/login',
                    headers: {
                        'Content-Type': 'application/json;charset=UTF-8'
                    },
                    withCredentials: true,
                    data: {
                        "email": loginData.userName,
                        "password": loginData.password
                    }
                }).success(function (response) {

                    localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName });

                    _authentication.isAuth = true;
                    _authentication.userName = loginData.userName;

                    deferred.resolve(response);

                })
                .error(function (err, status) {
                    _logOut();
                    deferred.reject(err);
                });
            return deferred.promise;
        };

        var _logOut = function () {

            localStorageService.remove('authorizationData');

            _authentication.isAuth = false;
            _authentication.userName = "";
            _authentication.useRefreshTokens = false;
        };

        var _fillAuthData = function () {

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                _authentication.isAuth = true;
                _authentication.userName = authData.userName;
                console.log(authData);
            }
        };

        authServiceFactory.saveRegistration = _saveRegistration;
        authServiceFactory.login = _login;
        authServiceFactory.logOut = _logOut;
        authServiceFactory.fillAuthData = _fillAuthData;
        authServiceFactory.authentication = _authentication;

        return authServiceFactory;
    }


})();