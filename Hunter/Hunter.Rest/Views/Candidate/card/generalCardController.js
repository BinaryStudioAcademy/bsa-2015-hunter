(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('GeneralCardController', GeneralCardController);

    GeneralCardController.$inject = [
        '$routeParams',
        '$scope',
        'CandidateHttpService',
        'CardService'
    ];

    function GeneralCardController($routeParams, $scope, candidateHttpService, cardService) {
        var vm = this;
        vm.templateToShow = '';
        vm.tabs = ['Overview', 'Special Notes', 'HR Interview', 'Technical Interview', 'Test'];
        vm.candidate;

        vm.changeTemplate = vm.tabs[0];

        console.log("rout",$routeParams);

        candidateHttpService.getCandidate($routeParams["cid"]).then(function (response) {
            vm.candidate = response.data;
            console.log(response.data);
        });


        $scope.$watch('generalCardCtrl.changeTemplate', function () {
            vm.templateToShow = cardService.changeTemplate(vm.changeTemplate);
            
        });

    }
})();