(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('CandidateHttpService', CandidateHttpService);

    CandidateHttpService.$inject = [
        'HttpHandler',
        '$q'
    ];

    function CandidateHttpService(httpHandler,$q) {
        var service = {
            updateCandidate: updateCandidate,
            getCandidate: getCandidate,
            getCandidateList: getCandidateList,
            addCandidate: addCandidate,
//            getLongList: getLongList,
            getLongListDetails: getLongListDetails,
            parseLinkedIn: parseLinkedIn,
            getAddedByList: getAddedByList,
            setShortListFlag: setShortListFlag,
            updateCandidateResolution : updateCandidateResolution
        };

        function updateCandidate(body, successCallback, id) {
            httpHandler.sendRequest({
                verb: 'PUT',
                url: '/api/candidates/'+id,
                body: body,
                successCallback: successCallback
            });
        }

        function addCandidate(body, successCallback) {
            httpHandler.sendRequest({
                verb: 'POST',
                url: 'api/candidates/',
                body: body,
                successCallback: successCallback
            });
        }

        function getCandidate(id) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                verb: 'GET',
                url: '/api/candidates/' + id,
//                body: body,
                successCallback: function (response) {
                    deferred.resolve(response);
                },
                errorCallback: function (status) {
                    console.log("getting candidate error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        function getCandidateList() {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                verb: 'GET',
                url: '/api/candidates/',
//                body: body,
                successCallback: function (response) {
                    deferred.resolve(response);
                },
                errorCallback: function (status) {
                    console.log("getting candidates error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        //function getLongList(id) {
        //    var deferred = $q.defer();
        //    httpHandler.sendRequest({
        //        url: '/api/candidates/longlist/' + id,
        //        verb: 'GET',
        //        successCallback: function (result) {
        //            deferred.resolve(result.data);
        //        },
        //        errorCallback: function (status) {
        //            console.log("Get candidates long list error");
        //            console.log(status);
        //        }
        //    });
        //    return deferred.promise;
        //}

        function getLongListDetails(vid, cid) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/candidates/candidatelonglist/' + vid + '/' + cid,
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get candidates long list error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        function parseLinkedIn(url) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/resume/parse?url=' + url,
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get candidates long list error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        function getAddedByList() {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/candidates/addedby',
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get candidates long list Added by filter data error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        function setShortListFlag(cid, isShort) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/candidates/' +cid + '/' + isShort,
                verb: 'PUT',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log(status);
                    deferred.reject(status);
                }
            });
            return deferred.promise;
        }

        function updateCandidateResolution(cid, resolution) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/candidates/' + cid + '/resolution/' + resolution,
                verb: 'PUT',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log(status);
                    deferred.reject(status);
                }
            });
            return deferred.promise;
        }

        return service;
    }
})();