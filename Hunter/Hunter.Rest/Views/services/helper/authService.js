(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('AuthService', AuthService);

    AuthService.$inject = ['$http', '$q', 'localStorageService', '$cookies', 'jwtHelper'];

    function AuthService($http, $q, localStorageService, $cookies, jwtHelper) {

        var authServiceFactory = {};

        var _authentication = {
            isAuth: false,
            userName: ""
        };

        var _saveRegistration = function (registration) {

            //_logOut();

            return $http.post('/api/account/register', registration).then(function (response) {
                return response;
            });

        };

        var _login = function (loginData) {

            var deferred = $q.defer();

            $http.post('/api/login', loginData)
                  .success(function (response) {

                    _authentication.isAuth = true;
                    _authentication.userName = loginData.userName;

                    if (response.referer === undefined || response.referer === '') {
                        $location.path('/');
                    } else {
                        window.location.href = decodeURIComponent(response.referer);
                    }
                    _fillAuthData();
                    deferred.resolve(response);
                })
                .error(function (err, status) {
                   // _logOut();
                    deferred.reject(err);
                });
            return deferred.promise;
        };

        var _logOut = function () {

            //localStorageService.remove('authorizationData');

            _authentication.isAuth = false;
            _authentication.userName = "";
            _authentication.useRefreshTokens = false;
        };

        var _fillAuthData = function () {

            var token = $cookies.get('x-access-token');
            if (token) {

                var payload = jwtHelper.decodeToken(token);

                _authentication.isAuth = true;
                _authentication.userName = token.email;
                _authentication.role = token.role;
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