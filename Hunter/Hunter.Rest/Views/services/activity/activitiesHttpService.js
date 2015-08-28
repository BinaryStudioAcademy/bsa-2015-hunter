(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('ActivityHttpService', ActivityHttpService);

    ActivityHttpService.$inject = [
        'HttpHandler',
        '$q'
    ];

    function ActivityHttpService(httpHandler, $q) {
        var service = {
            getActivityList: getActivityList,
            saveLastActivityId: saveActivityId,
            getFilterOptions: getFilterOptions
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

        function getFilterOptions() {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/activities/filters',
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get activities filter options error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        return service;
    }
})();