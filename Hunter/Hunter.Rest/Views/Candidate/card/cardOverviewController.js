(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardOverviewController', CardOverviewController);

    CardOverviewController.$inject = [
        'FeedbackHttpService',
        '$routeParams'
    ];

    function CardOverviewController(FeedbackHttpService, $routeParams) {
        var vm = this;
       

        vm.feedbacks;
        vm.newFeedback;
        vm.techFeedback;

        FeedbackHttpService.getHrFeedback($routeParams.vid, $routeParams.cid).then(function (result) {
            vm.feedbacks = result;
        });

        FeedbackHttpService.getTechFeedback($routeParams.vid, $routeParams.cid).then(function (result) {
            console.log(result);
            vm.techFeedback = result;
        });

    }
})();