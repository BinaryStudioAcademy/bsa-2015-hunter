
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

        var searchText = "";
        var pools = {
            1: false,
            2: false,
            3: false,
            4: false
        };

        var statuses = {
            0: false,
            1: false,
            2: false
        };

        var adders = [
            { 'name': "recruiter@local.com", 'isChecked': false },
            { 'name': "Heaven Hayden", 'isChecked': false },
            { 'name': "Chantel Sherley", 'isChecked': false }
        ];

        var options = {
            'poolFilters': pools,
            'adderFilters': adders,
            'statusFilters': statuses,
            'searchText':searchText
        };

        vm.filterOptions = options;


        vm.currentPage = 1;
        vm.pageSize = 20;//TODO change to 5
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