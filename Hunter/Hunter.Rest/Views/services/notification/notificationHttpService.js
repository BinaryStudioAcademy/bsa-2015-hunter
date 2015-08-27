(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('NotificationHttpService', NotificationHttpService);

    NotificationHttpService.$inject = [
        '$q',
        'HttpHandler'
    ];

    function NotificationHttpService($q, httpHandler) {
        var service = {
        };

        return service;
    }
})();