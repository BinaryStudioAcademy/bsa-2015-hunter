(function() {
    "use strict";

    angular
        .module('hunter-app')
        .factory('globalService', globalService);

    globalService.$inject = [];

    function globalService() {

        var service = {
            baseUrl: window.globalVars.baseUrl,
            viewsUrl: window.globalVars.baseUrl + 'Views/'
        }

        function getUrl(url) {
            if (window.globalVars.baseUrl === '/') {
                return url;
            }
            return window.globalVars.baseUrl + url;
        };

        function redirect(url) {
            var target = getUrl(url);
            window.location = target;
        }

        service.url = getUrl;
        service.redirect = redirect;
        return service;
    }
})();