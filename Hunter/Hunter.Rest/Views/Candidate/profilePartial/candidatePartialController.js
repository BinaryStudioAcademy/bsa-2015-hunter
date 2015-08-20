(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CandidatePartialController', CandidatePartialController);

    CandidatePartialController.$inject = [
        '$scope',
        '$location',
        '$rootScope',
        'AuthService',
        'CandidateHttpService'
    ];

    function CandidatePartialController($scope, $location, $rootScope, authService, candidateHttpService) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        $rootScope.$watch(
            '$root.candidateDetails.id',
            function () {
                getCandidateDetails($rootScope.candidateDetails.id);
            });

        $rootScope.$watch(
            '$root.candidateDetails.shortListed',
            function () {
                if (vm.candidate) {
                    vm.candidate.shortListed = $rootScope.candidateDetails.shortListed;
                }
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

        $scope.$watch('candidatePartCtrl.candidate.shortListed', function () {
            if (vm.candidate) {
                angular.forEach($scope.$parent.candidateListCtrl.candidateList, function (item) {
                    if (item.id == vm.candidate.id) {
                        item.shortListed = vm.candidate.shortListed;
                    }
                });
            }
        });


        function getCandidateDetails(id) {
            candidateHttpService.getCandidate(id).then(function (response) {
                vm.candidate = response.data;
            });

        }
    }
})();