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
            getCandidateNotifications: getCandidateNotifications,
            getActiveNotifications: getActiveNotifications,
            addNotification: addNotification,
            notificationShown: notificationShown,
            deleteNotification: deleteNotification,
            notify: notify
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

        function getCandidateNotifications(id) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/notifications/candidate/' + id,
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get candidate notification list error");
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

        function notificationShown(id) {
            httpHandler.sendRequest({
                verb: 'PUT',
                url: '/api/notifications/' + id + '/shown',
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

        function notify() {
            httpHandler.sendRequest({
                verb: 'GET',
                url: '/api/notifications/notify',
                successMessageToUser: 'Scheduled notifications sent',
                errorMessageToUser: 'Scheduled notifications not sent',
                errorCallback: function (status) {
                    console.log("Scheduled notifications sending error");
                    console.log(status);
                }
            });
        }

        return service;
    }
})();