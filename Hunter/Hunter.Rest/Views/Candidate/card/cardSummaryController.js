(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardSummaryController', cardSummaryController);

    cardSummaryController.$inject = [
        'FeedbackHttpService',
        'VacancyHttpService',
        '$routeParams'
    ];

    function cardSummaryController(feedbackHttpService, vacancyHttpService, $routeParams) {
        var vm = this;
        vm.vacancy;
        vm.summary;

        vm.saveSummary  = function (id, cardId, text, type) {
            var body = {
                id: id,
                cardId: cardId,
                text: text,
                type: type
            }
            feedbackHttpService.saveSummary(body, $routeParams.vid, $routeParams.cid).then(function (result) {
                vm.summary = result;
                console.log("result after update");
                console.log(result);
            });
        }

        vacancyHttpService.getLongList($routeParams.vid).then(function (result) {
            console.log(result);
            vm.vacancy = result;
        });

        feedbackHttpService.getSummary($routeParams.vid, $routeParams.cid).then(function (result) {
            console.log(result);
            vm.summary = result;
        });
    }
})();