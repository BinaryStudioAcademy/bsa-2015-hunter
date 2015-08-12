(function() {
    "use strict";

    angular
        .module("hunter-app")
        .controller("LongListController", LongListController);

    LongListController.$inject = [
        "$location",
        "VacancyHttpService",
        "$routeParams"
    ];

    function LongListController($location, VacancyHttpService, $routeParams) {
        var vm = this;

        vm.longList;

        VacancyHttpService.getLongList($routeParams.id).then(function (result) {
            console.log(result);
            vm.longList = result;
        });
        //Here we should write all vm variables default values. For Example:
        //vm.someVariable = "This is datailse vacancy page";

        //(function() {
        //    // This is function for initialization actions, for example checking auth
        //    if (authService.isLoggedIn()) {
        //    // Can Make Here Any Actions For Data Initialization, for example, http queries, etc.
        //    } else {
        //        $location.url('/login');
        //    }
        //})();
        // Here we should write any functions we need, for example, body of user actions methods.

    }
})();