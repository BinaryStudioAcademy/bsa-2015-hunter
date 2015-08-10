(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CandidateListController', CandidateListController);

    CandidateListController.$inject = [
        '$location',
        'AuthService',
        'CandidateHttpService'
        

    ];

    function CandidateListController($location, authService, candidateHttpService) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        vm.name = "Candidates";
        vm.currentPage = 1;
        vm.pageSize = 10;
        //(function() {
        //    // This is function for initialization actions, for example checking auth
        //    if (authService.isLoggedIn()) {
        //    // Can Make Here Any Actions For Data Initialization, for example, http queries, etc.
        //    } else {
        //        $location.url('/login');
        //    }
        //})();

        vm.candidateList;
        // Here we should write any functions we need, for example, body of user actions methods.
        candidateHttpService.getCandidateList(function (data) {
            vm.candidateList = data.data;
            console.log(data.data);
        });
    }
})();