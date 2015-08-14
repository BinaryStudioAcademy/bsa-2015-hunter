(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('GeneralCardController', GeneralCardController);

    GeneralCardController.$inject = [
        '$routeParams',
        '$scope',
        'CandidateHttpService',
        'CardService',
        'EnumConstants'
    ];

    function GeneralCardController($routeParams, $scope, candidateHttpService, cardService, enumConstants) {
        var vm = this;
        vm.templateToShow = '';
        vm.tabs = ['Overview', 'Special Notes', 'HR Interview', 'Technical Interview', 'Test'];
        vm.candidate;
        vm.origins = enumConstants.origins;
        vm.resolutions = enumConstants.resolutions;
        vm.substatuses = enumConstants.substatuses;

        console.log("rout",$routeParams);
        (function() {
            candidateHttpService.getCandidate($routeParams["cid"]).then(function (response) {
                vm.candidate = response.data;
                console.log(response.data);
            });
        })();
        

        vm.changeTemplate = function (tab) {
            vm.templateToShow = cardService.changeTemplate(tab);
            
        };

    }
})();