(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CandidateListController', CandidateListController);

    CandidateListController.$inject = [
        '$location',
        '$filter',
        'AuthService',
        'CandidateHttpService'
        

    ];

    function CandidateListController($location, $filter, authService, candidateHttpService) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        vm.name = "Candidates";

        var pools = {
            '.NET': false,
            'JS': false,
            'PHP': false,
            'QA': false
        };

        var statuses = {
            'New': false,
            'InReview': false,
            'Hired': false
        };

        var inviters = [
            { 'name': 'Ulyana', 'id': 1, 'isChecked': false },
            { 'name': 'Kate', 'id': 2, 'isChecked': false },
            { 'name': 'Ira', 'id': 3, 'isChecked': false }
        ];

        var options = {
            'poolFilters': pools,
            'inviterFilters': inviters,
            'statusFilters': statuses
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

        function toggleCheckbox() {
            $filter('CandidatesFilter')(vm.candidateList, vm.filterOptions);
        }

        vm.toggle = toggleCheckbox;

        function fake() {
            //pool fake
            vm.candidateList[0].pools.push({ 'name': '.NET' });
            vm.candidateList[1].pools.push({ 'name': 'JS' });
            vm.candidateList[1].pools.push({ 'name': '.NET' });

            //inviter fake
            vm.candidateList[0].addedByProfileId = 1;
            vm.candidateList[1].addedByProfileId = 2;

            //statuses fake
            vm.candidateList[0].resolutionString = 'New';
            vm.candidateList[1].resolutionString = 'InReview';
            vm.candidateList[2].resolutionString = 'Hired';
        }
    }
})();