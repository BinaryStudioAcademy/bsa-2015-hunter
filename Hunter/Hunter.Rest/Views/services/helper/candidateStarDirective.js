﻿(function () {
    'use strict';
    angular
        .module('hunter-app')
        .directive('shorListStar',shorListStar);

    shorListStar.$inject = ['CandidateHttpService'];

    function shorListStar(CandidateHttpService) {
            return {
                template: '<i class="fa " ' +
                    'ng-class="isShort ? \'fa-star\' : \'fa-star-o\' "' +
                    'ng-click="update()" ' +
                    'style="position: absolute;font-size:{{size}};top: 2px;left: 2px;color: gold;"' +
                    '></i>',
                restrict: 'E',
                scope: {
                    isShort: "=isShort",
                    candidateId: "=candidateId",
                    size:"=size"
                },
                link: function (scope, element, attrs, ctrl, transclude) {
                    

                    scope.update = function() {
                       scope.isShort = !scope.isShort;
                       CandidateHttpService.setShortListFlag(scope.candidateId,scope.isShort);
                    }

                }
            }
        }

})();