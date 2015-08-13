(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('GeneralCardController', GeneralCardController);

    GeneralCardController.$inject = [
        '$routeParams',
        '$scope',
        'CardService'
    ];

    function GeneralCardController($routeParams, $scope, cardService) {
        var vm = this;
        vm.templateToShow = '';
        vm.tabs = ['Overview', 'Special Notes', 'HR Interview', 'Technical Interview', 'Test'];

        vm.changeTemplate = changeTemplate;

        console.log($routeParams);

        function changeTemplate(templateName) {
            vm.templateToShow = cardService.changeTemplate(templateName);
        }
    }
})();