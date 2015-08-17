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
            saveTestFeedback: saveTestFeedback
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
                url: '/api/feedback/hr/save',
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

        function saveTestFeedback(feedbackObj) {
            httpHandler.sendRequest({
                'url': '/api/feedback/test/update',
                'verb': 'PUT',
                'body': feedbackObj,
                'successCallback': function(response) { console.log('test feedback was successfuly updated') },
                'errorCallback': function(response) { console.log('Updating was failed') }
            });
        }

        return service;
    }
})();