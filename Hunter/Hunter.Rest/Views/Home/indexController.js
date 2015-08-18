(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('IndexController', IndexController);

    IndexController.$inject = [
        '$scope',
        'IndexHttpService',
        '$interval'
    ];

    function IndexController($scope, IndexHttpService, $interval) {
        var vm = this;
        vm.name = "Index";
        $scope.radioModel = 'Home';

        getActivityAmount();

        $interval(getActivityAmount, 180000);   //3 minutes

        function getActivityAmount() {
            IndexHttpService.getActivityAmount(function (response) {
                vm.amount = response.data;
            });
        }
    }
})();