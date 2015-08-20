(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CandidateListController', CandidateListController);

    CandidateListController.$inject = [
        '$location',
        '$routeParams',
        '$filter',
        '$scope',
        '$rootScope',
        'AuthService',
        '$odataresource',
        'PoolsHttpService',
        '$odata',
        'EnumConstants',
        'CandidateHttpService',
        'LonglistHttpService',
        'VacancyHttpService'
    ];

    function CandidateListController($location, $routeParams, $filter, $scope, $rootScope, authService, $odataresource, PoolsHttpService, $odata, EnumConstants, CandidateHttpService, longlistHttpService, vacancyHttpService) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        vm.name = 'Candidates';

        $rootScope.candidateDetails = {
            id: null
        };

        // vm.currentPage = 1;
        vm.pageSize = 5;
        vm.totalItems = 0;
        vm.skip = 0;
        vm.order;

        vm.vacancy;
        vm.vacancyId;
        
        vm.pageConfig = {
            pageTitle: 'Candidates (general pool)',
            isAddToVacancyButton: true,
            locationAfterAdding: '/candidate/list'
    };

        if (!isObjectEmpty($routeParams)) {
            vm.vacancyId = $routeParams.addToVacancy;

            // get vacancy info
            vacancyHttpService.getLongList(vm.vacancyId).then(function (result) {
                console.log(result);
                vm.vacancy = result;
                vm.pageConfig.pageTitle = "Add Candidates to '" + vm.vacancy.name + "'";
            });

            vm.pageConfig.isAddToVacancyButton = false;
            vm.pageConfig.locationAfterAdding = '/vacancy/' + vm.vacancyId + '/longlist';
        }

        vm.pools = [];
        PoolsHttpService.getAllPools().then(function (result) {
            vm.pools = result;
        });

        vm.filter = {
            pools: [],
            inviters: [],
            statuses: [],
            search: '',
            currentPage: 1
        };

        vm.statuses = EnumConstants.resolutions;

        vm.inviters = [];

        vm.sortOptions = [
        { text: 'Name \u25BC', options: { field: 'FirstName', dir: 'asc' } },
        { text: 'Name \u25B2', options: { field: 'FirstName', dir: 'desc' } },
        { text: 'Added \u25BC', options: { field: 'AddedBy', dir: 'asc' } },
        { text: 'Added \u25B2', options: { field: 'AddedBy', dir: 'desc' } },
        { text: 'Status \u25BC', options: { field: 'Resolution', dir: 'asc' } },
        { text: 'Status \u25B2', options: { field: 'Resolution', dir: 'desc' } },
        { text: 'Email \u25BC', options: { field: 'Email', dir: 'asc' } },
        { text: 'Email \u25B2', options: { field: 'Email', dir: 'desc' } },
        { text: 'Years Of Experience \u25BC', options: { field: 'YearsOfExperience', dir: 'asc' } },
        { text: 'Years Of Experience \u25B2', options: { field: 'YearsOfExperience', dir: 'desc' } },
        { text: 'Company \u25BC', options: { field: 'Company', dir: 'asc' } },
        { text: 'Company \u25B2', options: { field: 'Company', dir: 'desc' } },
        { text: 'Location \u25BC', options: { field: 'Location', dir: 'asc' } },
        { text: 'Location \u25B2', options: { field: 'Location', dir: 'desc' } },
        { text: 'Salary \u25BC', options: { field: 'Salary', dir: 'asc' } },
        { text: 'Salary \u25B2', options: { field: 'Salary', dir: 'desc' } }
        ];

        vm.order = vm.sortOptions[0].options;

        CandidateHttpService.getAddedByList().then(function (result) {
            vm.inviters = result;
        });

        var predicate;

        var Candidates = $odataresource('/api/Candidates/odata');

        vm.getCandidates = function () {
            var cands = Candidates.odata()
                                .withInlineCount()
                                .take(vm.pageSize)
                                .skip(vm.skip)
                                .filter(predicate)
                                .orderBy(vm.order.field, vm.order.dir)
                                .query(function () {
                                    vm.candidateList = cands.items;
                                    $rootScope.candidateDetails.id = vm.candidateList[0].id;
                                    vm.totalItems = cands.count;
                                    console.log(vm.candidateList);
                                });
        }

        $scope.$watch('candidateListCtrl.filter', function () {
            var filt = [];

            if (vm.filter.pools.length > 0) {
                var poolPred = [];
                angular.forEach(vm.filter.pools, function (value, key) {
                    poolPred.push(new $odata.Predicate(new $odata.Property('PoolNames/any(p: p eq \'' + value + '\' )'), true));
                });

                poolPred = $odata.Predicate.or(poolPred);
                filt.push(poolPred);
            }

            if (vm.filter.inviters.length > 0) {
                var invPred = [];
                angular.forEach(vm.filter.inviters, function (value, key) {
                    invPred.push(new $odata.Predicate('AddedBy', value));
                });

                invPred = $odata.Predicate.or(invPred);
                filt.push(invPred);
            }

            if (vm.filter.statuses.length > 0) {
                var stPred = [];
                angular.forEach(vm.filter.statuses, function (value, key) {
                    stPred.push(new $odata.Predicate('Resolution', value));
                });

                stPred = $odata.Predicate.or(stPred);
                filt.push(stPred);
            }

            if (vm.filter.search.length > 0) {
                var pred = $odata.Predicate.or([
                    new $odata.Func('substringof', new $odata.Property('tolower(\'' + vm.filter.search + '\')'), new $odata.Property('tolower(FirstName)')),
                    new $odata.Func('substringof', new $odata.Property('tolower(\'' + vm.filter.search + '\')'), new $odata.Property('tolower(LastName)'))
                ]);

                filt.push(pred);
            }

            if (filt.length > 0) {
                predicate = $odata.Predicate.and(filt);
            } else {
                predicate = undefined;
            }

            vm.skip = (vm.filter.currentPage - 1) * vm.pageSize;
            vm.getCandidates();
        }, true);

        vm.ShowDetails = function (item) {
            // if ($rootScope.candidateDetails.id != id ){
            $rootScope.candidateDetails.id = item.id;
            $rootScope.candidateDetails.shortListed = item.shortListed;
            // } 
        }

        vm.ActiveTr = function (id) {
            if (id == $rootScope.candidateDetails.id) {
                return 'info';
            }
            else {
                return '';
            }
        }

        // signatures for user actions callback method
        vm.addCandidateToLongList = addCandidateToLongList;
        // user actions methods

        // add cardidates to Long List
        function addCandidateToLongList() {
            var cards = createCardRequestBody();
            //console.log(cards);
            longlistHttpService.addCards(cards, vm.pageConfig.locationAfterAdding);
        }

        // not user-event functions 
        vm.selectedCandidates = [];
        
        vm.vacancyByState;
        vm.vacancyStateId = EnumConstants.vacancyStates[1].id;

        vacancyHttpService.getVacancyByState(vm.vacancyStateId).then(function (result) {
            console.log(result);
            vm.vacancyByState = result;
        });

        function createCardRequestBody() {
            var cards = [];
            for (var i = 0; i < vm.selectedCandidates.length; i++) {
                cards.push({
                    CandidateId: vm.selectedCandidates[i],
                    VacancyId: vm.vacancyId,
                    Stage: EnumConstants.cardStages[0].id
                });
            }

            return cards;
        }

        function isObjectEmpty(obj) {
            return (Object.getOwnPropertyNames(obj).length === 0);
        }
    }

})();