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

        FeedbackHttpService.getHrFeedback($routeParams.vid, $routeParams.cid).then(function (result) {
            vm.feedbacks = result;
        });

        FeedbackHttpService.getTechFeedback($routeParams.vid, $routeParams.cid).then(function (result) {
            console.log(result);
            vm.techFeedback = result;
        });

        CardTestHttpService.getTest($routeParams.vid, $routeParams.cid, function (response) {
            vm.tests = response.data;
            console.log(response);
        });
    }
})();