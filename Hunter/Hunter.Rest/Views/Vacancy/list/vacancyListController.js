
﻿(function () {

    'use strict';

    angular
        .module('hunter-app')
        .controller('VacancyListController', VacancyListController);
    angular
        .module('hunter-app')
        .controller('OtherController', OtherController);

    VacancyListController.$inject = [
        '$scope',
        'VacancyHttpService'
    ];

    function VacancyListController($scope, VacancyHttpService) {
        var vm = this;

        vm.currentPage = 1;
        vm.pageSize = 5;

        VacancyHttpService.getVacancies().then(function (result) {
            console.log(result);
            vm.vacancies = result;
        });
    }

    function OtherController() {

    }
})();