
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
        '$filter',
        'VacancyHttpService'
    ];

    function VacancyListController($scope, $filter, VacancyHttpService) {
        var vm = this;

        var pools = {
            1: false,
            2: false,
            3: false,
            4: false
        };

        var statuses = {
            'New': false,
            'InReview': false,
            'Hired': false
        };

        var inviters = [
            { 'name': 'Ulyana', 'id': 1, 'isChecked': false },
            { 'name': 'Kate', 'id': 2, 'isChecked': false },
            { 'name': 'Ira', 'id': 3, 'isChecked': false }
        ];

        var options = {
            'poolFilters': pools,
            'inviterFilters': inviters,
            'statusFilters': statuses
        };

        vm.filterOptions = options;


        vm.currentPage = 1;
        vm.pageSize = 5;
        vm.vacancies;

        VacancyHttpService.getVacancies().then(function (result) {
            console.log(result);
            vm.vacancies = result;
        });

        function toggleCheckbox() {
            $filter('VacanciesFilter')(vm.vacancies, vm.filterOptions);
        }

        vm.toggle = toggleCheckbox;
    }

    function OtherController() {

    }
})();