(function() {
    'use strict';

    angular
        .module('hunter-app')
        .directive('fileRead', FileRead);

    FileReader.$inject = [
    ];

    function FileRead() {
        return {
            restrict: 'A',
            scope: true,
            link:function(scope, element, attributes) {
                element.bind('change', function(changeEvent) {
                    scope.$apply(function() {
                        scope.fileread = changeEvent.target.files[0];
                    });
                });
            }
        }
    }
})();