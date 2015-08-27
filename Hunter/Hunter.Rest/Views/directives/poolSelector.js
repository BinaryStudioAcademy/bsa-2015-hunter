(function() {
    angular
        .module('hunter-app')
        .directive('poolSelector', PoolSelector);

    PoolSelector.$inject = [];

    function PoolSelector() {
        return {
            restrict: 'EA',
            link: function(scope, elem, attr, ctrl) {
            },
            scope: {
                'candidate': '=candidate'
            },
            controllerAs: 'poolSelectorCtrl',
            controller: ['$scope', 'CandidateHttpService', function ($scope, CandidateHttpService) {
                var vm = this;

                vm.addPoolToCandidate = function(pool) {
                    CandidateHttpService.addCandidatePool($scope.candidate.id, pool.id)
                        .then(function(data) {
                            $scope.candidate.poolNames.push(pool.name);
                            $scope.$apply();
                    });
                }
            }],
            template: 
                '<div style="width: auto; display: inline-block;">' +
                    '<div style="float: left; width: auto; display: inline-block;">' +
                        '<div ng-repeat="pool in candidate.poolNames" style="width: auto; float: left;' + '">' +
                        '{{pool}}</div>' +
                    '</div>' +
                    '<button id="addPoolBtn" class=" btn btn-default"><i class="fa fa-plus"></i></button>' +
                    '<div style="position: absolute; width: auto; background-color: white; z-index: 100" ng-controller="PoolGeneralController as generalCtrl">' +
                        '<div style="width: 380px;" ng-include="generalCtrl.link"></div>' +
                    '</div>' +
                '</div>'
        }
    }
})()