(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('LongListController', LongListController);

    LongListController.$inject = [
        '$location',
        'VacancyHttpService',
        'CandidateHttpService',
        '$routeParams',
        '$odataresource',
        '$odata',
        '$filter',
        '$scope',
        'EnumConstants'
    ];

    function LongListController($location, VacancyHttpService, CandidateHttpService, $routeParams, $odataresource, $odata, $filter, $scope, EnumConstants) {
        var vm = this;
        vm.tab = 0;

        vm.stages = EnumConstants.cardStages;
        vm.shortlisted = true;

        vm.vacancy;
        vm.candidateDetails;
        vm.starUpdate = starUpdate;

        // get vacancy info
        VacancyHttpService.getLongList($routeParams.id).then(function (result) {
            vm.vacancy = result;
        });

        // get all vacancies candidates
        //CandidateHttpService.getLongList($routeParams.id).then(function (result) {
        //    console.log(result);
        //    vm.candidates = result;
        //});

        // get all Added by
        vm.addedByList;
        VacancyHttpService.getLongListAddedBy($routeParams.id).then(function (result) {
            vm.addedByList = result;
        });

        // click on candidate item shows candidates preview
        vm.tabIsSet = function (checkTab) {
            return vm.tab === checkTab;
        };

        vm.ActiveItem = function(id) {
            if (vm.tabIsSet(id)) {
                return 'll_candidate_item_active';
            } else {
                return '';
            }
        }

        vm.previewTabSet = function (id) {
            vm.tab = id;
        }

        vm.viewCandidateInfo = function (id) {
            vm.previewTabSet(id);

            CandidateHttpService.getLongListDetails(id).then(function (result) {
                vm.candidateDetails = result;
            });
        }

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

        // 
        var CandidatesForLongList = $odataresource('/api/candidates/longlist/' + $routeParams.id + '/odata');

        vm.getCandidatesForLongList = function () {
            var cands = CandidatesForLongList.odata()
                                            .withInlineCount()
                                            .take(vm.pageSize)
                                            .skip(vm.skip)
                                            .filter(predicate)
                                            .orderBy(vm.order.field, vm.order.dir)
                                            .query(function () {
                                                vm.candidatesList = cands.items;
                    vm.viewCandidateInfo(vm.candidatesList[0].id);
                                                vm.totalItems = cands.count;
                                            });
            console.log('long list', vm.candidatesList);
        };

        // seatch filter
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

        //function setTab(setTab) {
        //    vm.tab = setTab;
        //};
        //Here we should write all vm variables default values. For Example:
        //vm.someVariable = 'This is datailse vacancy page';

        //(function() {
        //    // This is function for initialization actions, for example checking auth
        //    if (authService.isLoggedIn()) {
        //    // Can Make Here Any Actions For Data Initialization, for example, http queries, etc.
        //    } else {
        //        $location.url('/login');
        //    }
        //})();
        // Here we should write any functions we need, for example, body of user actions methods.

    }
})();