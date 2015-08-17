(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CandidatePartialController', CandidatePartialController);

    CandidatePartialController.$inject = [
        '$location',
        '$rootScope',
        'AuthService',
        'CandidateHttpService'
    ];

    function CandidatePartialController($location, $rootScope, authService, candidateHttpService) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        $rootScope.$watch(
            '$root.candidateDetails.id',
            function () {
                getCandidateDetails($rootScope.candidateDetails.id);
            });

        vm.candidate;

        //(function() {
        //    // This is function for initialization actions, for example checking auth
        //    if (authService.isLoggedIn()) {
        //    // Can Make Here Any Actions For Data Initialization, for example, http queries, etc.
        //    } else {
        //        $location.url('/login');
        //    }
        //})();


        function getCandidateDetails(id) {
            if (id != null) {
                candidateHttpService.getCandidate(id).then(function (response) {
                    console.log(response.data);
                    vm.candidate = response.data;
                });
            } else {
                return;
            }

        }
    }
})();