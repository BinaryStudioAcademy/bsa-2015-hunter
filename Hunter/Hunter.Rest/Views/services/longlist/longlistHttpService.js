(function () {
    'use strict';

    angular.module('hunter-app')
        .factory('LonglistHttpService', LonglistHttpService);

    LonglistHttpService.$inject = [
    'HttpHandler',
    '$q',
	'$location'
    ];
    function LonglistHttpService(httpHandler, $q, $location) {
        var service = {
            addCards: addCards,
            removeCard: removeCard,
            getAppResults: getAppResults,
            getCardInfo: getCardInfo
        }

        function addCards(body, newLocation) {
            httpHandler.sendRequest({
                verb: 'POST',
                url: '/api/card',
                body: JSON.stringify(body),
                successCallback: function (result) {
                    $location.url(newLocation);
                },
                successMessageToUser: 'Cards were added',
                errorMessageToUser: 'Cards were not added',
                errorCallback: function (status) {
                    console.log("Add cards error");
                    console.log(status);
                }
            });
        }

        function removeCard(vid, cid) {
            httpHandler.sendRequest({
                url: 'api/card/isUsed/' + vid + '/' + cid,
                verb: 'GET',
                successCallback: function (result) {
                    if (result.data) {
                        var clickResult = confirm('Would you like to remove card from Long List and delete all its feedbacks and notes?');
                        if (clickResult === true) {
                            (function () {
                                httpHandler.sendRequest({
                                    url: 'api/card/deleteallinfo/' + vid + '/' + cid,
                                    verb: "DELETE",
                                    successCallback: function (result) {
                                        //console.log(result);
                                    },
                                    successMessageToUser: 'Card and all feedbacks and notes were removed',
                                    errorMessageToUser: 'Card was not removed. Card is used',
                                    errorCallback: function (result) { console.log(result); }
                                });
                            })();
                            //alert('user click true');
                        } else {
                             //alert('user click cancel');
                        }
                    } else {
                        //alert('is Card used result (else): ' + result.data);
                        (function () {
                            httpHandler.sendRequest({
                                url: 'api/card/' + vid + '/' + cid,
                                verb: "DELETE",
                                successCallback: function (result) {
                                    //console.log(result);
                                },
                                successMessageToUser: 'Card was removed',
                                errorMessageToUser: 'Card was not removed. Card is used',
                                errorCallback: function (result) { console.log(result); }
                            });
                        })();
                    }
                    //console.log(result);
                    //$location.url("/vacancy/1/longlist");
                },
                //successMessageToUser: 'Card was removed',
                //errorMessageToUser: 'Card was not removed. Card is used',
                errorCallback: function (result) { console.log(result); }
            });




            /**
             
             function getCardInfo(vid, cid) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/card/info/' + vid + '/' + cid,
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get application results error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }
             * /
             */



            
        }

        function getAppResults(cid) {

            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/card/appResults/' + cid,
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get application results error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        function getCardInfo(vid, cid) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                url: '/api/card/info/' + vid + '/' + cid,
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get application results error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }


        return service;
    }
})();