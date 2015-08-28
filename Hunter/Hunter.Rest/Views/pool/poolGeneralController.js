(function() {
    angular
        .module('hunter-app')
        .controller('PoolGeneralController', PoolGeneralController);

    PoolGeneralController.$inject = [
        '$scope'
    ];

    function PoolGeneralController($scope) {
        var vm = this;
        vm.link = 'Views/pool/list/list.html';
        vm.selectedPool = null;

        vm.selectPool = selectPool;
        vm.closeChooser = closeChooser;

        function selectPool(pool) {
            if ($scope.poolSelectorCtrl != undefined) {
                $scope.poolSelectorCtrl.addPoolToCandidate(pool);
            }
        }

        function closeChooser() {
            if ($scope.poolSelectorCtrl != undefined) {
                $scope.poolSelectorCtrl.toggleShow();
            } else {
                vm.link = '';
            }
        }
    }
})()