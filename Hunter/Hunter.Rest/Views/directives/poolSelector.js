(function() {
    angular
        .module('hunter-app')
        .directive('poolSelector', PoolSelector);

    PoolSelector.$inject = [];

    function PoolSelector() {
        return {
            restrict: 'EA',
            controller: 'PoolListController',
            controllerAs: 'poolCtrl',
            link: function(scope, elem, attr) {
                
            },
            template: 
                '<div style="width: auto; display: inline-block;">' +
                    '<button class=" btn btn-default"><i class="fa fa-plus"></i></button>' +
                    '<div style="position: absolute; width: auto; background-color: white; z-index: 100" ng-controller="PoolListController as poolCtrl">' +
                        '<div ng-include="\'Views/pool/list/list.html\'"></div>' +
                    '</div>' +
                '</div>'
        }
    }
})()