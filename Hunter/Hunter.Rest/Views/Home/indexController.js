(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('IndexController', IndexController);

    IndexController.$inject = [
        'IndexHttpService',
        '$interval'
    ];

    function IndexController(IndexHttpService, $interval) {
        var vm = this;

        vm.name = "Index";
        getActivityAmount();

        $interval(getActivityAmount, 180000);   //3 minutes

        function getActivityAmount() {
            IndexHttpService.getActivityAmount(function (response) {
                vm.amount = response.data;
            });
        }
    }
})();