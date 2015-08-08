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

        var pools = {
            '.NET': true,
            'JS': true,
            'PHP': false,
            'QA': false
        };

        var inviters = [
            { 'name': 'Ulyana', 'id': 1, 'isChecked': true },
            { 'name': 'Kate', 'id': 2, 'isChecked': true }
        ];

        var options = {
            'poolFilters': pools,
            'inviterFilters': inviters
        };

        vm.filterOptions = options;

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

            fake();
        });

        function fake() {
            //pool fake
            vm.candidateList[0].pools.push({ 'name': '.NET' });
            vm.candidateList[1].pools.push({ 'name': 'JS' });
            vm.candidateList[1].pools.push({ 'name': '.NET' });

            //inviter fake
            vm.candidateList[0].addedByProfileId = 1;
            vm.candidateList[1].addedByProfileId = 2;
        }
    }
})();