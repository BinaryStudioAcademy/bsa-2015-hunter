(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardTestController', CardTestController);

    CardTestController.$inject = [];

    function CardTestController() {
        var vm = this;
        vm.templateName = 'Test';
    }
})();