(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('vacancyListCtrl', vacancyListCtrl);

    vacancyListCtrl.$inject = [
        '$scope',
        'VacancyHttpService'
    ];

    function vacancyListCtrl($scope, VacancyHttpService) {
        VacancyHttpService.getVacancies().then(function (result) {
            $scope.vacancies = result;
        });
    }
})();