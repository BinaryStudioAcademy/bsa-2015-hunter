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
            deleteUserProfile: deleteUserProfile
        };

        function updateUserProfile(body, onSuccess, id) {
            httpHandler.sendRequest({
                verb: 'POST', //'PUT',
                url: '/api/userprofile/' + id,
                body: body,
                successCallback: onSuccess
            });
        };

        function deleteUserProfile(id, successCallback) {
            httpHandler.sendRequest({
                verb: 'DELETE',
                url: 'api/userprofile/' + id,
                successCallback: successCallback,
                successMessageToUser: "User with id " + id + "deleted",
                errorCallback: function (status) {
                    console.log("Delete vacancy error");
                    console.log(status);
                }
            });
        };

        function getUserProfile(id, onSuccess) {
            httpHandler.sendRequest({
                verb: 'GET',
                url: '/api/userprofile/' + id,
                successCallback: onSuccess,
                errorMessageToDev: 'GET USERPROFILE INFO ERROR: ',
                errorMessageToUser: 'Failed'
            });
        };

        function getUserProfileList(page, successCallback) {
            httpHandler.sendRequest({
                verb: 'GET',
                url: '/api/userprofile/',
                params: {'page': page},
                successCallback: successCallback,
                errorMessageToDev: 'GET USERPROFILE INFO ERROR: ',
                errorMessageToUser: 'Failed'
            });
        };

        return service;
    }
})();