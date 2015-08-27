(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('LongListPreviewController', LongListPreviewController);

    LongListPreviewController.$inject = [
        '$scope',
        '$location',
        '$route',
        '$rootScope',
        //'$emit',
        'CandidateHttpService',
        'LonglistHttpService',
        'EnumConstants',
        'LonglistService',
        'FeedbackHttpService',
        'CardTestHttpService',
        'SpecialNoteHttpService'
    ];

    function LongListPreviewController($scope, $location, $route, $rootScope, candidateHttpService, longlistHttpService, EnumConstants, longlistService, feedbackHttpService, cardTestHttpService, specialNoteHttpService) {
        var vm = this;

        vm.hideCandidatePreview = hideCandidatePreview;
        vm.getCandidateDetails = getCandidateDetails;
        vm.removeCard = removeCard;
        vm.changeTemplate = changeTemplate;
        vm.showResume = showResume;

        vm.isPreviewShown = false;
        vm.stages = EnumConstants.cardStages;
        vm.resolutions = EnumConstants.resolutions;

        vm.tabs = [
            { name: 'Overview', route: 'overview' },
            { name: 'Notes', route: 'specialnotes' },
            { name: 'App results', route: 'appresults' }
        ];
        vm.currentTabName = vm.tabs[0];
        vm.templateToShow = '';

        vm.overviews = [
            { title: 'Summary', text: '', date: '', user: '' },
            { title: 'English', text: '', date: '', user: '' },
            { title: 'Personal', text: '', date: '', user: '' },
            { title: 'Expertise', text: '', date: '', user: '' },
            { title: 'Tech Feedback', text: '', date: '', user: '' },
            { title: 'Test Feedback', text: '', date: '', user: '' }
        ];

        $rootScope.$watch(
            '$root.candidatePreview.cid',
            function () {
                vm.vacancyId = $rootScope.candidatePreview.vid;

                if ($rootScope.candidatePreview.cid === 0) {
                    vm.isPreviewShown = false;
                } else {
                    getCandidateDetails($rootScope.candidatePreview.cid);
                    vm.isPreviewShown = true;
                    changeTemplate(vm.tabs[2]);
                }
            });

        function hideCandidatePreview() {
            $scope.$emit('hideCandidatePreview');
            $rootScope.candidatePreview.cid = 0;
            vm.isPreviewShown = false;
        }

        vm.overviewText = '';
        vm.notes = [];
        vm.appResults = [];

        function getCandidateDetails(cid) {
            vm.prevLoad = true;
            (function () {
                candidateHttpService.getLongListDetails(vm.vacancyId, cid).then(function (result) {
                    vm.candidateDetails = result;
                    //console.log(result);
                });
            })();

            (function () {
                feedbackHttpService.getSummary(vm.vacancyId, cid).then(function (result) {
                    vm.overviews[0].text = result.text;
                    vm.overviews[0].date = result.date;
                    vm.overviews[0].user = result.userAlias;

                    vm.overviewText = result.text;
                });
            })();

            (function () {
                feedbackHttpService.getHrFeedback(vm.vacancyId, cid).then(function (result) {
                    for (var i = 0; i < 2; i++) {
                        vm.overviews[i + 1].text = result[i].text;
                        vm.overviews[i + 1].date = result[i].date;
                        vm.overviews[i + 1].user = result[i].userAlias;

                        vm.overviewText = result[i].text;
                    }
                });
            })();

            (function () {
                feedbackHttpService.getTechFeedback(vm.vacancyId, cid).then(function (result) {
                    vm.techFeedback = result;
                    vm.overviews[4].text = result.text;
                    vm.overviews[4].date = result.date;
                    vm.overviews[4].user = result.userAlias;

                    vm.overviewText = result.text;
                });
            })();

            //(function () {
            //    cardTestHttpService.getTest(vm.vacancyId, cid, function (result) {
            //        vm.tests = result.data;
            //        //console.log(result);
            //    });
            //})();

            (function () {
                specialNoteHttpService.getCardSpecialNote(vm.vacancyId, cid)
                    .then(function (result) {
                        for (var i = 0; i < result.data.length; i++) {
                            vm.notes.push({
                                text: result.data[i].text,
                                date: result.data[i].lastEdited,
                                user: result.data[i].userAlias
                            });
                        }
                    });
            })();

            (function() {
                longlistHttpService.getAppResults(cid).then(function (result) {
                    vm.appResults = result;
                    vm.prevLoad = false;
                });
            })();
        }

        function removeCard(cid) {
            longlistHttpService.removeCard(vm.vacancyId, cid);
            vm.isPreviewShown = false;
            $route.reload();
        }

        function changeTemplate(tab) {
            vm.currentTabName = tab.name;
            vm.currentTabEmpty = longlistService.isCurrentTabEmpty(tab.route, vm.overviewText, vm.notes);
            vm.templateToShow = longlistService.changeTemplate(tab.route);
        }

        function showResume() {
            window.open(vm.candidateDetails.lastResumeUrl);
        }
    }
})();