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
            getActivityList: getActivityList,
            saveLastActivityId: saveActivityId
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

        function saveActivityId(successCallback, body) {
            httpHandler.sendRequest({
                'verb': 'POST',
                'url': 'api/activities/save/lastid',
                'body': body,
                'successCallback': successCallback,
                'errorMessageToDev': 'POST ACTIVITY_ID ERROR',
                'errorMessageToUser': 'Failed'
            });
        }

        return service;
    }
})();