(function() {
    'use strict';

    angular
        .module('hunter-app')
        .factory('CandidateService', CandidateService);

    CandidateService.$inject = [];

    function CandidateService() {
        var service = {
                changeTemplate: changeTemplate
            },
            candidateProfileViewsUrl = 'Views/candidate/profile/';

        function changeTemplate(templateName) {
            switch (templateName.toLowerCase()) {
                case 'applications':
                    return candidateProfileViewsUrl + 'profileApplications.html';
                case 'special notes':
                    return candidateProfileViewsUrl + 'profileSpecialNotes.html';
                case 'tests':
                    return candidateProfileViewsUrl + 'profileTests.html';
                case 'interviews':
                    return candidateProfileViewsUrl + 'profileInterviews.html';
                default:
                    return '';
            }
        }

        return service;
    }
})();