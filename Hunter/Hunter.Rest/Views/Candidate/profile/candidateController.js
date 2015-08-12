﻿(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CandidateController', CandidateController);

    CandidateController.$inject = [
        '$location',
        'AuthService',
        'CandidateHttpService',
        'CandidateAddEditService'

    ];

    function CandidateController($location, authService, candidateHttpService, candidateAddEditService) {
        var vm = this;


        //(function() {
        //    // This is function for initialization actions, for example checking auth
        //    if (authService.isLoggedIn()) {
        //    // Can Make Here Any Actions For Data Initialization, for example, http queries, etc.
        //    } else {
        //        $location.url('/login');
        //    }
        //})();

        vm.candidate;
        candidateHttpService.getCandidate(getCandidateID($location.path())).then(function (response) {
            vm.candidate = response.data;
            console.log(response.data);
        });

        function getCandidateID(url) {
            return url.split('/').pop();
        }
    }
})();