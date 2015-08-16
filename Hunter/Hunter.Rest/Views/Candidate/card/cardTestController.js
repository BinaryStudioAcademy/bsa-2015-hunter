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
        vm.testFile = '';

        //to check that feedbackText was changed
        vm.prevFeedbackText = '';

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
        vm.feedbackConfig;
        CardTestHttpService.getTest(vm.vacancyId, vm.candidateId, function(response) {
            vm.test = response.data;

            if (vm.test.feedback != null && vm.test.feedback.text != '') {
                vm.feedbackConfig = {
                    'buttonText': 'Edit',
                    'fieldReadonly': true
                }
            } else {
                vm.feedbackConfig = {
                    'buttonText': 'Save',
                    'fieldReadonly': false
                }
            }
        });

        vm.feedbackButtonToggle = function() {
            toggleFeedbackConfig();

            //if feedback text changed -> update test
            if (vm.prevFeedbackText != vm.test.feedback.text) {
                
            }
        }
        
        //change name of feedback button and readonly expression for textarea
        function toggleFeedbackConfig() {
            vm.feedbackConfig.fieldReadonly = !vm.feedbackConfig.fieldReadonly;

            if (vm.feedbackConfig.fieldReadonly) {
                vm.feedbackConfig.buttonText = 'Edit';
            } else {
                vm.feedbackConfig.buttonText = 'Save';
                vm.prevFeedbackText = vm.test.feedback.text;
            }
        }
    }
})();