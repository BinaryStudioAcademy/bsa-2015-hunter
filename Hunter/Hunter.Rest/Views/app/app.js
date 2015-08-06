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
                .when('/vacancy', {
                    templateUrl: viewPath + 'Vacancy/list.html',
                    controller: 'vacancyListCtrl',
                    reloadOnSearch: false
                })
                .when('/vacancy/:id', {
                    templateUrl: viewPath + 'Vacancy/longlist.html',
                    controller: 'vacancyListCtrl'
                })
                .when('/vacancy/add', {
                    templateUrl: viewPath + 'Vacancy/edit.html',
                    controller: 'vacancyEditCtrl'
                })
                .when('/vacancy/edit/:id', {
                    templateUrl: viewPath + 'Vacancy/edit.html',
                    controller: 'vacancyEditCtrl'
                });

            $routeProvider
                .when('/candidate', {
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
                    controller: 'candidateCtrl'
                })
                .when('/candidate/:id', {
                    templateUrl: viewPath + 'Candidate/edit.html',
                    controller: 'CandidateAddEditController',
                    controllerAs: 'candidateAddEditCtrl'
                })
                .when('/candidate/edit/:id', {
                    templateUrl: viewPath + 'Candidate/edit.html',
                    controller: 'vacancyEditCtrl'
                });


            //result on statistics button click (slide 1)
            $routeProvider.when('/statistics', {
                templateUrl: '',
                controller: 'statisticsController',
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