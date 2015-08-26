(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('LongListController', LongListController);

    LongListController.$inject = [
        '$location',
        'VacancyHttpService',
        'CandidateHttpService',
        'LonglistHttpService',
        'LonglistService',
        '$routeParams',
        '$odataresource',
        '$odata',
        '$filter',
        '$scope',
        'EnumConstants',
        '$timeout',
        '$rootScope'
        //'$on'
    ];

    function LongListController($location, vacancyHttpService, candidateHttpService, longlistHttpService, longlistService, $routeParams, $odataresource, $odata, $filter, $scope, EnumConstants, $timeout, $rootScope) {
        var vm = this;

        vm.stages = EnumConstants.cardStages;
        vm.shortlisted = true;

        vm.vacancy;
        vm.candidateDetails;
        vm.starUpdate = starUpdate;
        vm.viewCandidateInfo = viewCandidateInfo;
        vm.getCandidatesForLongList = getCandidatesForLongList;
        
        // get vacancy info
        vacancyHttpService.getLongList($routeParams.id).then(function (result) {
            vm.vacancy = result;
        });

        // get all Added by
        vacancyHttpService.getLongListAddedBy($routeParams.id).then(function (result) {
            vm.addedByList = result;
        });

        // click on candidate item shows candidates preview
        $rootScope.candidatePreview = {
            vid: $routeParams.id,
            cid: 0
        };

        vm.activateItemId = 0;
        function viewCandidateInfo(cid) {
            $rootScope.candidatePreview.cid = cid;
            vm.activateItemId = cid;
        }
        
        $scope.$on('hideCandidatePreview', function() {
            vm.activateItemId = 0;
        });

        // filtering/sorting candidates
        vm.pageSize = 5;
        vm.skip = 0;
        var predicate;
        vm.order;

        vm.sortOptions = [
            { text: 'Added Date (new first)', options: { field: 'AddDate', dir: 'desc' } },
            { text: 'Added Date (old first)', options: { field: 'AddDate', dir: 'asc' } },
            { text: 'Name (A-Z)', options: { field: 'FirstName', dir: 'asc' } },
            { text: 'Name (Z-A)', options: { field: 'FirstName', dir: 'desc' } }
        ];

        vm.order = vm.sortOptions[0].options;

        vm.filter = {
            search: '',
            shortlisted: false,
            stages: [],
            salary: [],
            hr: [],
            currentPage: 1
        };

        vm.name = 'CandidatesForLongList';
 
        var CandidatesForLongList = $odataresource('/api/candidates/longlist/' + $routeParams.id + '/odata');
        vm.listSpinner = false;
        function getCandidatesForLongList() {
            vm.listSpinner = true;
            var cands = CandidatesForLongList.odata()
                                            .withInlineCount()
                                            .take(vm.pageSize)
                                            .skip(vm.skip)
                                            .filter(predicate)
                                            .orderBy(vm.order.field, vm.order.dir)
                                            .query(function () {
                                                vm.candidatesList = cands.items;
                                                vm.listSpinner = false;
                                                if (vm.candidatesList.length > 0) {
                                                    vm.viewCandidateInfo(vm.candidatesList[0].id);
                                                }
                                                vm.totalItems = cands.count;
                                            });
        };

        // filters
        $scope.$watch('longListCtrl.filter', function () {
            var filt = [];

            if (vm.filter.search.length > 0) {
                var pred = $odata.Predicate.or([
                    new $odata.Func('substringof', new $odata.Property('tolower(\'' + vm.filter.search + '\')'), new $odata.Property('tolower(FirstName)')),
                    new $odata.Func('substringof', new $odata.Property('tolower(\'' + vm.filter.search + '\')'), new $odata.Property('tolower(LastName)')),
                    new $odata.Func('substringof', new $odata.Property('tolower(\'' + vm.filter.search + '\')'), new $odata.Property('tolower(Company)')),
                    new $odata.Func('substringof', new $odata.Property('tolower(\'' + vm.filter.search + '\')'), new $odata.Property('tolower(Location)'))
                ]);

                filt.push(pred);
            }

            if (vm.filter.hr.length > 0) {
                var hrPred = [];
                angular.forEach(vm.filter.hr, function (value, key) {
                    hrPred.push(new $odata.Predicate('AddedBy', value));
                });

                hrPred = $odata.Predicate.or(hrPred);
                filt.push(hrPred);
            }

            if (vm.filter.stages.length > 0) {
                var stPred = [];
                angular.forEach(vm.filter.stages, function (value, key) {
                    stPred.push(new $odata.Predicate('Stage', value));
                });

                stPred = $odata.Predicate.or(stPred);
                filt.push(stPred);
            }

            if (vm.filter.shortlisted.length > 0) {
                var slPred = [];
                angular.forEach(vm.filter.shortlisted, function (value, key) {
                    slPred.push(new $odata.Predicate('Shortlisted', value));
                });

                slPred = $odata.Predicate.or(slPred);
                filt.push(slPred);
            }

            if (filt.length > 0) {
                predicate = $odata.Predicate.and(filt);
            } else {
                predicate = undefined;
            }

            vm.skip = (vm.filter.currentPage - 1) * vm.pageSize;

            vm.getCandidatesForLongList();
        }, true);

        vm.getCandidatesForLongList();

        function starUpdate() {
            angular.forEach(vm.candidatesList, function (item) {
               if (item.id == vm.candidateDetails.id) {
                    item.shortlisted = vm.candidateDetails.shortlisted;
                }
            });
        };

       
    }
})();