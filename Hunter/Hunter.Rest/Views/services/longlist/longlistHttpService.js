(function () {
    angular.module('hunter-app')
        .factory('LonglistHttpService', LonglistHttpService);

    LonglistHttpService.$inject = [
    'HttpHandler',
    '$q'
    ];
    function LonglistHttpService(httpHandler, $q) {
        var service = {
            addCards: addCards
        }

        function addCards(body) {
            httpHandler.sendRequest({
                verb: 'POST',
                url: '/api/card',
                body: JSON.stringify(body),
                successMessageToUser: 'Cards was added',
                errorMessageToUser: 'Cards was not added',
                errorCallback: function (status) {
                    console.log("Add cards error");
                    console.log(status);
                }
            });
        }

        return service;
    }
})();