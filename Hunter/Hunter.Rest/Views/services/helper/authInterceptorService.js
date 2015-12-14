(function () {
    'use strict';
    angular
        .module('hunter-app')
        .factory('AuthInterceptorService', AuthInterceptorService);

    AuthInterceptorService.$inject = ['$q', '$injector', '$location', 'localStorageService', '$cookies', '$window'];

    function AuthInterceptorService($q, $injector, $location, localStorageService, $cookies, $window) {

        var authInterceptorServiceFactory = {};

        var _responseError = function (rejection) {

            if (rejection.status === 401) {

                if ($window.location.href != rejection.headers('location')) {

                    // var authService = $injector.get('AuthService');
                    // authService.logOut();

                    $cookies.put('referer', $location.absUrl());
                    $window.location.href = rejection.headers('location');
                }
            }

            return $q.reject(rejection);
        }

        authInterceptorServiceFactory.responseError = _responseError;

        return authInterceptorServiceFactory;
    }
})();