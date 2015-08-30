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
        'EnumConstants',
        '$scope'
    ];

    function CardHrInterviewController(VacancyHttpService, FeedbackHttpService, CardHrInterviewService,
        $routeParams, EnumConstants, $scope) {
        var vm = this;

        vm.templateName = 'HR Interview';
        vm.vacancy;
        vm.feedbacks;
        vm.newFeedback;
        vm.saveFeedback = saveFeedback;
        vm.updateFeedback = updateFeedback;
        vm.getFeedbacks = getFeedbacks;
        vm.getAllFeedbacks = getAllFeedbacks;
        vm.getMyFeedbacks = getMyFeedbacks;
        vm.voteColors = [];
        vm.HrFeedbackTypes = [];

        (function () {
            if ($scope.$parent.generalCardCtrl.isLLM) {
                VacancyHttpService.getVacancy($routeParams.vid).then(function (result) {
                    vm.vacancy = result;
                });
                vm.getFeedbacks();
            } else {
                vm.getAllFeedbacks();
            }

            for (var x in EnumConstants.voteColors) {
                if (EnumConstants.voteColors.hasOwnProperty(x)) {
                    vm.voteColors.push(EnumConstants.voteColors[x]);
                }
            }

            vm.HrFeedbackTypes = EnumConstants.feedbackTypes.slice(0, 2);

        })();


        function saveFeedback(feedback) {

            feedback.cardId = $scope.$parent.generalCardCtrl.cardInfo.cardId;

            FeedbackHttpService.saveFeedback(feedback, $routeParams.vid, $routeParams.cid).then(function (result) {
                feedback.id = result.id;
                feedback.date = result.update;
                feedback.userAlias = result.userAlias;
                vm.feedbacks.push(result);
            });
        }

        function updateFeedback(feedback) {
            if (!feedback.editMode) {
                feedback.editMode = !feedback.editMode;
            } else {
                feedback.editMode = !feedback.editMode;
                vm.saveFeedback(feedback);
            }
        }

        function getAllFeedbacks() {
            FeedbackHttpService.getHrFeedback(0, $routeParams.cid).then(function (result) {
                vm.feedbacks = result;
            });
        }
        function getMyFeedbacks() {
            FeedbackHttpService.getHrFeedback(-1, $routeParams.cid).then(function (result) {
                vm.feedbacks = result;
            });
        }
        function getFeedbacks() {
            FeedbackHttpService.getHrFeedback($routeParams.vid, $routeParams.cid).then(function (result) {
                vm.feedbacks = result;
            });
        }


    }
})();