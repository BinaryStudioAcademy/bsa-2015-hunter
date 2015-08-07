(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('VacancyListController', VacancyListController);

    VacancyListController.$inject = [
        '$location',
        'AuthService',
        'CandidateHttpService'
        

    ];

    function VacancyListController($location, authService, candidateHttpService) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        vm.someVariable = 'This is list vacancy page';
        (function() {
            // This is function for initialization actions, for example checking auth
            if (authService.isLoggedIn()) {
            // Can Make Here Any Actions For Data Initialization, for example, http queries, etc.
            } else {
                $location.url('/login');
            }
        })();
        
        // Here we should write any functions we need, for example, body of user actions methods.

    }
})();