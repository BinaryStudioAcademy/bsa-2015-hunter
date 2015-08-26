(function () {
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
        '$filter',
        'LonglistHttpService',
        '$timeout',
        'VacancyHttpService'
    ];

    function GeneralCardController($routeParams, $scope, candidateHttpService, cardService, enumConstants, $location, $filter, longlistHttpService, $timeout, VacancyHttpService) {
        var vm = this;
        vm.templateToShow = '';
        vm.isLoad = true;

        vm.tabs = [];
        vm.currentTabName = {};
        vm.candidate = {};
        vm.origins = enumConstants.origins;
        vm.resolutions = enumConstants.resolutions;
        vm.substatuses = enumConstants.substatuses;
        vm.feedbackTypes = enumConstants.feedbackTypes;
        vm.stages = enumConstants.cardStages;
        vm.currentStage = {};
        vm.currentSubstatus = enumConstants.substatuses[0];
        vm.removeCard = removeCard;
        vm.updateResolution = updateResolution;
        vm.changeTemplate = changeTemplate;
        vm.vacancy = {};
        vm.isLLM = false;

        var vid = $routeParams["vid"];
        var cid = $routeParams["cid"];

        if (vid) {
            vm.isLLM = true;
        }

        (function () {
            candidateHttpService.getCandidate(cid).then(function (response) {
                vm.candidate = response.data;
            });
        })();

        if (vm.isLLM) {

            (function () {
                vm.tabs = [
                    { name: 'Overview', route: 'overview' },
                    { name: 'Special Notes', route: 'specialnotes' },
                    { name: 'HR Interview', route: 'hrinterview' },
                    { name: 'Technical Interview', route: 'technicalinterview' },
                    { name: 'Test', route: 'test' },
                    { name: 'Summary', route: 'summary' }
                ];
                vm.currentTabName = vm.tabs[0];
                cardService.getCardStage(vid, cid).then(function (response) {
                    vm.currentStage = enumConstants.cardStages[response.data];
                    vm.isLoad = true;
                });
                VacancyHttpService.getVacancy(vid).then(function (response) {
                    vm.vacancy = response;
                });
            })();
        } else {
            (function() {
                vm.tabs = [
                   { name: 'Overview', route: 'overview' },
                   { name: 'Special Notes', route: 'specialnotes' },
                   { name: 'Test', route: 'test' },
                ];
                vm.currentTabName = vm.tabs[0];
            })();
        }

        // TODO: Define event function at the beginning of controller and only then should be implementation vm.saveHrFeedback = saveHrFeedback; function saveHrFeedback() {}
        function changeTemplate(tab) {
            vm.currentTabName = tab.name;
            vm.templateToShow = cardService.changeTemplate(tab.route);
            $location.search('tab', tab.route);
        };

        vm.changeTemplate($filter('filter')(vm.tabs, { route: $location.search().tab }, true)[0]);

        $scope.$watch('generalCardCtrl.currentStage', function () {
            if (!vm.isLoad)
                updateCardStage();
            vm.isLoad = false;
        }, true);

        function updateCardStage() {
            cardService.updateCardStage(vid, cid, vm.currentStage.id);
        }

        function removeCard(cid) {
            longlistHttpService.removeCard(vid, cid);
            $timeout($location.url('/vacancy/' + vid + '/longlist'), 1200);
        }

        function updateResolution() {
            candidateHttpService.updateCandidateResolution($routeParams.cid, vm.candidate.resolution);
        };
    }
})();