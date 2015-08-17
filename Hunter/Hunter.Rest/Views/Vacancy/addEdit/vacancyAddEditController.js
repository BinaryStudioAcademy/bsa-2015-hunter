(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('VacancyAddEditController', VacancyAddEditController);

    VacancyAddEditController.$inject = [
        '$location',
        '$routeParams',
        'AuthService',
        'VacancyHttpService',
        'PoolsHttpService',
        'EnumConstants'
    ];

    function VacancyAddEditController($location, $routeParams, authService, vacancyHttpService, poolsHttpService, enumConstants) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:

        vm.controllerName = 'Add / Edit Vacancy';
        vm.statuses = enumConstants.vacancyStates;
        vm.isNewVacancy = true;
        vm.currentVacancy = {
            id: 0,
            Name: '',
            StartDate: new Date(),
            EndDate: null,
            Location: '',
            Status: 0,
            Description: '',
            PoolId: 1
        };
        vm.pools = [];
        poolsHttpService.getAllPools().then(function (data) {
            vm.pools = data;
            initializeFields($routeParams);
        });

        vm.submitVacancy = function () {
            if (vacancyHttpService.validateVacancy(vm.currentVacancy)) {
                if (vm.isNewVacancy)
                {
                    vacancyHttpService.addVacancy(vm.currentVacancy);
                } else {
                    vacancyHttpService.updateVacancy(vm.currentVacancy, vm.currentVacancy.id);
                }
                $location.url('/vacancy/list');
            }
        };

        function initializeFields($routeParams) {
            var id = -1;
            if ($routeParams.id) {
                id = $routeParams.id;
            } else {
                return;
            }
            vacancyHttpService.getVacancy(id).then(function (data) {
                vm.isNewVacancy = false;
                vm.currentVacancy = {
                    id: id,
                    Name: data.name,
                    StartDate: new Date(data.startDate),
                    EndDate: new Date(data.endDate),
                    Location: data.location,
                    Status: data.status,
                    Description: data.description,
                    PoolId: data.poolId
                };
            });
            //if (vacancy != null) {
            //    console.log(vacancy.data);
            //    vm.isNewVacancy = false;
            //    vm.currentVacancy = {
            //        id: id,
            //        Name: vacancy.Name,
            //        StartDate: vacancy.StartDate,
            //        EndDate: vacancy.EndDate,
            //        Location: vacancy.Location,
            //        Status: vacancy.Status,
            //        Description: vacancy.Description,
            //        PoolId: vacancy.PoolId
            //    };
            //};
        }
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