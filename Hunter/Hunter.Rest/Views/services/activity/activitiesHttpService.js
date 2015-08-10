(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('ActivityHttpService', ActivityHttpService);

    ActivityHttpService.$inject = [
        'HttpHandler'
    ];

    function ActivityHttpService(httpHandler) {
        var service = {
            getActivityList: getActivityList
        };

        function getActivityList(successCallback, body) {
            httpHandler.sendRequest({
                verb: 'GET',
                url: '/api/activities/',
                //                body: body,
                successCallback: successCallback,
                errorMessageToDev: 'GET ACTIVITY INFO ERROR: ',
                errorMessageToUser: 'Failed'
            });
        }

        return service;
    }
})();