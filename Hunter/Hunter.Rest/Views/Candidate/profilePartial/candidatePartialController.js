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
        'EnumConstants',
        'LonglistHttpService',
        'SpecialNoteHttpService',
        'CandidatePartialProfileService',
        'FeedbackHttpService',
        '$timeout',
        '$route'
    ];

    function CandidatePartialController($scope, $location, $rootScope, authService, candidateHttpService, EnumConstants,
        longlistHttpService, specialNoteHttpService, candidatePartialProfileService, feedbackHttpService, $timeout, $route) {
        var vm = this,
            firstUse = true;
        //Here we should write all vm variables default values. For Example:
        vm.isEmpty = false;

        vm.stages = EnumConstants.cardStages;
        vm.resolutions = EnumConstants.resolutions;
        vm.tabs = [
            { name: 'Overview', route: 'overview' },
            { name: 'Notes', route: 'specialnotes' },
            { name: 'App results', route: 'appresults' }
        ];
        vm.templateToShow = '';
        vm.candidate;
        vm.appResults = [];
        vm.specialNotes = [];
        vm.specialNotes = [];
        vm.overviews = [];
        vm.isLongList = false;
        vm.vacancyId = [];

        vm.updateResolution = updateResolution;
        vm.changeTemplate = changeTemplate;
        vm.showResume = showResume;
        vm.removeCard = removeCard;

        $scope.$on('candidateSelected',function(event, item, vacancyId){
                    if(vacancyId == undefined){
                        getCandidateDetails(item.id);
                        vm.tabs = [
                            { name: 'Notes', route: 'specialnotes' },
                            { name: 'App results', route: 'appresults' }
                        ];
                    }else{
                        getCandidateDetailsVacancy(item.id, vacancyId);
                        vm.tabs = [
                            { name: 'Overview', route: 'overview' },
                            { name: 'Notes', route: 'specialnotes' },
                            { name: 'App results', route: 'appresults' }
                        ];
                        vm.isLongList = true;                      
                    }
                })

        //getCandidateDetails(cardPreviewDirectiveService.candidate.id);
        /*$scope.$watch('cardPreviewDirectiveService.candidate.id',
            function(){
                if (!cardPreviewDirectiveService.candidate.id) {
                    getCandidateDetails(cardPreviewDirectiveService.candidate.id);
                };
            }
            );*/

        //vm.closeModal = closeModal;

        /*$rootScope.$watch(
            '$root.candidateDetails.id',
            function () {
                if (!$rootScope.candidateDetails.id) {
                    vm.isEmpty = true;
                } else {
                    getCandidateDetails($rootScope.candidateDetails.id);
                    vm.isEmpty = false;
                    vm.changeTemplate(vm.tabs[1]);
                }
            });*/

        $rootScope.$watch(
            '$root.candidateDetails.shortListed',
            function () {
                if (vm.candidate) {
                    vm.candidate.shortListed = $rootScope.candidateDetails.shortListed;
                }
            });



        function getCandidateDetails(id) {
            vm.prevLoad = true;
            candidateHttpService.getCandidate(id).then(function (response) {
                vm.candidate = response.data;        
            });

            longlistHttpService.getAppResults(id).then(function (response) {
                vm.appResults = response;
                vm.changeTemplate(vm.tabs[1]);
            });

            specialNoteHttpService.getCandidateSpecialNote(id).then(function (response) {
                vm.specialNotes = response.data;
                vm.prevLoad = false;
            });

        }


        function getCandidateDetailsVacancy(cid, vid) {
            vm.vacancyId = vid;
            vm.prevLoad = true;
            (function () {
                candidateHttpService.getLongListDetails(vid, cid).then(function (result) {
                    vm.candidate = result;
                    vm.prevLoad = false;

                });
                feedbackHttpService.getLastFeedbacks(vid, cid)
                    .then(function(result) {
                        vm.overviews = result;
                    });
                specialNoteHttpService.getCardSpecialNote(vid, cid)
                    .then(function (result) {
                        vm.specialNotes = result.data;
                        console.log(vm.specialNotes);
                    });
                longlistHttpService.getAppResults(cid).then(function (result) {
                    vm.appResults = result;
                    vm.changeTemplate(vm.tabs[2]);
                });
            })();


        }

        function updateResolution() {
            candidateHttpService.updateCandidateResolution(vm.candidate.id, vm.candidate.resolution);
            angular.forEach($scope.$parent.candidateListCtrl.candidateList, function (item) {
                if (item.id == vm.candidate.id) {
                    item.resolution = vm.candidate.resolution;
                }
            });
        };

        function removeCard(cid) {
            longlistHttpService.removeCard(vm.vacancyId, cid);
            
            $timeout(function () {
                $route.reload();
                //$scope.$emit('getCardsAfterCardDeleting');
            }, 1200);

            //$route.reload();
            //$scope.$emit('getCardsAfterCardDeleting');
        }

        function changeTemplate(tab) {
            vm.currentTabName = tab.name;
            vm.currentTabEmpty = candidatePartialProfileService.isCurrentTabEmpty(tab.route,vm.overviews, vm.specialNotes, vm.appResults);
            vm.templateToShow = candidatePartialProfileService.changeTemplate(tab.route);
        }

        function showResume() {
            window.open(vm.candidate.lastResumeUrl);
        }
    }
})();