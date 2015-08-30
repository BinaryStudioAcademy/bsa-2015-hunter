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
        vm.poolChanged = poolChanged;
        vm.poolRemoved = poolRemoved;
        vm.getCandidatePools = getCandidatePools;
        vm.checkIsLabelClicked = checkIsLabelClicked;
        vm.changeLabel = changeLabel;

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

        function poolChanged(pool) {
            if ($scope.poolSelectorCtrl != undefined) {
                $scope.poolSelectorCtrl.syncPools(pool, vm.selectedPool);
            }
        }

        function poolRemoved(pool) {
            if ($scope.poolSelectorCtrl != undefined) {
                $scope.poolSelectorCtrl.removePoolFromCandidate(pool);
            }
        }

        function getCandidatePools() {
            if ($scope.poolSelectorCtrl != undefined) {
                return $scope.poolSelectorCtrl.candidatePools();
            } else {
                return [];
            }
        }

        function checkIsLabelClicked() {
            if ($scope.poolSelectorCtrl != undefined) {
                return $scope.poolSelectorCtrl.labelClicked;
            } else {
                return '';
            }
        }

        function changeLabel(pool, oldId) {
            $scope.poolSelectorCtrl.changePoolClick(pool, oldId);
        }
    }
})()