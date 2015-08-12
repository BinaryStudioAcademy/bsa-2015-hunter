(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CandidateListController', CandidateListController);

    CandidateListController.$inject = [
        '$location',
        '$filter',
        '$scope',
        'AuthService',
        'CandidateHttpService'
        

    ];

    function CandidateListController($location, $filter, $scope, authService, candidateHttpService) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        vm.name = "Candidates";

        vm.currentPage = 1;
        vm.pageSize = 10;

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
            'statusFilters': statuses,
            'nameFilter': ''
        };

        vm.filterOptions = options;
        $scope.$watch(
            'candidateCtrl.filterOptions.nameFilter',
            function (newVal) {
                vm.filterOptions.nameFilter = newVal;
                $filter('CandidatesFilter')(vm.candidateList, vm.filterOptions);
            });

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
        candidateHttpService.getCandidateList().then(function (data) {
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
            vm.candidateList[0].poolNames.push({ 'name': '.NET' });
            vm.candidateList[1].poolNames.push({ 'name': '.NET' });
            vm.candidateList[2].poolNames.push({ 'name': '.NET' });
            vm.candidateList[3].poolNames.push({ 'name': 'JS' });
            vm.candidateList[4].poolNames.push({ 'name': 'JS' });
            vm.candidateList[5].poolNames.push({ 'name': 'JS' });
            vm.candidateList[6].poolNames.push({ 'name': 'PHP' });
            vm.candidateList[7].poolNames.push({ 'name': 'PHP' });
            vm.candidateList[8].poolNames.push({ 'name': 'PHP' });
            vm.candidateList[9].poolNames.push({ 'name': 'QA' });
            vm.candidateList[10].poolNames.push({ 'name': 'QA' });
            vm.candidateList[11].poolNames.push({ 'name': 'QA' });
            vm.candidateList[12].poolNames.push({ 'name': '.NET' });
            vm.candidateList[13].poolNames.push({ 'name': '.NET' });
            vm.candidateList[14].poolNames.push({ 'name': '.NET' });

            //results:
            //.NET -> Hollis Sefton; Porter Wystan; Gabe Raven; Gloria Delma; Deanne Imogene; Lon Abner;
            //JS -> Jack Sylvanus; Lindsay Darryl; Jennie Charlie
            //PHP -> Gracelyn Moriah; Elizabeth Rona; Sunny Fawn
            //QA -> Christianne Diantha; Allie Marideth; Kennedy Wardell


            //inviter fake
            vm.candidateList[0].addedByProfileId = 1;
            vm.candidateList[1].addedByProfileId = 2;
            vm.candidateList[2].addedByProfileId = 3;
            vm.candidateList[3].addedByProfileId = 1;
            vm.candidateList[4].addedByProfileId = 1;
            vm.candidateList[5].addedByProfileId = 1;
            vm.candidateList[6].addedByProfileId = 2;
            vm.candidateList[7].addedByProfileId = 2;
            vm.candidateList[8].addedByProfileId = 2;
            vm.candidateList[9].addedByProfileId = 3;
            vm.candidateList[10].addedByProfileId = 3;
            vm.candidateList[11].addedByProfileId = 3;
            vm.candidateList[12].addedByProfileId = 3;
            vm.candidateList[13].addedByProfileId = 3;
            vm.candidateList[14].addedByProfileId = 3;

            //results:
            //Ulyana -> Hollis Sefton; Jack Sylvanus; Lindsay Darryl; Jennie Charlie
            //Kate -> Porter Wystan; Gracelyn Moriah; Elizabeth Rona; Sunny Fawn
            //Ira -> Gabe Raven; Christianne Diantha; Allie Marideth; Kennedy Wardell; Gloria Delma; Deanne Imogene; Lon Abner;

            //statuses fake
            vm.candidateList[0].resolutionString = 'New';
            vm.candidateList[1].resolutionString = 'New';
            vm.candidateList[2].resolutionString = 'New';
            vm.candidateList[3].resolutionString = 'New';
            vm.candidateList[4].resolutionString = 'New';
            vm.candidateList[5].resolutionString = 'Hired';
            vm.candidateList[6].resolutionString = 'Hired';
            vm.candidateList[7].resolutionString = 'Hired';
            vm.candidateList[8].resolutionString = 'Hired';
            vm.candidateList[9].resolutionString = 'Hired';
            vm.candidateList[10].resolutionString = 'InReview';
            vm.candidateList[11].resolutionString = 'InReview';
            vm.candidateList[12].resolutionString = 'InReview';
            vm.candidateList[13].resolutionString = 'InReview';
            vm.candidateList[14].resolutionString = 'InReview';

            //result:
            //New -> Hollis Sefton; Porter Wystan; Gabe Raven; Jack Sylvanus; Lindsay Darryl;
            //Hired -> Jennie Charlie; Gracelyn Moriah; Elizabeth Rona; Sunny Fawn; Christianne Diantha;
            //InReview -> Allie Marideth; Kennedy Wardell; Gloria Delma; Deanne Imogene; Lon Abner;
        }
    }
})();