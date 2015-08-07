
﻿(function () {

    'use strict';

    angular
        .module('hunter-app')
        .controller('VacancyListController', VacancyListController);

    VacancyListController.$inject = [
        '$scope',
        'VacancyHttpService'
    ];

    function VacancyListController($scope, VacancyHttpService) {
        var vm = this;

        VacancyHttpService.getVacancies().then(function (result) {
            console.log(result);
            vm.vacancies = result;
        });
    }
})();