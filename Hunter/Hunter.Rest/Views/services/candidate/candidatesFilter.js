﻿(function() {
    'use strict';

    Array.prototype.unique = function() {
        var arr = this.concat();

        if (arr.length == 0) {
            return [];
        }

        for (var i = 0; i < arr.length - 1; i++) {
            for (var j = i+1; j < arr.length; j++) {
                if (arr[i] === arr[j]) {
                    arr.splice(j--, 1);
                }
            }
        }

        return arr;
    }

    angular	
        .module('hunter-app')
        .filter('CandidatesFilter', CandidatesFilter);

    function filterCandidatesByPool(candidates, poolFilters) {
        var filtered = [];

        if (candidates == undefined) {
            return filtered;
        }

        candidates.forEach(function(candidate) {
            var wasPushed = false;

            candidate.pools.forEach(function(pool) {
                var name = pool.name;

                if (poolFilters[name] && !wasPushed) {
                    filtered.push(candidate);
                    wasPushed = true;
                }
            });
        });

        return filtered;
    }

    function filterCandidatesByStatus(candidates, statusFilters) {
        var filtered = [];

        if (candidates == undefined) {
            return filtered;
        }

        candidates.forEach(function(candidate) {
            var resolution = candidate.resolutionString;

            if (statusFilters[resolution]) {
                filtered.push(candidate);
                wasPushed = true;
            }
        });

        return filtered;
    }

    function filterCandidatesByInviter(candidates, inviterFilters) {
        var filtered = [];

        if (candidates == undefined) {
            return filtered;
        }

        candidates.forEach(function(candidate) {
            inviterFilters.forEach(function(inviter) {
                if (inviter.id == candidate.addedByProfileId && inviter.isChecked) {
                    filtered.push(candidate);
                }
            });
        });

        return filtered;
    }

    function CandidatesFilter() {

        return function (candidates, options) {

            if (!checkOptions(options.poolFilters) &&
                !checkOptions(options.statusFilters) &&
                !checkOptions(options.inviterFilters))
            {
                return candidates;
            }

            var res = filterCandidatesByPool(candidates, options.poolFilters);
            res = res.concat(filterCandidatesByInviter(candidates, options.inviterFilters));
            res = res.concat(filterCandidatesByStatus(candidates, options.statusFilters));

            return res.unique();
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