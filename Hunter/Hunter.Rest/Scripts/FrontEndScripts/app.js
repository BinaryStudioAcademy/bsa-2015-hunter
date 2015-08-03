angular.module('hunter-app', ['ngRoute'])
	.config(['$routeProvider', function($routeProvider){
		$routeProvider
			//slide 1
			.when('/candidates', {
				templateUrl: '',
				controller: 'candidatesController',
				controllerAs: 'candidatesCtrl'
			})
			//result on Latest activity button click (slide 1 and menu item on slide 3)
			.when('/news', {
				templateUrl: '',
				controller: 'newsController',
				controllerAs: 'newsCtrl'
			})
			//result on statistics button click (slide 1)
			.when('/statistics', {
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
			})
	}]);