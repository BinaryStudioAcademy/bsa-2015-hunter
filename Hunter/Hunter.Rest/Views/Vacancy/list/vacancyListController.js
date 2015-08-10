
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
        'VacancyHttpService',
        'PoolsHttpService',
        'UserHttpService'
    ];

    function VacancyListController($scope, $filter, VacancyHttpService, PoolsHttpService, UserHttpService) {
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
            1: false
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
        $scope.$watchCollection(
            'VacancyListCtrl.filterOptions',
            function() {
                $filter('VacanciesFilter')(vm.vacancies, vm.filterOptions);
            });

        vm.currentPage = 1;
        vm.pageSize = 5;
        vm.vacancies;
        vm.pools;
        vm.users;

        VacancyHttpService.getVacancies().then(function (result) {
            //console.log(result);
            vm.vacancies = result;
        });

        PoolsHttpService.getAllPools().then(function (result) {
            //console.log(result);
            vm.pools = result;
        });

        UserHttpService.getUsersByRole('Recruiter').then(function (result) {
            //console.log(result);
            vm.users = result;
        });


    }

    function OtherController() {

    }
})();