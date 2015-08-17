(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardTestController', CardTestController);

    CardTestController.$inject = [
        'CardTestHttpService',
        '$routeParams',
        'FeedbackHttpService'
    ];

    function CardTestController(CardTestHttpService, $routeParams, FeedbackHttpService) {
        var vm = this;
        vm.templateName = 'Test';
        var candidateId = $routeParams.cid;
        var vacancyId = $routeParams.vid;

        vm.testLink = '';
        vm.testFile = '';

        //voting
        vm.like = {'count': 0, 'wasClicked': false}
        vm.dislike = { 'count': 0, 'wasClicked': false };
        vm.vote = function(isLike) {
            if (isLike && !vm.dislike.wasClicked) {
                vm.like.count += vm.like.wasClicked ? -1 : 1;
                vm.like.wasClicked = !vm.like.wasClicked;
            }

            if (!isLike && !vm.like.wasClicked) {
                vm.dislike.count -= vm.dislike.wasClicked ? -1 : 1;
                vm.dislike.wasClicked = !vm.dislike.wasClicked;
            }
        }
        //----

        //to check that feedbackText was changed
//        var prevFeedbackText = '';

//        vm.lastUploadTestId;
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
                var lastUploadTestId = response.data;
                testSend.id = lastUploadTestId;
                testSend.feedback = {
                    'cardId': vm.test.cardId,
                    'text': '',
                    'date': '',
                    'type': 4
                };
                testSend.feedbackConfig = {
                    'buttonText': 'Save',
                    'fieldReadonly': false
                }

                vm.test.tests.push(testSend);
            });
        }

        vm.test;
//        vm.feedbackConfig;
        CardTestHttpService.getTest(vacancyId, candidateId, function(response) {
            vm.test = response.data;

            vm.test.tests.forEach(function (test) {
                var feedbackConfig;
                if (test.feedback != null && test.feedback.text != '') {
                    feedbackConfig = {
                        'buttonText': 'Edit',
                        'fieldReadonly': true
                    }
                } else {
                    test.feedback = {
                        'cardId': vm.test.cardId,
                        'text': '',
                        'date': '',
                        'type': 4
                    };

                    feedbackConfig = {
                        'buttonText': 'Save',
                        'fieldReadonly': false
                    }
                }

                test.feedbackConfig = feedbackConfig;
//                prevFeedbackText = vm.test.feedback.text;
            });
        });

        vm.feedbackButtonToggle = function (test) {
            if (vm.test.tests.length == 0) {
                return;
            }

            toggleFeedbackConfig(test.feedbackConfig);

            //if feedback text changed -> update test
            //            if (prevFeedbackText != vm.test.feedback.text) {

            if(test.feedbackConfig.fieldReadonly)
                FeedbackHttpService.saveTestFeedback({
                    'feedback': test.feedback,
                    'testId': test.id
                });

//                prevFeedbackText = vm.test.feedback.text;
//            }
        }
        
        //change name of feedback button and readonly expression for textarea
        function toggleFeedbackConfig(config) {
            config.fieldReadonly = !config.fieldReadonly;

            if (config.fieldReadonly) {
                config.buttonText = 'Edit';
            } else {
                config.buttonText = 'Save';
//                vm.prevFeedbackText = vm.test.feedback.text;
            }
        }
    }
})();