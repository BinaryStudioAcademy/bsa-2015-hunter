(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('TestListController', TestListController);

    TestListController.$inject = [
        'TestHttpService'
    ];

    function TestListController(TestHttpService) {
        var vm = this;
        vm.tests = [];
        
        TestHttpService.getTestsNotChecked().then(function (result) {
            console.log(result);
            vm.tests = result;
        });
    }
})();