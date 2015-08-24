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
            removeCard: removeCard
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
                url: 'api/card/' + vid + '/' + cid,
                verb: "DELETE",
                successCallback: function (result) {
                    //console.log(result);
                    //$location.url("/vacancy/1/longlist");
                },
                errorCallback: function (result) { console.log(result); }
            });
        }

        return service;
    }
})();