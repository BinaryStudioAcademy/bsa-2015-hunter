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
        'CandidateHttpService',
        'EnumConstants'
    ];

    function CandidatePartialController($scope, $location, $rootScope, authService, candidateHttpService, EnumConstants) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        vm.isEmpty = false;

        vm.candidate;
        vm.resolutions = EnumConstants.resolutions;
        vm.starUpdate = starUpdate;
        vm.updateResolution = updateResolution;

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

        function updateResolution() {
            candidateHttpService.updateCandidateResolution(vm.candidate.id, vm.candidate.resolution);
            angular.forEach($scope.$parent.candidateListCtrl.candidateList, function (item) {
                if (item.id == vm.candidate.id) {
                    item.resolution = vm.candidate.resolution;
                }
            });
        };
    }
})();