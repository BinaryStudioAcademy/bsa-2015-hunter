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

        vm.saveFeedback = function (id, cardId, text, type) {
            var body = {
                id: id,
                cardId: cardId,
                text: text,
                type: type
            }
            FeedbackHttpService.saveFeedback(body, $routeParams.vid, $routeParams.cid).then(function (result) {
                vm.techFeedback = result;
                //!!!!!!
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
                //!!!!!!!
                console.log("result after update");
                console.log(result);
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
            vm.techFeedback.feedbackConfig.readOnly = !vm.techFeedback.feedbackConfig.readOnly;

            if (vm.techFeedback.feedbackConfig.readOnly) {
                vm.techFeedback.feedbackConfig.buttonName = 'Edit';
                vm.saveFeedback(vm.techFeedback.id, vm.techFeedback.cardId, vm.techFeedback.text, vm.techFeedback.type);
//                feedback.userName = userName;
//                feedback.date = new Date();
            } else {
                vm.techFeedback.feedbackConfig.buttonName = 'Save';
            }
        }

    }
})();