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
        'EnumConstants',
        '$location',
        '$filter'
    ];

    function GeneralCardController($routeParams, $scope, candidateHttpService, cardService, enumConstants, $location, $filter) {
        var vm = this;
        vm.templateToShow = '';

        vm.tabs = [
            { name: 'Overview', route: 'overview' },
            { name: 'Special Notes', route: 'specialnotes' },
            { name: 'HR Interview', route: 'hrinterview' },
            { name: 'Technical Interview', route: 'technicalinterview' },
            { name: 'Test', route: 'test' },
            { name :'Summary', route: 'summary'}
        ];
        vm.currentTabName = vm.tabs[0];
        vm.candidate;
        vm.origins = enumConstants.origins;
        vm.resolutions = enumConstants.resolutions;
        vm.substatuses = enumConstants.substatuses;
        vm.feedbackTypes = enumConstants.feedbackTypes;
        vm.stages = enumConstants.cardStages;
        vm.currentStage = enumConstants.cardStages[0];
        vm.currentSubstatus = enumConstants.substatuses[0];
        console.log("rout", $routeParams);
        (function() {
            candidateHttpService.getCandidate($routeParams["cid"]).then(function (response) {
                vm.candidate = response.data;
                console.log(response.data);
            });
        })();
        

        vm.changeTemplate = function (tab) {
            vm.currentTabName = tab.name;
            vm.templateToShow = cardService.changeTemplate(tab.route);
            $location.search('tab', tab.route);      
        };

        vm.changeTemplate($filter('filter')(vm.tabs, { route: $location.search().tab }, true)[0]);
    }
})();