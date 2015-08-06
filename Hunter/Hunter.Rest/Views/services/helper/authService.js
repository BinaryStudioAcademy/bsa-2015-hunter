(function() {
    'use strict';

    angular
        .module('hunter-app')
        .factory('AuthService', AuthService);

    AuthService.$inject = [];

    function AuthService() {
        var service = {
            isLoggedIn: isLoggedIn
        };

        function isLoggedIn() {
            /*
                if (some) {
                    return true;
                } else {
                    return false;
                }
            */
            return true;
        }

        return service;
    }
})();