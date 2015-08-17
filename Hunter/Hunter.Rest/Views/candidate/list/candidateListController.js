(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CandidateListController', CandidateListController);

    CandidateListController.$inject = [
       '$location',
        '$filter',
        '$scope',
        '$rootScope',
        'AuthService',
        '$odataresource',
        'PoolsHttpService',
        '$odata',
        'EnumConstants'


    ];

    function CandidateListController($location, $filter, $scope, $rootScope, authService, $odataresource, PoolsHttpService, $odata, EnumConstants) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        vm.name = "Candidates";

        $rootScope.candidateDetails = {
                        show: false,
                        id: null
                    };

        vm.currentPage = 1;
        vm.pageSize = 5;
        vm.totalItems = 0;
        vm.skip = 0;
        vm.order = {
            field: 'FirstName',
            dir: 'asc'
        }

        vm.pools = [];
        PoolsHttpService.getAllPools().then(function (result) {
            vm.pools = result;
        });

        vm.filter = {
            pools: [],
            inviters: [],
            statuses: [],
            search: ''
        };

        vm.statuses = EnumConstants.resolutions;

        vm.inviters = [
            { 'name': 'Ulyana', 'email': 'recruiter@local.com' },
            { 'name': 'Kate', 'email': 'recruiter2@local.com' },
            { 'name': 'Ira', 'email': 'recruiter3@local.com' }
        ];

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
                                    vm.totalItems = cands.count;
                                });
        }

        vm.sort = function (field) {
            vm.order.field = field;
            vm.order.dir = vm.order.dir == 'desc' ? 'asc' : 'desc';
            vm.getCandidates();
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
                    invPred.push(new $odata.Predicate("AddedBy", value));
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
            vm.getCandidates();
        }, true);


        $scope.$watch('candidateListCtrl.currentPage', function () {
            vm.skip = (vm.currentPage - 1) * vm.pageSize;
            vm.getCandidates();
        });

        vm.getCandidates();

        vm.ShowDetails = function (id) {
            if ($rootScope.candidateDetails.id === id && $rootScope.candidateDetails.show === true) {
                $rootScope.candidateDetails.show = false;
            } else {
                $rootScope.candidateDetails.show = true;
                $rootScope.candidateDetails.id = id;
            }
        }

    }

})();