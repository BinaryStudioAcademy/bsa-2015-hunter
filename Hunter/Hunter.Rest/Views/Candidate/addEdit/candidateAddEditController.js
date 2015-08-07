(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CandidateAddEditController', CandidateAddEditController);

    CandidateAddEditController.$inject = [
        '$location',
        '$routeParams',
        'AuthService',
        'CandidateHttpService',
        'CandidateAddEditService',
        'PoolsHttpService'
    ];

    function CandidateAddEditController($location, $routeParams, authService, candidateHttpService, candidateAddEditService, poolsHttpService) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        //vm.categories = [{ name: 'Select Candidate Category' }]; // .NET, JS, PHP
        vm.origins = [{ name: 'Sourced', value: 0 }, { name: 'Applied', value: 1 }];
        vm.resolutions = [
            { name: 'None', value: 0 }, { name: 'Available', value: 1 }, { name: 'Not interested', value: 2 },
            { name: 'Hired', value: 3 }, { name: 'Unfit', value: 4 }, { name: 'Now now', value: 5 }
        ];
        vm.pools = null;
        vm.candidate = null;
        vm.errorObject = {
            emptyCategoryError: false
        }

        vm.selectedOrigin = vm.origins[0];
        vm.selectedResolution = vm.resolutions[0];
        vm.selectedPools = null;
        var id = null;
        var Pools = [];

        //Here we should write all signatures for user actions callback method, for example,
        vm.addEditCandidate = addEditCandidate;

        (function () {
            // This is function for initialization actions, for example checking auth
            if (true) {
                if ($routeParams.id) {
                    id = $routeParams.id;
                    initializeFields();
                }
                poolsHttpService.getAllPools(function (response) {
                    vm.pools = response.data;
                }, null);

                // Can Make Here Any Actions For Data Initialization, for example, http queries, etc.
            } else {
                $location.url('/login');
            }
        })();

        // Here we should write any functions we need, for example, body of user actions methods.
        function addEditCandidate() {

            var candidate = createCandidateRequestBody();
            if (candidate && candidate.Id!=null) {
                if (candidateAddEditService.validateData(candidate, vm.errorObject)) {
                    candidateHttpService.updateCandidate(candidate, successAddEditCandidate, candidate.Id);
                } else {
                    //alertify.error('Some Fields Are Incorrect');
                    alert('Some Fields Are Incorrect');
                }
            } else if (candidate) {
                if (candidateAddEditService.validateData(candidate, vm.errorObject)) {
                    candidateHttpService.addCandidate(candidate, successAddEditCandidate);
                } else {
                    //alertify.error('Some Fields Are Incorrect');
                    alert('Some Fields Are Incorrect');
                }
            }
        }

        // not user-event functions
        function createCandidateRequestBody() {
            var Origin = vm.selectedOrigin.value;

            var Resolution = vm.selectedResolution.value;

            Pools = [];
            for (var i = 0; i<vm.selectedPools.length; i++) {
                var pool = { id: vm.selectedPools[i].id, name: vm.selectedPools[i].name };
                Pools.push(pool);
            }
            var DateOfBirth = vm.DateOfBirth;

            // taking offset to account, otherwise a wrong date might be saved to database
            if (DateOfBirth.getTimezoneOffset > 0) {
                DateOfBirth.setMinutes(DateOfBirth.getMinutes() + DateOfBirth.getTimezoneOffset());
            } else {
                DateOfBirth.setMinutes(DateOfBirth.getMinutes() - DateOfBirth.getTimezoneOffset());
            }
           
            var candidate = {
                FirstName: vm.FirstName,
                LastName: vm.LastName,
                Email: vm.Email,
                CurrentPosition: vm.Position,
                Company: vm.Company,
                Location: vm.Location,
                Skype: vm.Skype,
                Phone: vm.Phone,
                Salary: vm.Salary,
                ResumeId: 1,
                Origin: Origin,
                Resolution: Resolution,
                ShortListed: vm.Shortlisted,
                DateOfBirth: DateOfBirth,
                Pools : Pools
            }
            if (id != null) {
                candidate.Id = id;
            }
            return candidate;
        }

        function initializeFields() {
            candidateHttpService.getCandidate(id, function(response) {
                vm.FirstName = response.data.firstName;
                vm.LastName = response.data.lastName;
                vm.Email = response.data.email;
                vm.Position = response.data.currentPosition;
                vm.Company = response.data.company;
                vm.Location = response.data.location;
                vm.Skype = response.data.skype;
                vm.Phone = response.data.phone;
                vm.Salary = response.data.salary;
                vm.selectedOrigin = vm.origins[response.data.origin];
                vm.selectedResolution = vm.resolutions[response.data.resolution];
                vm.selectedPools = response.data.pools;
                vm.ShortListed = response.data.shortlisted;
                vm.DateOfBirth = new Date(response.data.dateOfBirth);
            });
        }

        function successAddEditCandidate(data) {
            //            $location.url('/cadidates/list');
        }
    }
})();