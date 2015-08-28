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
            getNotifications: getNotifications,
            getActiveNotifications: getActiveNotifications,
            addNotification: addNotification,
            notificationShown: notificationShown,
            deleteNotification: deleteNotification
        };

        function getNotifications() {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/notifications',
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get notification list error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        function getActiveNotifications() {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/notifications/active',
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get notification list error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        function addNotification(data) {
            httpHandler.sendRequest({
                verb: 'POST',
                url: '/api/notifications',
                body: data,
                successMessageToUser: 'Scheduled notification added',
                errorMessageToUser: 'Scheduled notification not added',
                errorCallback: function (status) {
                    console.log("Add Scheduled notification error");
                    console.log(status);
                }
            });
        }

        function notificationShown(data) {
            httpHandler.sendRequest({
                verb: 'PUT',
                url: '/api/notifications/shown/' + data,
                errorCallback: function (status) {
                    console.log("Shown Scheduled notification error");
                    console.log(status);
                }
            });
        }

        function deleteNotification(id) {
            httpHandler.sendRequest({
                verb: 'DELETE',
                url: '/api/notifications/' + id,
                successMessageToUser: 'Scheduled notification deleted',
                errorMessageToUser: 'Scheduled notification not deleted',
                errorCallback: function (status) {
                    console.log("Delete Scheduled notification error");
                    console.log(status);
                }
            });
        }

        return service;
    }
})();