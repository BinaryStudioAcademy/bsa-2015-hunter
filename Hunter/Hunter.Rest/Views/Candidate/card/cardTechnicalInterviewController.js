(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardTechnicalInterviewController', CardTechnicalInterviewController);

    CardTechnicalInterviewController.$inject = [];

    function CardTechnicalInterviewController() {
        var vm = this;
        vm.templateName = 'Technical Interview';
    }
})();