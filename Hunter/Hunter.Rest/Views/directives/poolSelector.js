(function() {
    angular
        .module('hunter-app')
        .directive('poolSelector', PoolSelector);

    PoolSelector.$inject = [];

    function PoolSelector() {
        return {
            restrict: 'EA',
            link: function (scope, elem, attr, ctrl) {
                if(scope.poolReadonly === 'false'){
                    $(document).click(function (event) {
                        if ($(event.target).closest('#addPoolBtn').length != 0) {
                            return;
                        }

                        if ($(event.target).hasClass('pool-label')) {
                            return;
                        }

                        //edit-link-search
                        //back-search
                        //add-pool-btn-search
                        if (hasClass(event.target) != '' && $(hasClass(event.target)).length == 0) {
                            return;
                        }

                        if ($(event.target).closest('#selectPoolMain').length != 0 && scope.poolSelectorCtrl.show)
                            return;

                        scope.poolSelectorCtrl.close();
                    });
                }

                function hasClass(elem) {
                    var search = ['edit-link-search', 'back-search', 'add-pool-btn-search'];
                    var res = '';

                    search.forEach(function(s) {
                        if ($(elem).hasClass(s)) {
                            res = s;
                        }
                    });

                    return res;
                }

            },
            scope: {
                'candidate': '=candidate',
                'poolReadonly': '@poolReadonly',
                'poolShort': '@poolShort'
            },
            controllerAs: 'poolSelectorCtrl',
            controller: ['$scope', 'CandidateHttpService', function ($scope, CandidateHttpService) {
                var vm = this;
                vm.show = false;
                $scope.wid = $scope.poolReadonly === 'true' ? '100%' : 'auto';
                vm.firstUse = true;

                vm.toggleShow = function () {
                    vm.show = !vm.show;
                    vm.firstUse = false;
                }

                vm.close = function() {
                    vm.show = false;
                    $scope.$apply();
                }

                vm.addPoolToCandidate = function (pool) {

                    if ($scope.candidate.poolNames.indexOf(pool.name) == -1) {
                        CandidateHttpService.addCandidatePool($scope.candidate.id, pool.id)
                            .then(function(data) {
                                $scope.candidate.poolNames.push(pool.name);

                                if (!(pool.name in $scope.candidate.poolColors)) {
                                    $scope.candidate.poolColors[pool.name.toLowerCase()] = pool.color;
                                }
                            });
                    } else {
                        CandidateHttpService.removeCandidatePool($scope.candidate.id, pool.id)
                            .then(function(data) {
                                var index = $scope.candidate.poolNames.indexOf(pool);
                                $scope.candidate.poolNames.splice(index, 1);
                                delete $scope.candidate.poolColors[pool.name.toLowerCase()];
                        });
                    }
                }

                vm.syncPools = function(pool, oldPool) {
                    var index = $scope.candidate.poolNames.indexOf(oldPool.name);
                    if(index != -1){
                        $scope.candidate.poolNames[index] = pool.name;
                        delete $scope.candidate.poolColors[oldPool.name.toLowerCase()];
                        $scope.candidate.poolColors[pool.name.toLowerCase()] = pool.color;
                    }
                }

                vm.removePoolFromCandidate = function(pool) {
                    var index = $scope.candidate.poolNames.indexOf(pool.name);
                    if (index != -1) {
                        $scope.candidate.poolNames.splice(index, 1);
                        delete $scope.candidate.poolColors[pool.name];
                    }

                    if (vm.labelClicked != '') {
                        vm.labelClicked = '';
                        vm.show = false;
                    }
                }

                vm.candidatePools = function() {
                    return $scope.candidate.poolNames;
                }

                vm.onLabelClick = function(pool) {
                    vm.show = true;
                    vm.firstUse = false;
                }
            }],
            template: 
                '<div style="width: auto; display: block;">' +
                    '<div style="width: {{wid}};" class="pool-label-container">' +
                        '<div ng-if="poolShort === \'false\' && poolReadonly === \'false\'">' +
                            '<div ng-repeat="pool in candidate.poolNames" class="pool-label" ng-click="poolSelectorCtrl.onLabelClick(pool)" style="background-color: {{candidate.poolColors[pool.toLowerCase()]}};">' +
                            '{{pool}}</div>' +
                        '</div>' +
                        '<div ng-if="poolShort === \'false\' && poolReadonly === \'true\'">' +
                            '<div ng-repeat="pool in candidate.poolNames" class="pool-label" style="background-color: {{candidate.poolColors[pool.toLowerCase()]}};">' +
                            '{{pool}}</div>' +
                        '</div>' +
                        '<div ng-if="poolShort === \'true\'">' +
                            '<div ng-if="poolShort === \'true\'" ng-repeat="pool in candidate.poolNames" class="pool-label-short" style="background-color: {{candidate.poolColors[pool.toLowerCase()]}};">' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                    '<button ng-if="poolReadonly === \'false\'" style="margin-left: 5px;" id="addPoolBtn" ng-click="poolSelectorCtrl.toggleShow()" class=" btn btn-default"><i class="fa fa-plus"></i></button>' +
                    '<div ng-if="!poolSelectorCtrl.firstUse && poolReadonly === \'false\'" id="selectPoolMain" ng-show="poolSelectorCtrl.show" class="pool-widget-container" ng-controller="PoolGeneralController as generalCtrl">' +
                        '<div style="width: 380px;" ng-include="generalCtrl.link"></div>' +
                    '</div>' +
                '</div>'
        }
    }
})()