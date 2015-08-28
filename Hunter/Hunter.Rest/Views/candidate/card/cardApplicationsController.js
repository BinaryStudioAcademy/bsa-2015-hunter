(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('cardApplicationsController', cardApplicationsController);

    cardApplicationsController.$inject = [
        'LonglistHttpService',
        'EnumConstants',
        '$routeParams'
      
    ];

    function cardApplicationsController(longlistHttpService, EnumConstants, $routeParams) {
        var vm = this;

        vm.appResults = [];
        vm.stages = [];

        (function () {
            longlistHttpService.getAppResults($routeParams.cid).then(function (result) {
                vm.appResults = result;
            });

            vm.stages = EnumConstants.cardStages;
        })();


    };

})();