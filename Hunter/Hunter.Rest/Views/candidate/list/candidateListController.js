(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CandidateListController', CandidateListController);

    CandidateListController.$inject = [
       '$location',
        '$filter',
        '$scope',
        'AuthService',
        '$odataresource',
        'PoolsHttpService',
        '$odata'

    ];

    function CandidateListController($location, $filter, $scope, authService, $odataresource, PoolsHttpService, $odata) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        vm.name = "Candidates";

        vm.currentPage = 1;
        vm.pageSize = 5;
        vm.totalItems = 0;
        vm.skip = 0;
        vm.order = {
            field: 'FirstName',
            dir:'asc'
        }

        vm.pools = [];
        PoolsHttpService.getAllPools().then(function (result) {
            vm.pools = result;
        });

        vm.filter = {
            pools: [],
            inviters: []

        };

        var statuses = {
            'New': false,
            'InReview': false,
            'Hired': false
        };

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
                                .orderBy(vm.order.field,vm.order.dir)
                                .query(function () {
                                    vm.candidateList = cands.items;
                                    vm.totalItems = cands.count;
                                });
        }

        vm.sort = function(field) {
            vm.order.field = field;
            vm.order.dir = vm.order.dir == 'desc' ? 'asc' : 'desc';
            vm.getCandidates();
        }


        $scope.$watch('candidateListCtrl.filter', function () {
            var filt = [];

            if (vm.filter.pools.length > 0) {
                var poolPred = [];
                angular.forEach(vm.filter.pools, function (value, key) {
                    poolPred.push(new $odata.Predicate( new $odata.Property('PoolNames/any(p: p eq \'' + value +'\' )'), true));
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
    }
    }
})();