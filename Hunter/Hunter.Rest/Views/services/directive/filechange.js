(function() {
    'use strict';

    angular
        .module('hunter-app')
        .directive('filechange', FileChange);

    FileChange.$inject = [];

    function FileChange() {
        return {
            'restrict': 'A',
            'link': function(scope, elem, attr) {
                var handler = scope.$eval(attr.filechange);
                elem.bind('change', handler);
            }
        }
    }
})();