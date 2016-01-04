(function () {
    angular
        .module('hunter-app')
        .factory('CandidatePartialProfileService', CandidatePartialProfileService);

    CandidatePartialProfileService.$inject = [];

    function CandidatePartialProfileService() {
        var service = {
            isCurrentTabEmpty: isCurrentTabEmpty,
            changeTemplate: changeTemplate
        }
        var viewsPath = './Views/candidate/profilePartial/';

        function changeTemplate(templateRoute) {
            switch (templateRoute.toLowerCase()) {
                case 'overview':
                    return viewsPath + 'profilePartialOverview.html';
                case 'specialnotes':
                    return viewsPath + 'profilePartialSpecialNotes.html';
                case 'appresults':
                    return viewsPath + 'profilePartialAppResults.html';

                default:
                    return '';
            }
        }
        function isCurrentTabEmpty(route, overviews, notes, appresults) {
            switch (route.toLowerCase()) {
                case 'overview':
                    return overviews.length > 0 ? true : false;
                case 'specialnotes':
                    return notes.length > 0 ? true : false;
                case 'appresults':
                    return appresults.length > 0 ? true : false;
                default:
                    return false;
            }
        }
        return service;
    }
})();