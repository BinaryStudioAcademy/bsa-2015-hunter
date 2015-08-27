(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardTechnicalInterviewController', CardTechnicalInterviewController);

    CardTechnicalInterviewController.$inject = [
        'FeedbackHttpService',
        '$routeParams',
        'VacancyHttpService',
        'EnumConstants',
        '$scope'
    ];

    function CardTechnicalInterviewController(FeedbackHttpService, $routeParams, VacancyHttpService, EnumConstants, $scope) {
        var vm = this;
        vm.templateName = 'Technical Interview';
        vm.techFeedbacks = {};
        vm.newTechFeedback = {};
        vm.vacancy = {};
        vm.saveFeedback = saveFeedback;
        vm.updateFeedback = updateFeedback;
        vm.getFeedbacks = getFeedbacks;
        vm.getAllFeedbacks = getAllFeedbacks;
        vm.getMyFeedbacks = getMyFeedbacks;
        vm.voteColors = [];

        (function () {
            VacancyHttpService.getVacancy($routeParams.vid).then(function (result) {
                vm.vacancy = result;
            });

            vm.getFeedbacks();

            for (var x in EnumConstants.voteColors) {
                if (EnumConstants.voteColors.hasOwnProperty(x)) {
                    vm.voteColors.push(EnumConstants.voteColors[x]);
                }
            }

        })();

        function saveFeedback(feedback) {
            var body = {
                id: feedback.id,
                cardId: $scope.$parent.generalCardCtrl.cardInfo.cardId,
                text: feedback.text,
                type: 3,
                successStatus: feedback.successStatus
            }

            FeedbackHttpService.saveFeedback(body, $routeParams.vid, $routeParams.cid).then(function (result) {
                feedback.id = result.id;
                feedback.date = result.update;
                feedback.userAlias = result.userAlias;
                vm.techFeedbacks.push(result);
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
            FeedbackHttpService.getTechFeedback(0, $routeParams.cid).then(function (result) {
                vm.techFeedbacks = result;
            });
        }
        function getMyFeedbacks() {
            FeedbackHttpService.getTechFeedback(-1, $routeParams.cid).then(function (result) {
                vm.techFeedbacks = result;
            });
        }
        function getFeedbacks() {
            FeedbackHttpService.getTechFeedback($routeParams.vid, $routeParams.cid).then(function (result) {
                vm.techFeedbacks = result;
            });
        }

    }
})();