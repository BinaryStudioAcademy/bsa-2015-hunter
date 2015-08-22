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
        'localStorageService',
        '$scope'
    ];

    function CardTestController(CardTestHttpService, $routeParams,
        FeedbackHttpService, UploadTestService, localStorageService, $scope) {
        var vm = this;
        vm.templateName = 'Test';
        vm.editingIndex = -1;
        var candidateId = $routeParams.cid;
        var vacancyId = $routeParams.vid;
        var userName = localStorageService.get('authorizationData').userName;

        vm.testLink = '';
        vm.testFile = '';

        vm.changeCurrentTest = changeCurrentTest;

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
                    'type': 4,
                    "successStatus": 0
                };
                testSend.feedbackConfig = {
                    'buttonText': 'Save',
                    'fieldReadonly': false,
                    'style': {'border-coor': 'grey'}
                }

                vm.test.tests.push(testSend);
            });
        }

        // TODO:  All vm and local variables should be declared at the beginning
        vm.test;
        CardTestHttpService.getTest(vacancyId, candidateId, function(response) {
            vm.test = response.data;

            vm.test.tests.forEach(function (test) {
                var feedbackConfig;
                if (test.feedback != null && test.feedback.text != '') {
                    feedbackConfig = {
                        'buttonText': 'Edit',
                        'fieldReadonly': true,
                        "style": {
                            "border-color": test.feedback.successStatus == 0 ? 'grey'
                                : test.feedback.successStatus == 1 ? 'green' : 'red'
                        }
                    }
                } else {
                    test.feedback = {
                        'cardId': vm.test.cardId,
                        'text': '',
                        'date': '',
                        'type': 4,
                        "successStatus": 0
                    };

                    feedbackConfig = {
                        'buttonText': 'Save',
                        'fieldReadonly': false,
                        'style': {"border-color": 'grey'}
                    }
                }

                test.feedbackConfig = feedbackConfig;
            });
        });

        vm.feedbackButtonToggle = function (test) {
            if (vm.test.tests.length == 0) {
                return;
            }

            toggleFeedbackConfig(test.feedbackConfig, test.feedback.text);

            if(test.feedbackConfig.fieldReadonly && test.feedback.text != ''){
                FeedbackHttpService.saveTestFeedback({
                    'feedback': test.feedback,
                    'testId': test.id
                }).then(function(result) {
                    test.feedback.id = result.id;
                    test.feedback.date = result.update;
                    test.feedback.userName = result.userName;
                });

                test.feedback.userName = userName;
                test.feedback.date = new Date();
            }

        }
        
        //change name of feedback button and readonly expression for textarea
        function toggleFeedbackConfig(config, text) {
            config.fieldReadonly = text != '' ? !config.fieldReadonly : config.fieldReadonly;

            if (config.fieldReadonly && text != '') {
                config.buttonText = 'Edit';
            } else {
                config.buttonText = 'Save';
            }
        }

        vm.fileName;
        var file;
        vm.onFileChanged = function (event) {
            file = event.target.files[0];
            vm.fileName = file.name;
            UploadTestService.onFileSelect(file);
            $scope.$apply();
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
                        'type': 4,
                        "successStatus": 0
                    };
                    test.feedbackConfig = {
                        'buttonText': 'Save',
                        'fieldReadonly': false,
                        "style": {"border-color": 'grey'}
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

        function changeCurrentTest(index) {
            if (vm.editingIndex == -1) {
                vm.editingIndex = index;
            } else {
                if (vm.editingIndex == index) {
                    var test = vm.test.tests[vm.editingIndex];
                    if (test.comment != null) {
                        CardTestHttpService.updateTestComment({
                            id: test.id, 
                            comment: test.comment
                        });
                    }
                    if (test.feedback.text != null) {
                        FeedbackHttpService.saveTestFeedback({
                            'feedback': test.feedback,
                            'testId': test.id
                        }).then(function (result) {
                            test.feedback.id = result.id;
                            test.feedback.date = result.update;
                            test.feedback.userName = result.userName;
                            test.feedbackId = result.id;
                        });
                    }
                    vm.editingIndex = -1;
                } else {
                    alertify.alert('Apply your changes!');
                }

            }
            
        }
    }
})();