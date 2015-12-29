(function () {
    'use strict';
    angular
        .module('hunter-app')
        .factory('AuthInterceptorService', AuthInterceptorService);

    AuthInterceptorService.$inject = ['$q', '$injector', '$location', 'localStorageService', '$cookies', '$window','$rootScope'];

    function AuthInterceptorService($q, $injector, $location, localStorageService, $cookies, $window, $rootScope) {

        var authInterceptorServiceFactory = {};

        var _responseError = function (rejection) {
            switch (rejection.status) {
            case 401:
                if ($window.location.href != rejection.headers('location')) {
                    // var authService = $injector.get('AuthService');
                    // authService.logOut();
                    //$cookies.put('referer', $location.absUrl());
                    $window.location.href = rejection.headers('location');
                }
                break;
            case 403:
                    $rootScope.errorMessage = "You haven't got enough rights to access that page";
                    $location.url('/vacancy');
                break;
            default:
                break;
            }
            return $q.reject(rejection);
        }

        authInterceptorServiceFactory.responseError = _responseError;

        return authInterceptorServiceFactory;
    }
})();