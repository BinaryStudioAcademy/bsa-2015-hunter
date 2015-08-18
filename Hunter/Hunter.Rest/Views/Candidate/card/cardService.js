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

        function changeTemplate(templateName) {
            switch (templateName.toLowerCase()) {
                case 'overview':
                    return viewsPath + 'cardOverview.html';
                case 'special notes':
                    return  viewsPath + 'cardSpecialNotes.html';
                case 'hr interview':
                    return  viewsPath + 'cardHrInterview.html';
                case 'technical interview':
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