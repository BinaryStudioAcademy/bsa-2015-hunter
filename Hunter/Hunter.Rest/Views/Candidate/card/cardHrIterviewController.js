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
        'localStorageService'
    ];

    function CardHrInterviewController(VacancyHttpService, FeedbackHttpService, CardHrInterviewService,
        $routeParams, localStorageService) {
        var vm = this;
        var userName = localStorageService.get('authorizationData').userName;

        vm.templateName = 'HR Interview';
        vm.vacancy;
        vm.feedbacks;
        vm.newFeedback;

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
            });
        });

        vm.saveHrFeedback = function (id, cardId, type, text) {
            vm.newFeedback = {
                id: id,
                cardId: cardId,
                type: type,
                text: text
            };

            FeedbackHttpService.saveHrFeedback(vm.newFeedback, $routeParams.vid, $routeParams.cid).then(function (result) {
                console.log(result);
//                vm.feedbacks = result;
            });
        }

        vm.toggleReadOnly = function(feedback) {
            feedback.feedbackConfig.readOnly = !feedback.feedbackConfig.readOnly;

            if (feedback.feedbackConfig.readOnly) {
                feedback.feedbackConfig.buttonName = 'Edit';
                vm.saveHrFeedback(feedback.id, feedback.cardId, feedback.type, feedback.text);
                feedback.date = new Date();
                feedback.userName = userName;
            } else {
                feedback.feedbackConfig.buttonName = 'Save';
            }
        }

        //vm.isDateShow = function (id) {
        //    return CardHrInterviewService.isDateShow(vm.feedbacks[id].date);
        //}

    }
})();