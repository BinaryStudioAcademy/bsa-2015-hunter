(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('LongListPreviewController', LongListPreviewController);

    LongListPreviewController.$inject = [
        '$scope',
        '$location',
        '$route',
        '$rootScope',
        'CandidateHttpService',
        'LonglistHttpService',
        'EnumConstants',
        'LonglistService'
    ];

    function LongListPreviewController($scope, $location, $route, $rootScope, candidateHttpService, longlistHttpService, EnumConstants, longlistService) {
        var vm = this;

        vm.hideCandidatePreview = hideCandidatePreview;
        vm.getCandidateDetails = getCandidateDetails;
        vm.removeCard = removeCard;
        vm.changeTemplate = changeTemplate;

        vm.isPreviewShown = false;
        vm.stages = EnumConstants.cardStages;

        vm.tabs = [
            { name: 'Overview', route: 'overview' },
            { name: 'Special Notes', route: 'specialnotes' },
            { name: 'Application results', route: 'appresults' }
        ];
        vm.currentTabName = vm.tabs[0];
        vm.templateToShow = '';

        $rootScope.$watch(
            '$root.candidatePreview.cid',
            function () {
                vm.vacancyId = $rootScope.candidatePreview.vid;

                if ($rootScope.candidatePreview.cid === 0) {
                    vm.isPreviewShown = false;
                } else {
                    getCandidateDetails($rootScope.candidatePreview.cid);
                    vm.isPreviewShown = true;
                }
            });

        function hideCandidatePreview() {
            vm.isPreviewShown = false;
        }

        function getCandidateDetails(id) {
            vm.prevLoad = true;
            candidateHttpService.getLongListDetails(id).then(function(result) {
                vm.candidateDetails = result;
                vm.prevLoad = false;
            });
        }

        function removeCard(cid) {
            longlistHttpService.removeCard(vm.vacancyId, cid);
            vm.isPreviewShown = false;
            $route.reload();
            //$timeout(vm.getCandidatesForLongList, 1200);
        }

        function changeTemplate(tab) {
            vm.currentTabName = tab.name;
            vm.templateToShow = longlistService.changeTemplate(tab.route);
            $location.search('tab', tab.route);
        }
    }
})();