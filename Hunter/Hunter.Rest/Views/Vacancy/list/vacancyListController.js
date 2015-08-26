
﻿(function () {

    'use strict';

    angular
        .module('hunter-app')
        .controller('VacancyListController', vacancyListController);

    vacancyListController.$inject = [
        '$scope',
        '$filter',
        'VacancyHttpService',
        'PoolsHttpService',
        'EnumConstants'
    ];

    function vacancyListController($scope, $filter, vacancyHttpService, poolsHttpService, enumConstants) {
        var vm = this;

        vm.filterParams = {
            page: 1,
            pageSize: 5,
            sortColumn: 'startDate',
            reverceSort: true,
            filter: '',
            pool: [],
            status: [],
            addedBy: []
        };

        vm.totalCount = 0;
        vm.pools = [];
        poolsHttpService.getAllPools().then(function (data) {
            vm.pools = data;
        });

        vm.statuses = enumConstants.vacancyStates;

        vm.sortBy = [
            { name: "Add Date (new first)", reverseSort: true, sortColumn: "startDate" },
            { name: "Add Date (old first)", reverseSort: false, sortColumn: "startDate" },
            { name: "Name (A-Z)", reverseSort: false, sortColumn: "name" },
            { name: "Name (Z-A)", reverseSort: true, sortColumn: "name" }
        ];
        vm.sortAction = vm.sortBy[0];

        vm.vacancies = [];

        vm.pageChangeHandler = function (pageIndex) {
            vm.filterParams.page = pageIndex;
            vm.loadDataByParams(vm.filterParams);
        };

        vm.loadDataByParams = function () {
            vm.filterParams.sortColumn = vm.sortAction.sortColumn;
            vm.filterParams.reverceSort = vm.sortAction.reverseSort;
            vacancyHttpService.getFilteredVacancies(vm.filterParams).then(function (result) {
                vm.vacancies = result.rows;
                console.log(vm.vacancies);
                vm.totalCount = result.totalCount;
            });
        }

        $scope.$watch('vacancyListCtrl.filterParams', function () {
            vm.loadDataByParams();
        }, true);

        $scope.$watch('vacancyListCtrl.sortAction', function () {
            vm.loadDataByParams();
        }, true);

        vm.pushPopItem = function (item, collection) {
            if (collection == undefined) return;
            var index = collection.indexOf(item);
            if (index == -1) {
                collection.push(item);
            } else {
                collection.splice(index, 1);
            }
        }

        vacancyHttpService.getAddedByList().then(function (result) {
            vm.adders = result;
        });

        vm.pushPopItem(vm.statuses[1].id, vm.filterParams.status);
        vm.loadDataByParams();
    }
})();