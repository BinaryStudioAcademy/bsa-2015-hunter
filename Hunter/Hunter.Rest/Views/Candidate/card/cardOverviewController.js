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
       
        // TODO: Initialization Should Be With Initial Value, at least [], {}, ''
        vm.feedbacks;
        vm.newFeedback;
        vm.techFeedback;
        vm.tests;
        vm.summary;

        vm.testFeedbacksPresent = false;

        // TODO: Initialization should be covered with self invoke function
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

        // TODO: Define event function at the beginning of controller and only then should be implementation vm.saveHrFeedback = saveHrFeedback; function saveHrFeedback() {}
        vm.filterTests = function(test) {

            if (test.feedbackId != null && test.feedback.text != '')
                return true;

            return false;
        }

        FeedbackHttpService.getSummary($routeParams.vid, $routeParams.cid).then(function (result) {
            console.log(result);
            vm.summary = result;
        });
    }
})();