(function() {
    angular
        .module('hunter-app')
        .directive('poolSelector', PoolSelector);

    PoolSelector.$inject = [];

    function PoolSelector() {
        return {
            restrict: 'EA',
            link: function (scope, elem, attr, ctrl) {
                $('#addPoolBtn').bind('click', function() {
                    $('#selectPoolMain').toggleClass('hide');
                });
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
                    '<div class="pool-label-container">' +
                        '<div ng-repeat="pool in candidate.poolNames" class="pool-label" style="background-color: {{candidate.poolColors[pool.toLowerCase()]}};">' +
                        '{{pool}}</div>' +
                    '</div>' +
                    '<button id="addPoolBtn" class=" btn btn-default"><i class="fa fa-plus"></i></button>' +
                    '<div id="selectPoolMain" class="pool-widget-container hide" ng-controller="PoolGeneralController as generalCtrl">' +
                        '<div style="width: 380px;" ng-include="generalCtrl.link"></div>' +
                    '</div>' +
                '</div>'
        }
    }
})()