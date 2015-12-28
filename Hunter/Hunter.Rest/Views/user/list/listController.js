(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('ListController', listController);

    listController.$inject = [
        '$scope',
        '$location',
        '$routeParams',
        'AuthService',
        'UserProfileService', //change on your service
        'UserRoleHttpService'
    ];

    function listController($scope, $location, $routeParams, authService, service, userRoleHttpService) {
        var vm = this;
        //Here we should write all vm variables default values. For Example:
       vm.rolesList = [];
       vm.changeTemplate = changeTemplate;
       vm.currentTabName = {};
       vm.templateToShow = '';

        // Here we should write any functions we need, for example, body of user actions methods.
        var updatePage = function (newPageNum) {
            if (typeof newPageNum !== "number" || newPageNum <= 0) {
                newPageNum = 1;
            }
           vm.page = newPageNum;
        }

        vm.loadUsers = function () {
            $location.search('page', vm.page);
            service.getUserProfileList(vm.page, function (response) {
                vm.profilesList = response.data;
            });
        }

        $scope.$watch(function() {return vm.page;}, vm.loadUsers);

        //vm.watch('page', vm.loadUsers);
        
        vm.nextPage = function () {
            updatePage(vm.page + 1);
        }

        vm.prevPage = function () {
            updatePage(vm.page - 1);
        }

        updatePage($routeParams.page);

        userRoleHttpService.getUserRoles().then(function (result) {
                vm.rolesList = result.data;
            });

        (function () {
                vm.tabs = [
                   { name: 'Users', route: 'users' },
                   { name: 'Roles', route: 'roles' },
                ];
                vm.currentTabName = vm.tabs[0].name;
                vm.templateToShow = templateUrl(vm.tabs[0].route);
            })();


        function changeTemplate(tab) {
                vm.currentTabName = tab.name;
                vm.templateToShow = templateUrl(tab.route);
                $location.search('tab', tab.route);
            }
        

        function templateUrl(templateName) {
            //viewsPath = './Views/user/list/';
            switch (templateName.toLowerCase()) {
                case 'users':
                    return './Views/user/list/' + 'usersList.html';
                case 'roles':
                    return './Views/user/list/' + 'rolesList.html';
                default:
                    return '';
            }
        }
        };
})();