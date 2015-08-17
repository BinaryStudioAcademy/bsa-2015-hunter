(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardTechnicalInterviewController', CardTechnicalInterviewController);

    CardTechnicalInterviewController.$inject = [
        'FeedbackHttpService',
        '$routeParams',
        'VacancyHttpService'
    ];

    function CardTechnicalInterviewController(FeedbackHttpService, $routeParams, VacancyHttpService) {
        var vm = this;
        vm.templateName = 'Technical Interview';
        vm.techFeedback;
        vm.vacancy;

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
                console.log("result after update");
                console.log(result);
            });
        }


        FeedbackHttpService.getTechFeedback($routeParams.vid, $routeParams.cid).then(function (result) {
            vm.techFeedback = result;
            console.log(result);
        });



    }
})();