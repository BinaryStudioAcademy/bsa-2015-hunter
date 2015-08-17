(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('FeedbackHttpService', FeedbackHttpService);

    FeedbackHttpService.$inject = [
        '$q',
        'HttpHandler'
    ];

    function FeedbackHttpService($q, httpHandler) {
        var service = {
            getHrFeedback: getHrFeedback,
            saveHrFeedback: saveHrFeedback,
            getTechFeedback: getTechFeedback,
            saveFeedback: saveFeedback
        }

        function getHrFeedback(vid, cid) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/feedback/hr/' + vid + '/' + cid,
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                    //console.log("getHrFeedback " + result.data);
                },
                errorCallback: function (status) {
                    console.log("Get vacancies error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        function saveHrFeedback(feedback, vid, cid) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/feedback/save',
                verb: 'POST',
                body: JSON.stringify(feedback),
                successCallback: function (result) {
                    var getResult = getHrFeedback(vid, cid);
                    deferred.resolve(getResult);
                },
                errorCallback: function (result) { console.log(result); }
            });
            return deferred.promise;
        };

        function getTechFeedback(vacancyId, candidateId) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/feedback/tech/' + vacancyId + '/' + candidateId,
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get feedback error");
                }
            });
            return deferred.promise;
        }

        function saveFeedback(feedback, vacancyId, candidateId) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/feedback/save',
                verb: 'POST',
                body: JSON.stringify(feedback),
                successCallback: function (result) {
                    var newFeedback = getTechFeedback(vacancyId, candidateId);
                    deferred.resolve(newFeedback);
                },
                errorCallback: function (result) {
                    console.log("tech feedback update - error");
                    console.log(result);
                }
            });
            return deferred.promise;
        };


        return service;
    }
})();