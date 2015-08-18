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
        vm.defaultHrs = [
            { 'name': 'Ulyana', 'email': 'recruiter@local.com' },
            { 'name': 'Kate', 'email': 'recruiter2@local.com' },
            { 'name': 'Ira', 'email': 'recruiter3@local.com' }
        ];
        vm.stages = EnumConstants.cardStages;
        vm.shortlisted = true;

        vm.vacancy;
        // vm.candidates;
        vm.candidateDetails;

        // get vacancy info
        //VacancyHttpService.getLongList($routeParams.id).then(function (result) {
        //    console.log(result);
        //    vm.vacancy = result;
        //});

        // get all vacancies candidates
        CandidateHttpService.getLongList($routeParams.id).then(function (result) {
            console.log(result);
            vm.candidates = result;
        });

        // click on candidate item shows candidates preview
        vm.tabIsSet = function (checkTab) {
            return vm.tab === checkTab;
        };

        vm.tabSet = function (id) {
            vm.tab = id;
        }
        vm.viewCandidateInfo = function (id) {
            vm.tabSet(id);

            CandidateHttpService.getLongListDetails(id).then(function (result) {
                console.log(result);
                vm.candidateDetails = result;
            });
        }

        // filtering candidates
        vm.pageSize = 5;
        vm.skip = 0;
        var predicate;
        vm.order = {
            field: 'FirstName',
            dir: 'asc'
        }

        vm.filter = {
            search: '',
            shortlisted: false,
            stages: [],
            salary: [],
            location: '',
            hr: []
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
                                            //.OrderBy(vm.order.field, vm.order.dir)
                                            .query(function () {
                                                vm.candidatesList = cands.items;
                                                vm.totalItems = cands.count;
                                            });
            console.log(vm.candidatesList);
        };

        // seatch filter
        $scope.$watch('longListCtrl.filter', function () {
            var filt = [];

            if (vm.filter.search.length > 0) {
                var pred = $odata.Predicate.or([
                    new $odata.Func('substringof', new $odata.Property('tolower(\'' + vm.filter.search + '\')'), new $odata.Property('tolower(FirstName)')),
                    new $odata.Func('substringof', new $odata.Property('tolower(\'' + vm.filter.search + '\')'), new $odata.Property('tolower(LastName)'))
                ]);

                filt.push(pred);
            }

            if (vm.filter.hr.length > 0) {
                var hrPred = [];
                angular.forEach(vm.filter.hr, function(value, key) {
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

            if (vm.filter.location.length > 0) {
                var locationPred = $odata.Predicate.or([
                    new $odata.Func('substringof', new $odata.Property('tolower(\'' + vm.filter.location + '\')'), new $odata.Property('tolower(Location)'))
                ]);

                filt.push(locationPred);
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

            vm.getCandidatesForLongList();
        }, true);

        vm.getCandidatesForLongList();


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