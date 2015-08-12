
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
        'PoolsHttpService'
    ];

    function VacancyListController($scope, $filter, vacancyHttpService, poolsHttpService) {
        var vm = this;

        vm.filterParams = {
            page: 0,
            pageSize: 5,
            sortColumn: 'startDate',
            reverceSort: true,
            filter: '',
            pool: [],
            status: [],
            addedBy: []
        };

        vm.pools = [];
        poolsHttpService.getAllPools().then(function (data) {
            vm.pools = data;
        });

        vm.statuses = [{ id: 0, name: 'Active' }, { id: 1, name: 'Closed' }, { id: 2, name: 'Burning' }];
        vm.adders = ["recruiter@local.com", "recruiter2@local.com", "recruiter3@local.com"];

        vm.vacancies = [];

        vm.loadDataByParams = function () {
            vacancyHttpService.getFilteredVacancies(vm.filterParams).then(function (result) {
                vm.vacancies = result;
            });
        }

        vm.pushPopItem = function (item, collection) {
            if (collection == undefined) return;
            var index = collection.indexOf(item);
            if (index == -1) {
                collection.push(item);
            } else {
                collection.splice(index, 1);
            }
        }

        vm.loadDataByParams();
    }

    function OtherController() {

    }
})();