(function() {
    'use strict';

    angular
        .module('hunter-app')
        .directive('thumbButton', ThumbButton);

    ThumbButton.$inject = [];

    function ThumbButton() {
        return {
            template:
                    '<div id="likeBtn" ng-click="click()" class="circleButton">' +
                        '<span id="likeIcon" class="fa circleButton-icon"></span>' +
                    '</div>',
            scope: {
                btnType: '@btnType',
                btnIcon: '@btnIcon',
                feedConfig: '=feedConfig',
                borderColor: '@borderColor'
            },
            restrict: 'EA',
            link: function (scope, elem, attr) {
                var button = $(elem).find('.circleButton')[0];
                var icon = $(button).find('.circleButton-icon')[0];

                $(icon).addClass(scope.btnIcon);
                $(button).addClass(scope.btnType);

                scope.click = function() {

                    if (scope.feedConfig.style == undefined || scope.feedConfig.style["border-color"] != scope.borderColor) {
                        scope.feedConfig.style = { 'border-color': scope.borderColor }
                    } else {
                        scope.feedConfig.style = {'border-color': 'gray'}
                    }
                }
            }
        }
    }
})()