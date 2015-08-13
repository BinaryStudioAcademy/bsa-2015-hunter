(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardOverviewController', CardOverviewController);

    CardOverviewController.$inject = [];

    function CardOverviewController() {
        var vm = this;
        vm.templateName = 'Overview';
    }
})();