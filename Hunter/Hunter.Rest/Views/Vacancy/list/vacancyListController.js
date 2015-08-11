
(function () {

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

        var statuses = {
            0: false,
            1: false
        };

        vm.currentPage = 1;
        vm.pageSize = 5;
        vm.vacancies;
        vm.pools;
        vm.users;


        VacancyHttpService.getVacancies().then(function (result) {
            console.log(result);
            vm.vacancies = result;
        });

        PoolsHttpService.getAllPools().then(function (result) {
            //console.log(result);
            vm.pools = result;
            vm.pools.forEach(function (pool) {
                pool.isChecked = false;
            });
            console.log(vm.pools);
        });

        UserHttpService.getUsersByRole('Recruiter').then(function (result) {
            //console.log(result);
            vm.users = result;
            vm.users.forEach(function (user) {
                user.isChecked = false;
            });
        });
        var options = {
            'statusFilters': statuses,
            'searchText': searchText
        };

        vm.filterOptions = options;
        $scope.$watchCollection(
            'VacancyListCtrl.filterOptions',
            function () {
                $filter('VacanciesFilter')(vm.vacancies, vm.filterOptions, vm.users, vm.pools);
            });
        $scope.$watchCollection(
            'VacancyListCtrl.pools',
            function() {
                $filter('VacanciesFilter')(vm.vacancies, vm.filterOptions, vm.users, vm.pools);
            });
        $scope.$watchCollection(
            'VacancyListCtrl.users',
            function() {
                $filter('VacanciesFilter')(vm.vacancies, vm.filterOptions, vm.users, vm.pools);
            });
    }

    function OtherController() {

    }
})();