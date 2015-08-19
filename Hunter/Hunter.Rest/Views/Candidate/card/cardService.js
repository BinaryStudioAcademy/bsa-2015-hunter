(function() {
    'use strict';

    angular
        .module('hunter-app')
        .factory('CardService', CardService);

    CardService.$inject = [];

    function CardService() {
        var service = {
                changeTemplate: changeTemplate
            },
            viewsPath = '/Views/candidate/card/';

        function changeTemplate(templateRoute) {
            switch (templateRoute.toLowerCase()) {
                case 'overview':
                    return viewsPath + 'cardOverview.html';
                case 'specialnotes':
                    return  viewsPath + 'cardSpecialNotes.html';
                case 'hrinterview':
                    return  viewsPath + 'cardHrInterview.html';
                case 'technicalinterview':
                    return  viewsPath + 'cardTechnicalInterview.html';
                case 'test':
                    return viewsPath + 'cardTest.html';
                case 'summary':
                    return viewsPath + 'cardSummary.html';
                
                default:
                    return '';
            }
        }

        return service;
    }
})();