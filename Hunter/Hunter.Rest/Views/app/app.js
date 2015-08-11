
angular.module('hunter-app', ['ngRoute', 'LocalStorageModule', 'angularUtils.directives.dirPagination', 'ngFileUpload'])
    .config([
        '$routeProvider', function ($routeProvider) {

            var viewPath = "/Views/";
            $routeProvider
                //result on Latest activity button click (slide 1 and menu item on slide 3)
                .when('/login', {
                    templateUrl: viewPath + 'login/login.html',
                    controller: 'LoginController',
                    controllerAs: 'loginCtrl'
                })
                .when('/register', {
                    templateUrl: viewPath + 'register/register.html',
                    controller: 'RegisterController',
                    controllerAs: 'registerCtrl'
                })
                .when('/news', {
                    templateUrl: '',
                    controller: 'newsController',
                    controllerAs: 'newsCtrl'
                });

            // sample of all needed routes for entity
            // TODO make similar for other entities (naming, routing)
            $routeProvider
                .when('/vacancy/list', {
                    templateUrl: viewPath + 'vacancy/list/list.html',
                    controller: "VacancyListController",
                    controllerAs: "VacancyListCtrl",
                    reloadOnSearch: false
                })
                .when('/vacancy/edit', {
                    templateUrl: viewPath + 'vacancy/addEdit/addEdit.html',
                    controller: "VacancyAddEditController",
                    controllerAs: 'vacancyAddEditCtrl'
                })
                .when('/vacancy/:id', {
                    templateUrl: viewPath + 'vacancy/profile/profile.html',
                    controller: "VacancyController",
                    controllerAs: 'vacancyCtrl'
                })
                .when('/vacancy/edit/:id', {
                    templateUrl: viewPath + 'vacancy/addEdit/addEdit.html',
                    controller: "VacancyAddEditController",
                    controllerAs: 'vacancyAddEditCtrl'
                });

            $routeProvider
                .when('/candidate/list', {
                    templateUrl: viewPath + 'candidate/list/list.html',
                    controller: 'CandidateListController',
                    controllerAs: 'candidateListCtrl',
                    reloadOnSearch: false
                })
                .when('/candidate/edit', {
                    templateUrl: viewPath + 'candidate/addEdit/addEdit.html',
                    controller: 'CandidateAddEditController',
                    controllerAs: 'candidateAddEditCtrl'
                })
                .when('/candidate/:cid', {
                    templateUrl: viewPath + 'candidate/profile/profile.html',
                    controller: 'CandidateController',
                    controllerAs: 'candidateCtrl'
                })
                .when('/vacancy/:vid/candidate/:cid', {
                    templateUrl: viewPath + 'Candidate/profile/profile.html',
                    controller: 'CandidateController',
                    controllerAs: 'candidateCtrl'
                })
                .when('/candidate/edit/:id', {
                    templateUrl: viewPath + 'candidate/addEdit/addEdit.html',
                    controller: 'CandidateAddEditController',
                    controllerAs: 'candidateAddEditCtrl'
                });

            $routeProvider
                .when('/pool', {
                    templateUrl: viewPath + 'pool/list/list.html',
                    controller: 'PoolListController',
                    controllerAs: 'poolCtrl'
                });

            $routeProvider
                .when("/pool/edit/:id", {
                    templateUrl: viewPath + "pool/addEdit/addEdit.html",
                    controller: "PoolAddEditController",
                    controllerAs: "poolAddEditCtrl"
                });
            $routeProvider
                .when('/activity', {
                    templateUrl: viewPath + 'activity/list/list.html',
                    controller: "ActivityListController",
                    controllerAs: "ActivityListCtrl"
                });
            $routeProvider
                .when('/files', {
                    templateUrl: viewPath + 'files/list/list.html',
                    controller: 'FileListController',
                    controllerAs: 'fileListCtrl'
                });

            $routeProvider
                .when('/role', {
                    templateUrl: viewPath + 'role/list/list.html',
                    controller: 'RoleListController',
                    controllerAs: 'roleListCtrl'
                })
                .when('/role/edit', {
                    templateUrl: viewPath + 'role/addEdit/addEdit.html',
                    controller: 'UserAddEditController',
                    controllerAs: 'userAddEditCtrl'
                })
                .when('/role/edit/:id', {
                    templateUrl: viewPath + 'role/addEdit/addEdit.html',
                    controller: 'UserAddEditController',
                    controllerAs: 'userAddEditCtrl'
                })
                .when('/role/:id', {
                    templateUrl: viewPath + 'role/profile/profile.html',
                    controller: 'RoleController',
                    controllerAs: 'roleCtrl'
                });

            $routeProvider
                .when('/user', {
                    templateUrl: viewPath + 'user/list/list.html',
                    controller: 'UserListController',
                    controllerAs: 'userListCtrl'
                })
                .when('/user/edit', {
                    templateUrl: viewPath + 'user/addEdit/addEdit.html',
                    controller: 'UserAddEditController',
                    controllerAs: 'userAddEditCtrl'
                })
                .when('/user/edit/:id', {
                    templateUrl: viewPath + 'user/addEdit/addEdit.html',
                    controller: 'UserAddEditController',
                    controllerAs: 'userAddEditCtrl'
                })
                .when('/user/:id', {
                    templateUrl: viewPath + 'user/profile/profile.html',
                    controller: 'UserController',
                    controllerAs: 'userCtrl'
                });

            //result on statistics button click (slide 1)
            $routeProvider
                .when('/statistics', {
                    templateUrl: viewPath + 'statistics/list.html',
                    controller: 'StatisticsController',
                    controllerAs: 'statisticsCtrl'
                })
                //slide 2
                .when('/candidates/viewprofile/:profileId', {
                        templateUrl: '',
                        controller: 'profileController',
                        controllerAs: 'profileCtrl'
                    })
                //slide 3
                .when('/add', {
                        templateUrl: '',
                        controller: 'createCandidateController',
                        controllerAs: 'createCandidateCtrl'
                    })
                //slide 4 5
                .when('/candidates/:profileId', {
                        templateUrl: '',
                        controller: 'profileController',
                        controllerAs: 'profileCtrl'
                    })
                //slide 6
                .when('/candidates/:profileId/specnotes', {
                        templateUrl: '',
                        controller: 'specnotesController',
                        controllerAs: 'specnotesCtrl',
                    })
                //slide 6 (history bitton click result)
                .when('/candidates/:profileId/specnotes/history', {
                        templateUrl: '',
                        controller: 'specnotesController',
                        controllerAs: 'specnotesCtrl'
                    })
                //slide 8
                .when('/candidates/:profileId/interview', {
                        templateUrl: '',
                        controller: 'interviewController',
                        controllerAs: 'interviewCtrl'
                    })
                //slide 8 (history bitton click result)
                .when('/candidates/:profileId/interview/history', {
                        templateUrl: '',
                        controller: 'interviewController',
                        controllerAs: 'interviewCtrl'
                    })
                //slide 7
                .when('/candidates/:profileId/testwork', {
                        templateUrl: '',
                        controller: 'testworkController',
                        controllerAs: 'testworkCtrl'
                    })
                //slide 7 (history bitton click result)
                .when('/candidates/:profileId/testwork/history', {
                        templateUrl: '',
                        controller: 'testworkController',
                        controllerAs: 'testworkCtrl'
                    });
        }
    ])
    .config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push('AuthInterceptorService');
    }])
    .run(['AuthService', function (AuthService) {
        AuthService.fillAuthData();
    }]);
