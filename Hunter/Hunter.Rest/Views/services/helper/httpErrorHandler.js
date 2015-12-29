(function() {
    angular
        .module('hunter-app')
        .factory('HttpErrorHandler', HttpErrorHandler);

    function HttpErrorHandler($location, $rootScope) {
        var service = {
            responseError: responseError
        };

        function responseError(errorResponse) {
            switch (errorResponse.status) {
                case 403:
                    $rootScope.errorMessage = "You haven't got enough rights to access that page";
                    $location.url('/vacancy');
                console.log('ERROR HAS BEEN CAUGHT!!!!!!!!!!!!!!!!!!!!!!');
            default:
            }
        }

        return service;
    }
})();