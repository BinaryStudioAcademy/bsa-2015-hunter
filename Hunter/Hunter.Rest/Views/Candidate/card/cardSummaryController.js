﻿(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardSummaryController', cardSummaryController);

    cardSummaryController.$inject = [
        'FeedbackHttpService',
        'VacancyHttpService',
        '$routeParams'
    ];

    function cardSummaryController(feedbackHttpService, vacancyHttpService, $routeParams) {
        var vm = this;
        vm.vacancy;
        vm.summary;

        vm.saveSummary  = function (summary) {
            var body = {
                id: summary.id,
                cardId: summary.cardId,
                text: summary.text,
                type: summary.type
            }
            feedbackHttpService.saveSummary(body, $routeParams.vid, $routeParams.cid).then(function (result) {
                summary.id = result.id;
                summary.date = result.update;
                summary.userName = result.userName;

                console.log("result after update");
                console.log(result);
            });
        }

        vacancyHttpService.getLongList($routeParams.vid).then(function (result) {
            console.log(result);
            vm.vacancy = result;
        });

        feedbackHttpService.getSummary($routeParams.vid, $routeParams.cid).then(function (result) {
            console.log(result);
            vm.summary = result;
            
            if (vm.summary.text == '') {
                vm.summary.summaryConfig = {
                    'buttonName': 'Save',
                    'readOnly': false
                }
            } else {
                vm.summary.summaryConfig = {
                    'buttonName': 'Edit',
                    'readOnly': true
                }
            }
        });

        vm.toggleReadOnly = function() {
            vm.summary.summaryConfig.readOnly = vm.summary.text == '' ? vm.summary.summaryConfig.readOnly
                : !vm.summary.summaryConfig.readOnly;

            if (vm.summary.summaryConfig.readOnly) {
                vm.summary.summaryConfig.buttonName = 'Edit';
                vm.saveSummary(vm.summary);
            } else {
                vm.summary.summaryConfig.buttonName = 'Save';
            }
        }
    }
})();