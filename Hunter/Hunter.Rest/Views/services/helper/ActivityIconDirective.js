(function () {
    'use strict';
    angular
        .module('hunter-app')
        .directive('activityType', function () {
            return function (scope, element, attrs) {
                    var icon = '';
                    switch (attrs.activityType) {
                        case '0':
                            icon = 'fa-users';
                            break;
                        case '1':
                            icon = 'fa-suitcase';
                            break;
                        case '2':
                            icon = 'fa-comment';
                            break;

                        default:
                            icon = 'fa-question';
                    }

                    element.html("<i class=\"fa " + icon + " \"></i>");
            }
        });

})();