(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardTestController', CardTestController);

    CardTestController.$inject = [
        'CardTestHttpService',
        '$routeParams'
    ];

    function CardTestController(CardTestHttpService, $routeParams) {
        var vm = this;
        vm.templateName = 'Test';
        vm.candidateId = $routeParams.cid;
        vm.vacancyId = $routeParams.vid;
        vm.testLink = '';

        vm.lastUploadTestId;
        vm.uploadLink = function () {
            if (vm.testLink == '') {
                return;
            }

            var testSend = {
                'url': vm.testLink,
                'fileId': null,
                'cardId': vm.test.cardId,
                'feedbackId': null
            }

            CardTestHttpService.sendTest(testSend, function(response) {
                vm.lastUploadTestId = response.data;
            });
        }

        vm.test;
        CardTestHttpService.getTest(vm.vacancyId, vm.candidateId, function(response) {
            vm.test = response.data;
        });
    }
})();