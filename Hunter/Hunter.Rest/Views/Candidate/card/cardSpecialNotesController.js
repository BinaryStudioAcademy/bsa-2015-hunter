(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardSpecialNotesController', CardSpecialNotesController);

    CardSpecialNotesController.$inject = [];

    function CardSpecialNotesController() {
        var vm = this;
        vm.templateName = 'Special Notes';
    }
})();