﻿(function() {
    angular
        .module('hunter-app')
        .directive('poolSelector', PoolSelector);

    PoolSelector.$inject = [];

    function PoolSelector() {
        return {
            restrict: 'EA',
//            link: function (scope, elem, attr, ctrl) {
//                $('#addPoolBtn').bind('click', function() {
//                    $('#selectPoolMain').toggleClass('hide');
//                });
//            },
            scope: {
                'candidate': '=candidate'
            },
            controllerAs: 'poolSelectorCtrl',
            controller: ['$scope', 'CandidateHttpService', function ($scope, CandidateHttpService) {
                var vm = this;
                vm.show = false;

                vm.toggleShow = function() {
                    vm.show = !vm.show;
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
            }],
            template: 
                '<div style="width: auto; display: inline-block;">' +
                    '<div class="pool-label-container">' +
                        '<div ng-repeat="pool in candidate.poolNames" class="pool-label" style="background-color: {{candidate.poolColors[pool.toLowerCase()]}};">' +
                        '{{pool}}</div>' +
                    '</div>' +
                    '<button id="addPoolBtn" ng-click="poolSelectorCtrl.toggleShow()" class=" btn btn-default"><i class="fa fa-plus"></i></button>' +
                    '<div id="selectPoolMain" ng-if="poolSelectorCtrl.show" class="pool-widget-container" ng-controller="PoolGeneralController as generalCtrl">' +
                        '<div style="width: 380px;" ng-include="generalCtrl.link"></div>' +
                    '</div>' +
                '</div>'
        }
    }
})()