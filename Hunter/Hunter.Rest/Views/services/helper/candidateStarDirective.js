(function () {
    'use strict';
    angular
        .module('hunter-app')
        .directive('shorListStar',shorListStar);

    shorListStar.$inject = [];

    function shorListStar() {
            return {
                template: '<i class="fa " ' +
                    'ng-class="fill ? \'fa-star-o\' : \'fa-star\' "' +
                    'ng-click="fill = !fill" ' +
                    'style="position: absolute;font-size: large;top: 2px;left: 2px;color: gold;"' +
                    '></i>',
                restrict: 'A',
                transclude: true,
                link: function (scope, element, attrs, ctrl, transclude) {
                    scope.fill = false;

                    transclude(function (clone) {
                        element.append(clone);
                    });

                }
            }
        }

})();