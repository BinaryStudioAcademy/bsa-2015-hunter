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
        vm.isEmpty = false;


        $rootScope.$watch(
            '$root.candidateDetails.id',
            function () {
                if ($rootScope.candidateDetails.id == 0) {
                    vm.isEmpty = true;
                } else {
                    getCandidateDetails($rootScope.candidateDetails.id);
                    vm.isEmpty = false;
                }
            });

        $rootScope.$watch(
            '$root.candidateDetails.shortListed',
            function () {
                if (vm.candidate) {
                    vm.candidate.shortListed = $rootScope.candidateDetails.shortListed;
                }
            });

        vm.candidate;
        vm.starUpdate = starUpdate;

        function starUpdate() {
            if (vm.candidate) {
                angular.forEach($scope.$parent.candidateListCtrl.candidateList, function (item) {
                    if (item.id == vm.candidate.id) {
                        item.shortListed = vm.candidate.shortListed;
                    }
                });
            }
        };


        function getCandidateDetails(id) {
            vm.prevLoad = true;
            candidateHttpService.getCandidate(id).then(function (response) {
                vm.candidate = response.data;
                vm.prevLoad = false;
            });

        }
    }
})();