(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CandidateAddEditController', CandidateAddEditController);

    CandidateAddEditController.$inject = [
        '$location',
        'AuthService',
        'CandidateHttpService',
        'AddEditService'

    ];

    function CandidateAddEditController($location, authService, candidateHttpService, addEditService) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        vm.categories = [{ name: 'Select Candidate Category' }]; // .NET, JS, PHP
        vm.errorObject = {
            emptyCategoryError: false
        }
        vm.someVariable = 'This is add / edit candidate page';

        //Here we should write all signatures for user actions callback method, for example,
        vm.addEditCandidate = addEditCandidate;

        (function() {
            // This is function for initialization actions, for example checking auth
            if (authService.isLoggedIn()) {
            // Can Make Here Any Actions For Data Initialization, for example, http queries, etc.
            } else {
                $location.url('/login');
            }
        })();


        // Here we should write any functions we need, for example, body of user actions methods.
        function addEditCandidate(candidate) {
            if (candidate && candidate._id) {
                if (addEditService.validateData(candidate, vm.errorObject)) {
                    candidateHttpService.updateCandidate(candidate, successAddEditCandidate);
                } else {
                    alertify.error('Some Fields Are Incorrect');
                }
            } else if (candidate) {
                if (addEditService.validateData(candidate, vm.errorObject)) {
                    candidateHttpService.addCandidate(candidate, successAddEditCandidate);
                } else {
                    alertify.error('Some Fields Are Incorrect');
                }
            }
        }

        function successAddEditCandidate(data) {
            $location.url('/cadidates/list');
        }
    }
})();