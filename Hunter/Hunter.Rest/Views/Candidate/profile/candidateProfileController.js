(function() {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CandidateProfileController', CandidateProfileController);

    CandidateProfileController.$inject = [
        '$location',
        '$routeParams',
        'AuthService',
        'CandidateHttpService',
        'CandidateAddEditService',
        'CandidateService'

    ];

    function CandidateProfileController($location, $routeParams, authService, candidateHttpService, candidateAddEditService, candidateService) {
        var vm = this;
        vm.isScinner = true;
        vm.subMenuItems = [
            {
                name: 'Applications',
                isActive: true
            }, {
                name: 'Special Notes',
                isActive: false
            }, {
                name: 'Tests',
                isActive: false
            }, {
                name: 'Interviews',
                isActive: false
            }
        ];

        vm.changeTemplate = changeTemplate;

        (function() {
            vm.templateToShow = candidateService.changeTemplate(vm.subMenuItems[0].name);
        } )();


        //(function() {
        //    // This is function for initialization actions, for example checking auth
        //    if (authService.isLoggedIn()) {
        //    // Can Make Here Any Actions For Data Initialization, for example, http queries, etc.
        //    } else {
        //        $location.url('/login');
        //    }
        //})();

        function changeTemplate(templateName) {
            vm.templateToShow = candidateService.changeTemplate(templateName);
            vm.subMenuItems.forEach(function(item, i, arr) {
                if (item === templateName) {
                    item.isActive = true;
                } else {
                    item.isActive = false;
                }
            });

        }

        vm.candidate;
        candidateHttpService.getCandidate(getCandidateID($location.path())).then(function (response) {
            vm.candidate = response.data;
            vm.isScinner = false;
            console.log(response.data);
        });

        function getCandidateID(url) {
            return url.split('/').pop();
        }
    }
})();