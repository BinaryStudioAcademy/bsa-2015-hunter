(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('UserProfileService', userProfileService);

    userProfileService.$inject = [
        'HttpHandler'
    ];

    function userProfileService(httpHandler) {
        var service = {
            updateUserProfile: updateUserProfile,
            getUserProfile: getUserProfile,
            getUserProfileList: getUserProfileList,
            addUserProfile: addUserProfile
        };

        function updateUserProfile(body, onSuccess, id) {
            httpHandler.sendRequest({
                verb: 'POST', //'PUT',
                url: '/api/userprofile/' + id,
                body: body,
                successCallback: onSuccess
            });
        }

        function addUserProfile(body, onSuccess) {
            httpHandler.sendRequest({
                verb: 'POST',
                url: 'api/userprofile/',
                body: body,
                successCallback: onSuccess
            });
        }

        function getUserProfile(id, onSuccess) {
            httpHandler.sendRequest({
                verb: 'GET',
                url: '/api/userprofile/' + id,
                successCallback: onSuccess,
                errorMessageToDev: 'GET USERPROFILE INFO ERROR: ',
                errorMessageToUser: 'Failed'
            });
        }

        function getUserProfileList(page, successCallback) {
            httpHandler.sendRequest({
                verb: 'GET',
                url: '/api/userprofile/',
                params: {'page': page},
                successCallback: successCallback,
                errorMessageToDev: 'GET USERPROFILE INFO ERROR: ',
                errorMessageToUser: 'Failed'
            });
        }

        return service;
    }
})();