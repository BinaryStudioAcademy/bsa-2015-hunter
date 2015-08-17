(function() {
    "use strict";

    angular
        .module("hunter-app")
        .controller("LongListController", LongListController);

    LongListController.$inject = [
        "$location",
        "VacancyHttpService",
        "CandidateHttpService",
        "$routeParams"
    ];

    function LongListController($location, VacancyHttpService, CandidateHttpService, $routeParams) {
        var vm = this;
        vm.tab = 0;

        vm.vacancy;
        vm.candidates;
        vm.candidateDetails;

        // get vacancy info
        VacancyHttpService.getLongList($routeParams.id).then(function (result) {
            console.log(result);
            vm.vacancy = result;
        });

        // get all vacancies candidates
        CandidateHttpService.getLongList($routeParams.id).then(function(result) {
            console.log(result);
            vm.candidates = result;
        });

        // click on candidate item shows candidates preview
        vm.tabIsSet = function (checkTab) {
            return vm.tab === checkTab;
        };

        vm.tabSet = function(id) {
            vm.tab = id;
        }
        vm.viewCandidateInfo = function (id) {
            vm.tabSet(id);

            CandidateHttpService.getLongListDetails(id).then(function (result) {
                console.log(result);
                vm.candidateDetails = result;
            });
        }

        // filtering candidates
        vm.filter = {
            search: '',
            shortlist: [],
            stages: [],
            salary: [],
            location: [],
            inviters: []
        };

        // seatch filter


        //function setTab(setTab) {
        //    vm.tab = setTab;
        //};
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