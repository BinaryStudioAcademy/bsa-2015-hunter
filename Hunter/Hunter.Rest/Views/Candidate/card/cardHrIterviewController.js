(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardHrInterviewController', CardHrInterviewController);

    CardHrInterviewController.$inject = [
        'VacancyHttpService',
        'FeedbackHttpService',
        'CardHrInterviewService',
        '$routeParams',
        'EnumConstants'
    ];

    function CardHrInterviewController(VacancyHttpService, FeedbackHttpService, CardHrInterviewService,
        $routeParams, EnumConstants) {
        var vm = this;

        vm.templateName = 'HR Interview';
        vm.vacancy;
        vm.feedbacks;
        vm.newFeedback;

        // TODO: Initialization Should Be Covered with self invoke function
        VacancyHttpService.getLongList($routeParams.vid).then(function (result) {
            console.log(result);
            vm.vacancy = result;
        });

        FeedbackHttpService.getHrFeedback($routeParams.vid, $routeParams.cid).then(function (result) {
            console.log(result);
            vm.feedbacks = result;

            vm.feedbacks.forEach(function(feedback) {
                if (feedback.text == '') {
                    feedback.feedbackConfig = {
                        'buttonName': 'Save',
                        'readOnly': false
                    }
                } else {
                    feedback.feedbackConfig = {
                        'buttonName': 'Edit',
                        "readOnly": true
                    };
                }

                feedback.feedbackConfig.style = {
                    "border-color": feedback.successStatus == 0 ? EnumConstants.voteColors['None']
                        : feedback.successStatus == 1 ? EnumConstants.voteColors['Like']
                        : EnumConstants.voteColors['Dislike']
                }
            });
        });

        // TODO: Define event function at the beginning of controller and only then should be implementation vm.saveHrFeedback = saveHrFeedback; function saveHrFeedback() {}
        vm.saveHrFeedback = function (feedback) {
            var newFeedback = {
                id: feedback.id,
                cardId: feedback.cardId,
                type: feedback.type,
                text: feedback.text,
                successStatus: feedback.successStatus
            };

            FeedbackHttpService.saveHrFeedback(newFeedback, $routeParams.vid, $routeParams.cid).then(function (result) {
                console.log(result);
                feedback.id = result.id;
                feedback.date = result.update;
                feedback.userAlias = result.userAlias;
            });
        }

        vm.toggleReadOnly = function(feedback) {
            feedback.feedbackConfig.readOnly = feedback.text != '' ? !feedback.feedbackConfig.readOnly
                : feedback.feedbackConfig.readOnly;

            if (feedback.feedbackConfig.readOnly) {
                feedback.feedbackConfig.buttonName = 'Edit';
                vm.saveHrFeedback(feedback);
            } else {
                feedback.feedbackConfig.buttonName = 'Save';
            }
        }

        //vm.isDateShow = function (id) {
        //    return CardHrInterviewService.isDateShow(vm.feedbacks[id].date);
        //}

    }
})();