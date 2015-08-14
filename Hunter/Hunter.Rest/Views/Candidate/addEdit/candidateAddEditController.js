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
        'PoolsHttpService',
        'UploadResumeService',
        'UploadPhotoService'
    ];

    function CandidateAddEditController($location, $routeParams, authService,
        candidateHttpService, candidateAddEditService, poolsHttpService, uploadResumeService, uploadPhotoService) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        //vm.categories = [{ name: 'Select Candidate Category' }]; // .NET, JS, PHP

        vm.nameInTitle = '';
        vm.origins = [{ name: 'Sourced', value: 0 }, { name: 'Applied', value: 1 }];
        vm.resolutions = [
            { name: 'None', value: 0 }, { name: 'Available', value: 1 }, { name: 'Not interested', value: 2 },
            { name: 'Hired', value: 3 }, { name: 'Unfit', value: 4 }, { name: 'Not now', value: 5 }
        ];
        vm.pools = [];
        vm.candidate = null;
        vm.errorObject = {
            emptyCategoryError: false,
            message :''
        }

        vm.selectedOrigin = vm.origins[0];
        vm.selectedResolution = vm.resolutions[0];
        vm.selectedPools = [];
        var id = null;
        var Pools = [];

        vm.photoLoaded = true;
        vm.picture = null;
        vm.pictureUrl = '';

        //Here we should write all signatures for user actions callback method, for example,
        vm.addEditCandidate = addEditCandidate;
        vm.previewSelected = previewSelected;
        vm.onFileSelect = uploadResumeService.onFileSelect;

        (function () {
            // This is function for initialization actions, for example checking auth
            if (true) {
                poolsHttpService.getAllPools().then(function(data) {
                    vm.pools = data;
                    initializeFields();
                });
                
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
                    alert('Some Fields Are Incorrect : ' + vm.errorObject.message);
                }
            } else if (candidate) {
                if (candidateAddEditService.validateData(candidate, vm.errorObject)) {
                    candidateHttpService.addCandidate(candidate, successAddEditCandidate);
                } else {
                    //alertify.error('Some Fields Are Incorrect');
                    alert('Some Fields Are Incorrect : ' + vm.errorObject.message);
                }
            }          
           
            vm.errorObject.message = '';
        }

        function previewSelected($file) {
            if (uploadPhotoService.validatePicture($file)) {
                vm.photoLoaded = false;
                vm.picture = $file;
            }
        }

        // not user-event functions
        function createCandidateRequestBody() {
            var Origin = vm.selectedOrigin.value;

            var Resolution = vm.selectedResolution.value;

            Pools = [];
            for (var i = 0; i<vm.selectedPools.length; i++) {
//                var pool = { id: vm.selectedPools[i].id, name: vm.selectedPools[i].name };
                Pools.push(vm.selectedPools[i].name);
            }
            
            if (vm.DateOfBirth) {
                var DateOfBirth = vm.DateOfBirth;
                // taking offset to account, otherwise a wrong date might be saved to database
                if (DateOfBirth.getTimezoneOffset > 0) {
                    DateOfBirth.setMinutes(DateOfBirth.getMinutes() + DateOfBirth.getTimezoneOffset());
                } else {
                    DateOfBirth.setMinutes(DateOfBirth.getMinutes() - DateOfBirth.getTimezoneOffset());
                }
            }
              
           
            var candidate = {
                FirstName: vm.FirstName,
                LastName: vm.LastName,
                Email: vm.Email,
                CurrentPosition: vm.Position,
                Company: vm.Company,
                Location: vm.Location,
                Skype: vm.Skype,
                Linkedin : vm.LinkedIn,
                Phone: vm.Phone,
                Salary: vm.Salary,
                ResumeId: 1,
                Origin: Origin,
                Resolution: Resolution,
                ShortListed: vm.ShortListed,
                DateOfBirth: DateOfBirth,
                PoolNames : Pools
            }
            if (id != null) {
                candidate.Id = id;
            }
            return candidate;
        }

        function initializeFields() {
            if ($routeParams.id) {
                id = $routeParams.id;
            } else {
                return;
            }   
            candidateHttpService.getCandidate(id).then(function(response) {
                vm.FirstName = response.data.firstName;
                vm.LastName = response.data.lastName;
                vm.Email = response.data.email;
                vm.Position = response.data.currentPosition;
                vm.Company = response.data.company;
                vm.Location = response.data.location;
                vm.Skype = response.data.skype;
                vm.Phone = response.data.phone;
                vm.LinkedIn = response.data.linkedin;
                vm.Salary = response.data.salary;
                vm.selectedOrigin = vm.origins[response.data.origin];
                vm.selectedResolution = vm.resolutions[response.data.resolution];
                vm.ShortListed = response.data.shortListed;
                vm.DateOfBirth = new Date(response.data.dateOfBirth);

                //getting already selected pools
                for (var i = 0; i < response.data.poolNames.length; i++) {
                    for (var j = 0; j < vm.pools.length; j++) {
                        if (response.data.poolNames[i] == vm.pools[j].name) {
                            vm.selectedPools.push(vm.pools[j]);
                        }
                    }
                }
                    
                candidateHttpService.getPictureUrl(id).then(function (response) {
                    if (response.status != 204) {
                        vm.pictureUrl = response.data;
                        vm.photoLoaded = true;
                    }
                });
                vm.nameInTitle = response.data.firstName + " " + response.data.lastName;
            });
        }

        function successAddEditCandidate(data) {
            debugger;
            uploadResumeService.uploadResume(data.data);
            uploadPhotoService.uploadPicture(vm.picture, data.data.id);
            $location.url('/candidate/list');
        }
    }
})();