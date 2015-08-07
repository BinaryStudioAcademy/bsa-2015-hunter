(function() {
    'use strict';

    angular
        .module('hunter-app')
        .filter('CandidatesFilter', CandidatesFilter);

    function filterCandidatesByPool(candidate, poolFilters) {
        var result = false;

        for (var i in poolFilters) {
            if (!poolFilters[i].isChecked) {
                continue;
            }

            candidate.Pools.forEach(function(candPool) {
                if (candPool.Name == poolFilters[i].Name) {
                    result = true;
                }
            });

            if (result) {
                break;
            }
        }

        return result;
    }

    function filterCandidatesByStatus(candidate, statusFilters) {
        var result = false;


    }

    function filterCandidatesByInviter(candidate, inviterFilters) {
        var result = false;

        for (var i in inviterFilters) {
            if (inviterFilters[i].isChecked && inviterFilters[i].inviterId == candidate.AddedByProfileId) {
                result = true;
                break;
            }
        }

        return result;
    }

    function CandidatesFilter() {

        return function(candidate, options) {
            if (
                filterCandidatesByPool(candidate, options.poolFilters) ||
                filterCandidatesByInviter(candidate, options.inviterFilters)
            ) {
                return true;
            } else {
                return false;
            }
        };
    }

})();