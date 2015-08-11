(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('IndexController', IndexController);

    IndexController.$inject = [
        'IndexHttpService'
    ];

    function IndexController(IndexHttpService) {
        var vm = this;

        vm.name = "Index";
        IndexHttpService.getActivityAmount(function (response) {
            vm.amount = response.data;
        });
    }
})();