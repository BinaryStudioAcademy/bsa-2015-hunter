(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardTechnicalInterviewController', CardTechnicalInterviewController);

    CardTechnicalInterviewController.$inject = [
        'FeedbackHttpService',
        '$routeParams',
        'VacancyHttpService',
        'EnumConstants'
    ];

    function CardTechnicalInterviewController(FeedbackHttpService, $routeParams, VacancyHttpService,
        EnumConstants) {
        var vm = this;
        vm.templateName = 'Technical Interview';
        vm.techFeedback;
        vm.vacancy;

        // TODO: Initialization Should Be covered with self invoke function
        VacancyHttpService.getVacancy($routeParams.vid).then(function (result) {
            vm.vacancy = result;
        });
        // TODO: Define event function at the beginning of controller and only then should be implementation vm.saveHrFeedback = saveHrFeedback; function saveHrFeedback() {}
        vm.saveFeedback = function (feedback) {
            var body = {
                id: feedback.id,
                cardId: feedback.cardId,
                text: feedback.text,
                type: feedback.type,
                successStatus: feedback.successStatus
            }

            FeedbackHttpService.saveFeedback(body, $routeParams.vid, $routeParams.cid).then(function (result) {
                feedback.id = result.id;
                feedback.date = result.update;
                feedback.userAlias = result.userAlias;
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

            vm.techFeedback.feedbackConfig.style = {
                "border-color": vm.techFeedback.successStatus == 0 ? EnumConstants.voteColors['None']
                                : vm.techFeedback.successStatus == 1 ? EnumConstants.voteColors['Like']
                                : EnumConstants.voteColors['Dislike']
            }

            console.log(result);
        });

        vm.toggleReadOnly = function () {
            vm.techFeedback.feedbackConfig.readOnly = vm.techFeedback.text != '' ? 
                !vm.techFeedback.feedbackConfig.readOnly : vm.techFeedback.feedbackConfig.readOnly;

            if (vm.techFeedback.feedbackConfig.readOnly) {
                vm.techFeedback.feedbackConfig.buttonName = 'Edit';
                vm.saveFeedback(vm.techFeedback);
            } else {
                vm.techFeedback.feedbackConfig.buttonName = 'Save';
            }
        }

    }
})();