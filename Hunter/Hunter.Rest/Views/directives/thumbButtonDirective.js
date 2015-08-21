(function() {
    'use strict';

    angular
        .module('hunter-app')
        .directive('thumbButton', ThumbButton);

    ThumbButton.$inject = [
//        'EnumConstants'
    ];

    function ThumbButton() {
        return {
            template:
                    '<div id="likeBtn" ng-click="click()" class="circleButton">' +
                        '<span id="likeIcon" class="fa circleButton-icon"></span>' +
                    '</div>',
            scope: {
                btnType: '@btnType',
                btnIcon: '@btnIcon',
                test: '=test',
                borderColor: '@borderColor',
                key: '@key'
            },
            restrict: 'EA',
            link: function (scope, elem, attr) {
                var button = $(elem).find('.circleButton')[0];
                var icon = $(button).find('.circleButton-icon')[0];

                $(icon).addClass(scope.btnIcon);
                $(button).addClass(scope.btnType);

                scope.click = function() {

                    if (scope.test.feedbackConfig.style == undefined || scope.test.feedbackConfig.style["border-color"] != scope.borderColor) {
                        scope.test.feedbackConfig.style = { 'border-color': scope.borderColor };
                        scope.test.feedback.successStatus = scope.getSuccess(scope.key);
                    } else {
                        scope.test.feedbackConfig.style = { 'border-color': 'gray' }
                        scope.test.feedback.successStatus = scope.getSuccess('None');
                    }
                }
            },
            controller: ['$scope', 'EnumConstants', function($scope, EnumConstants) {
                    $scope.getSuccess = function(key) {
                        return EnumConstants.successStatus[key];
                    }
            }]
        }
    }
})()