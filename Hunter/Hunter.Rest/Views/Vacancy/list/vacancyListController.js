(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('vacancyListCtrl', vacancyListCtrl);

    vacancyListCtrl.$inject = [
        '$scope'
    ];

    function vacancyListCtrl($scope) {
        $scope.vacancy = "junior QA";
    }
})();