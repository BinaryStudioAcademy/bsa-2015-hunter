angular.module('hunter-app', ['ngRoute'])
    .config([
        '$routeProvider', function ($routeProvider) {
            var viewPath = "/Views/";
            $routeProvider
                //result on Latest activity button click (slide 1 and menu item on slide 3)
                .when('/news', {
                    templateUrl: '',
                    controller: 'newsController',
                    controllerAs: 'newsCtrl'
                });

            // sample of all needed routes for entity
            // TODO make similar for other entities (naming, routing)
            $routeProvider
                .when('/vacancy/list', {
                    templateUrl: viewPath + 'Vacancy/list/list.html',
                    controller:  "VacancyListtController",
                    controllerAs: 'vacancyListCtrl',
                    reloadOnSearch: false
                })
                .when('/vacancy/:id', {
                    templateUrl: viewPath + 'Vacancy/profile/profile.html',
                    controller:  "VacancyController",
                    controllerAs: 'vacancyCtrl'
                })
                .when('/vacancy/edit/:id', {
                    templateUrl: viewPath + 'Vacancy/addEdit/addEdit.html',
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
                .when('/candidate/:cid', {
                    templateUrl: viewPath + 'Candidate/profile/profile.html',
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


            //result on statistics button click (slide 1)
            $routeProvider.when('/statistics', {
                templateUrl: viewPath + 'Statistics/list.html',
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
    ]);