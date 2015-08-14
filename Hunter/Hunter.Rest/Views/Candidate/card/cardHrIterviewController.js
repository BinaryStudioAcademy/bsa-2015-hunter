(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardHrInterviewController', CardHrInterviewController);

    CardHrInterviewController.$inject = [
        "VacancyHttpService",
        "$routeParams"
    ];

    function CardHrInterviewController(VacancyHttpService, $routeParams) {
        var vm = this;

        vm.templateName = 'HR Interview';
        vm.vacancy;

        VacancyHttpService.getLongList($routeParams.vid).then(function (result) {
            console.log(result);
            vm.vacancy = result;
        });
    }
})();