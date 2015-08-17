(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardHrInterviewController', CardHrInterviewController);

    CardHrInterviewController.$inject = [
        'VacancyHttpService',
        'FeedbackHttpService',
        'CardHrInterviewService',
        '$routeParams'
    ];

    function CardHrInterviewController(VacancyHttpService, FeedbackHttpService, CardHrInterviewService, $routeParams) {
        var vm = this;

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
                vm.feedbacks = result;
            });
        }

        //vm.isDateShow = function (id) {
        //    return CardHrInterviewService.isDateShow(vm.feedbacks[id].date);
        //}

    }
})();