
﻿(function () {

    'use strict';

    angular
        .module('hunter-app')
        .controller('VacancyListController', VacancyListController);

    VacancyListController.$inject = [
        '$scope',
        '$filter',
        'VacancyHttpService',
        'PoolsHttpService'
    ];

    function VacancyListController($scope, $filter, vacancyHttpService, poolsHttpService) {
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

        vm.pools = [];
        poolsHttpService.getAllPools().then(function (data) {
            vm.pools = data;
        });

        vm.statuses = [
            { id: 0, name: 'Active' },
            { id: 1, name: 'Closed' },
            { id: 2, name: 'Burning' }
        ];
        //vm.adders = ["recruiter@local.com", "recruiter2@local.com", "recruiter3@local.com"];
        vm.sortBy = [
            { name: "Add Date (new first)", reverseSort: true, sortColumn: "startDate" },
            { name: "Add Date (old first)", reverseSort: false, sortColumn: "startDate" },
            { name: "Name (A-Z)", reverseSort: false, sortColumn: "name" },
            { name: "Name (Z-A)", reverseSort: true, sortColumn: "name" },
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

        getFilterInfo('Recruiter').then(function (result) {
            vm.pools = result.pools;
            vm.adders = result.users;
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