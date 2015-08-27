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
            controllerAs: 'poolSelectorCtrl',
            controller: function() {
                var vm = this;
                vm.addPoolToCandidate = function(pool) {
                    var a = pool.id;
                }
            },
            template: 
                '<div style="width: auto; display: inline-block;">' +
                    '<button id="addPoolBtn" class=" btn btn-default"><i class="fa fa-plus"></i></button>' +
                    '<div style="position: absolute; width: auto; background-color: white; z-index: 100" ng-controller="PoolGeneralController as generalCtrl">' +
                        '<div ng-include="generalCtrl.link"></div>' +
                    '</div>' +
                '</div>'
        }
    }
})()