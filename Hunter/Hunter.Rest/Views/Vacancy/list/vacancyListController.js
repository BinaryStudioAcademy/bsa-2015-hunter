
﻿(function () {

    'use strict';

    angular
        .module('hunter-app')
        .controller('VacancyListController', VacancyListController);

    VacancyListController.$inject = [
        '$scope',
        '$filter',
        'VacancyHttpService',
        'HttpHandler',
        '$q'
    ];

    function VacancyListController($scope, $filter, VacancyHttpService, HttpHandler,$q) {
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
            console.log(result);
            vm.vacancies = result;
        });

        getFilterInfo('Recruiter').then(function (result) {
            vm.pools = result.pools;
            vm.users = result.users;
        });

        function getFilterInfo(roleName) {
            var deferred = $q.defer();
            HttpHandler.sendRequest({
                url: '/api/vacancy/filterInfo/' + roleName,
                verb: 'GET',
                successCallback: function (result) {
                    deferred.resolve(result.data);
                },
                errorCallback: function (status) {
                    console.log("Get filter data error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }
    }
})();