(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('ActivityListController', ActivityListController);

    ActivityListController.$inject = [
        '$location',
        '$scope',
        'AuthService',
        'ActivityHttpService',
        'localStorageService',
        '$odataresource',
        '$odata',
        'ActivityService',
        'EnumConstants'
    ];

    function ActivityListController($location, $scope, authService, activityHttpService, localStorageService, $odataresource, $odata, activityService, EnumConstants) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
        vm.name = "Activity";
        vm.itemsOnPage = EnumConstants.itemsOnPage;

        // TODO: Use camelCase
        $scope.IndexCtrl.amount = 0;

        

        //(function() {
        //    // This is function for initialization actions, for example checking auth
        //    if (authService.isLoggedIn()) {
        //    // Can Make Here Any Actions For Data Initialization, for example, http queries, etc.
        //    } else {
        //        $location.url('/login');
        //    }
        //})();

        vm.getActivitiesOdata = getActivitiesOdata;

        vm.activitiesList = [];

        vm.filterSpinner = false;
        // get filter options 
        (function () {
            vm.filterSpinner = true;
            activityHttpService.getFilterOptions().then(function(data) {
                vm.filterUsers = activityService.getFilterUsers(data);
                vm.filterTags = activityService.getFilterTags(data);
                vm.filterSpinner = false;
            });
        })();

        vm.filter = {
            users: [],
            tags: [],
            currentPage: 1
        };
        vm.pageSize = vm.itemsOnPage.defaultItem;
        vm.skip = 0;
        var predicate;
        var activitiesOdata = $odataresource('/api/activities/odata');

        vm.listSpinner = false; 
        function getActivitiesOdata() {
            vm.listSpinner = true;
            var acts = activitiesOdata.odata()
                                        .withInlineCount()
                                        .take(vm.pageSize)
                                        .skip(vm.skip)
                                        .filter(predicate)
                                        .orderBy('Time', 'desc')
                                        .query(function () {
                                            vm.activitiesList = acts.items;
                                            vm.totalItems = acts.count;
                                            vm.listSpinner = false; 
                                        });
        };

        // filters
        $scope.$watch('ActivityListCtrl.filter', function () {
            var filt = [];

            if (vm.filter.users.length > 0) {
                var uPred = [];
                angular.forEach(vm.filter.users, function (value, key) {
                    uPred.push(new $odata.Predicate('UserAlias', value));
                });

                uPred = $odata.Predicate.or(uPred);
                filt.push(uPred);
            }

            if (vm.filter.tags.length > 0) {
                var tPred = [];
                angular.forEach(vm.filter.tags, function (value, key) {
                    tPred.push(new $odata.Predicate('Tag', value));
                });

                tPred = $odata.Predicate.or(tPred);
                filt.push(tPred);
            }

            if (filt.length > 0) {
                predicate = $odata.Predicate.and(filt);
            } else {
                predicate = undefined;
            }

            vm.skip = (vm.filter.currentPage - 1) * vm.pageSize;

            vm.getActivitiesOdata();
        }, true);

        vm.getActivitiesOdata();




        vm.activityList;
        // Here we should write any functions we need, for example, body of user actions methods.
        // TODO: Initialization Should Be covered with self invoke function
        activityHttpService.getActivityList(function (data) {
            vm.activityList = data.data;
            //console.log(data.data);

            if(isNewActivityPresent(vm.activityList)){
                saveLastViewdActivityId(vm.activityList);
            }

        });

        function saveLastViewdActivityId(activityList) {
            if (activityList == null || activityList.length == 0) {
                return;
            }

            activityList.sort(sortFunc);

            var lastViewdId = activityList[0].id;

            localStorageService.set('lastViewdActivityId', lastViewdId);

            activityHttpService.saveLastActivityId(function(response) {
                //console.log(response.data);
            }, 
            lastViewdId);
        }

        // TODO: Data Functions (not user event functions) Should Be In Services
        function isNewActivityPresent(activityList) {
            if (activityList == null || activityList.length == 0) {
                return;
            }

            activityList.sort(sortFunc);

            var lastId = localStorageService.get('lastViewdActivityId');

            if (lastId != activityList[0].id) {
                return true;
            } else {
                return false;
            }
        }
    }

    // TODO: Data Functions (not user event functions) Should Be In Services
    function sortFunc(a, b) {
        var aDate = new Date(a.time);
        var bDate = new Date(b.time);

        if (aDate > bDate) {
            return -1;
        } else if (aDate < bDate) {
            return 1;
        } else {
            return 0;
        }
    }

})();