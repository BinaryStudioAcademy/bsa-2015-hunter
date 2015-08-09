(function () {
    'use strict';

    Array.prototype.unique = function () {
        var arr = this.concat();

        if (arr.length == 0) {
            return [];
        }

        for (var i = 0; i < arr.length - 1; i++) {
            for (var j = i + 1; j < arr.length; j++) {
                if (arr[i] === arr[j]) {
                    arr.splice(j--, 1);
                }
            }
        }

        return arr;
    }

    angular
        .module('hunter-app')
        .filter('VacanciesFilter', VacanciesFilter);

    function filterVacanciesByPool(vacancies, poolFilters) {
        var filtered = [];

        if (vacancies == undefined) {
            return filtered;
        }

        vacancies.forEach(function (vacancy) {
            var id = vacancy.poolId;

                if (poolFilters[id]) {
                    filtered.push(vacancy);
                }
        });

        return filtered;
    }

    function filterVacanciesByStatus(vacancies, statusFilters) {
        var filtered = [];

        if (vacancies == undefined) {
            return filtered;
        }

        vacancies.forEach(function (vacancy) {
            var resolution = vacancy.resolutionString;

            if (statusFilters[resolution]) {
                filtered.push(vacancy);
            }
        });

        return filtered;
    }

    function filterVacanciesByAdder(vacancies, inviterFilters) {
        var filtered = [];

        if (vacancies == undefined) {
            return filtered;
        }

        vacancies.forEach(function (vacancy) {
            inviterFilters.forEach(function (inviter) {
                if (inviter.id == vacancy.addedByProfileId && inviter.isChecked) {
                    filtered.push(vacancy);
                }
            });
        });

        return filtered;
    }

    function VacanciesFilter() {

        return function (vacancies, options) {

            if (!checkOptions(options.poolFilters) &&
                !checkOptions(options.statusFilters) &&
                !checkOptions(options.inviterFilters)) {
                return vacancies;
            }

            var res = filterVacanciesByPool(vacancies, options.poolFilters);
            //res = res.concat(filterCandidatesByInviter(candidates, options.inviterFilters));
            //res = res.concat(filterCandidatesByStatus(candidates, options.statusFilters));

            return res;
        }
    }

    function checkOptions(options) {
        var anyChecked = false;

        if (options instanceof Array) {
            for (var i in options) {
                if (options[i].isChecked) {
                    anyChecked = true;
                }
            }
        } else {
            for (var key in options) {
                if (options[key]) {
                    anyChecked = true;
                }
            }
        }

        return anyChecked;
    }

})();