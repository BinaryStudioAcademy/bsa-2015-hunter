(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardHrInterviewController', CardHrInterviewController);

    CardHrInterviewController.$inject = [];

    function CardHrInterviewController() {
        var vm = this;

        vm.templateName = 'HR Interview';
    }
})();