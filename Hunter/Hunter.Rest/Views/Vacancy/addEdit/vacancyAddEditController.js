﻿(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('VacancyAddEditController', VacancyAddEditController);

    VacancyAddEditController.$inject = [
        '$location',
        'AuthService',
        'VacancyHttpService',
        'VacancyAddEditService',
        'PoolsHttpService'
    ];

    function VacancyAddEditController($location, authService, vacancyHttpService, vacancyAddEditService, poolsHttpService) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:

        vm.controllerName = 'Add / Edit Vacancy';
        vm.selectedPool = null;
        vm.newVacancy = {
            Name: 'New vacancy',
            Status: 0,
            StartDate: Date.Now,
            EndDate: null,
            Description: 'Description of vacancy',
            PoolId: 0
        };
        vm.pools = [];
        poolsHttpService.getAllPools(function (response) {
            vm.pools = response.data;
        }, null);

        vm.submitVacancy = function () {
            if (addEditService.validateData)
                addEditService.addData(vm.newVacancy);
            $location.url('/vacancy');
        };
        //Here we should write all signatures for user actions callback method, for example,

        //(function() {
        //    // This is function for initialization actions, for example checking auth
        //    if (authService.isLoggedIn()) {
        //    // Can Make Here Any Actions For Data Initialization, for example, http queries, etc.
        //    } else {
        //        $location.url('/login');
        //    }
        //})();


        // Here we should write any functions we need, for example, body of user actions methods.

    }
})();