(function () {
    'use strict';

    angular
        .module('hunter-app')
        .factory('TestService', TestService);

    TestService.$inject = [
        'CardHttpService'
    ];

    function TestService(CardHttpService) {
        var service = {
            getLinkOnTest: getLinkOnTest
        }

        function getLinkOnTest() {

        }

        return service;

    }
})();