(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardOverviewController', CardOverviewController);

    CardOverviewController.$inject = [
        'FeedbackHttpService',
        '$routeParams',
        'CardTestHttpService',
        'EnumConstants'
    ];

    function CardOverviewController(FeedbackHttpService, $routeParams, CardTestHttpService, EnumConstants) {
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

            vm.feedbacks.forEach(function(feedback) {
                feedback.style = {
                    "border-color": feedback.successStatus == 0 ? EnumConstants.voteColors['None']
                            : feedback.successStatus == 1 ? EnumConstants.voteColors['Like']
                            : EnumConstants.voteColors['Dislike']
                }
            });
        });

        FeedbackHttpService.getTechFeedback($routeParams.vid, $routeParams.cid).then(function (result) {
            vm.techFeedback = result;
            vm.techFeedback.style = {
                "border-color": vm.techFeedback.successStatus == 0 ? EnumConstants.voteColors['None']
                            : vm.techFeedback.successStatus == 1 ? EnumConstants.voteColors['Like']
                            : EnumConstants.voteColors['Dislike']
            }
        });

        CardTestHttpService.getTest($routeParams.vid, $routeParams.cid, function (result) {
            vm.tests = result.data;

            for (var i in vm.tests.tests) {
                var test = vm.tests.tests[i];
                if (vm.filterTests(test)) {
                    vm.testFeedbacksPresent = true;
                    //                    break;
                    test.feedback.style = {
                        "border-color": test.feedback.successStatus == 0 ? EnumConstants.voteColors['None']
                            : test.feedback.successStatus == 1 ? EnumConstants.voteColors['Like']
                            : EnumConstants.voteColors['Dislike']
                    }
                }else {
                    test.feedback = {
                        'style': {
                            "border-color": EnumConstants.voteColors['None']
                        }
                    };
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
            vm.summary.style = {
                "border-color": vm.summary.successStatus == 0 ? EnumConstants.voteColors['None']
                            : vm.summary.successStatus == 1 ? EnumConstants.voteColors['Like']
                            : EnumConstants.voteColors['Dislike']
            }
        });
    }
})();