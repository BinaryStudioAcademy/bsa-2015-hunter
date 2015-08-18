(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardTestController', CardTestController);

    CardTestController.$inject = [
        'CardTestHttpService',
        '$routeParams',
        'FeedbackHttpService',
        'UploadTestService',
        'localStorageService'
    ];

    function CardTestController(CardTestHttpService, $routeParams,
        FeedbackHttpService, UploadTestService, localStorageService) {
        var vm = this;
        vm.templateName = 'Test';
        var candidateId = $routeParams.cid;
        var vacancyId = $routeParams.vid;
        var userName = localStorageService.get('authorizationData').userName;

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

        vm.uploadLink = function () {
            if (vm.testLink == '') {
                return;
            }

            var testSend = {
                'url': vm.testLink,
                'fileId': null,
                'cardId': vm.test.cardId,
                'feedbackId': null,
                'added': new Date()
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
            });
        });

        vm.feedbackButtonToggle = function (test) {
            if (vm.test.tests.length == 0) {
                return;
            }

            toggleFeedbackConfig(test.feedbackConfig);

            if(test.feedbackConfig.fieldReadonly){
                FeedbackHttpService.saveTestFeedback({
                    'feedback': test.feedback,
                    'testId': test.id
                });

                test.feedback.userName = userName;
                test.feedback.date = new Date();
            }

        }
        
        //change name of feedback button and readonly expression for textarea
        function toggleFeedbackConfig(config) {
            config.fieldReadonly = !config.fieldReadonly;

            if (config.fieldReadonly) {
                config.buttonText = 'Edit';
            } else {
                config.buttonText = 'Save';
            }
        }

        vm.fileName;
        var file;
        vm.onFileChanged = function (event) {
            file = event.currentTarget.files[0];
            vm.fileName = file.name;
            UploadTestService.onFileSelect(file);
        }

        vm.uploadFile = function() {
            UploadTestService.uploadTest(candidateId, vacancyId, function(response) {
                var fileId = response.data;

                var test = {
                    'url': '',
                    'fileId': fileId,
                    'cardId': vm.test.cardId,
                    'feedbackId': null,
                    'added': new Date()
                };

                CardTestHttpService.sendTest(test, function (response) {
                    var testId = response.data;

                    test.id = testId;
                    test.feedback = {
                        'cardId': vm.test.cardId,
                        'text': '',
                        'date': '',
                        'type': 4
                    };
                    test.feedbackConfig = {
                        'buttonText': 'Save',
                        'fieldReadonly': false
                    };
                    test.file = {
                        'id': fileId,
                        'fileType': 4,
                        'fileName': file.name,
                        'added': new Date(),
                        'candidateId': candidateId,
                        'vacancyId': vacancyId,
                        'size': file.size
                    };

                    vm.test.tests.push(test);
                });
            });
        }
    }
})();