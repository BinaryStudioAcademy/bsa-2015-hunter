(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardTechnicalInterviewController', CardTechnicalInterviewController);

    CardTechnicalInterviewController.$inject = [
        'FeedbackHttpService',
        '$routeParams',
        'VacancyHttpService',
        'localStorageService'
    ];

    function CardTechnicalInterviewController(FeedbackHttpService, $routeParams, VacancyHttpService, localStorageService) {
        var vm = this;
        vm.templateName = 'Technical Interview';
        vm.techFeedback;
        vm.vacancy;
        var userName = localStorageService.get('authorizationData').userName;

        VacancyHttpService.getVacancy($routeParams.vid).then(function (result) {
            vm.vacancy = result;
        });

        vm.saveFeedback = function (feedback) {
            var body = {
                id: feedback.id,
                cardId: feedback.cardId,
                text: feedback.text,
                type: feedback.type
            }

            FeedbackHttpService.saveFeedback(body, $routeParams.vid, $routeParams.cid).then(function (result) {
                //                vm.techFeedback = result;
                feedback.id = result.id;
                feedback.date = result.update;
                feedback.userName = result.userName;
                //!!!!!!
//                if (vm.techFeedback.text == '') {
//                    vm.techFeedback.feedbackConfig = {
//                        'buttonName': 'Save',
//                        'readOnly': false
//                    }
//                } else {
//                    vm.techFeedback.feedbackConfig = {
//                        'buttonName': 'Edit',
//                        'readOnly': true
//                    }
//                }
                //!!!!!!!
                console.log("result after update");
                console.log(feedback);
            });
        }


        FeedbackHttpService.getTechFeedback($routeParams.vid, $routeParams.cid).then(function (result) {
            vm.techFeedback = result;

            if (vm.techFeedback.text == '') {
                vm.techFeedback.feedbackConfig = {
                    'buttonName': 'Save',
                    'readOnly': false
                }
            } else {
                vm.techFeedback.feedbackConfig = {
                    'buttonName': 'Edit',
                    'readOnly': true
                }
            }

            console.log(result);
        });

        vm.toggleReadOnly = function () {
            vm.techFeedback.feedbackConfig.readOnly = vm.techFeedback.text != '' ? 
                !vm.techFeedback.feedbackConfig.readOnly : vm.techFeedback.feedbackConfig.readOnly;

            if (vm.techFeedback.feedbackConfig.readOnly) {
                vm.techFeedback.feedbackConfig.buttonName = 'Edit';
                vm.saveFeedback(vm.techFeedback);
//                feedback.userName = userName;
//                feedback.date = new Date();
            } else {
                vm.techFeedback.feedbackConfig.buttonName = 'Save';
            }
        }

    }
})();