(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardOverviewController', CardOverviewController);

    CardOverviewController.$inject = [
        'FeedbackHttpService',
        '$routeParams',
        'CardTestHttpService'
    ];

    function CardOverviewController(FeedbackHttpService, $routeParams, CardTestHttpService) {
        var vm = this;
       

        vm.feedbacks;
        vm.newFeedback;
        vm.techFeedback;
        vm.tests;

        vm.testFeedbacksPresent = false;

        FeedbackHttpService.getHrFeedback($routeParams.vid, $routeParams.cid).then(function (result) {
            vm.feedbacks = result;
        });

        FeedbackHttpService.getTechFeedback($routeParams.vid, $routeParams.cid).then(function (result) {
            vm.techFeedback = result;
        });

        CardTestHttpService.getTest($routeParams.vid, $routeParams.cid, function (result) {
            vm.tests = result.data;

            for (var i in vm.tests.tests) {
                var test = vm.tests.tests[i];
                if (vm.filterTests(test)) {
                    vm.testFeedbacksPresent = true;
                    break;
                }
            }
        });

        vm.filterTests = function(test) {

            if (test.feedbackId != null && test.feedback.text != '')
                return true;

            return false;
        }
    }
})();