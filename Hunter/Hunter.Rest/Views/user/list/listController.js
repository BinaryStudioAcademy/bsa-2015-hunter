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
       vm.changeTemplate = changeTemplate;
       vm.currentTabName = {};
       vm.templateToShow = '';



        (function () {
                vm.tabs = [
                   { name: 'Users', route: 'users' },
                   { name: 'Roles', route: 'roles' },
                ];
                vm.currentTabName = vm.tabs[0].name;
                vm.templateToShow = templatePath(vm.tabs[0].route);
            })();


        function changeTemplate(tab) {
                vm.currentTabName = tab.name;
                vm.templateToShow = templatePath(tab.route);
                $location.search('tab', tab.route);
            }
        

        function templatePath(templateName) {
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