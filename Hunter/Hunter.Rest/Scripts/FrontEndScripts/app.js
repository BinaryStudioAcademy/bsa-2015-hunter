angular.module('app', ['ngRoute'])
	.config(['$routeProvider', function($routeProvider){
		$routeProvider
			//slide 1
			.when('/candidates', {
				templateUrl: '',
				controller: 'candidatesController'
			})
			//result on Latest activity button click (slide 1 and menu item on slide 3)
			.when('/news', {
				templateUrl: '',
				controller: 'newsController'
			})
			//result on statistics button click (slide 1)
			.when('/statistics', {
				templateUrl: '',
				controller: 'statisticsController'
			})
			//slide 2
			.when('/candidates/viewprofile/:profileId', {
				templateUrl: '',
				controller: 'profileController'
			})
			//slide 3
			.when('/add', {
				templateUrl: '',
				controller: 'createCandidateController'
			})
			//slide 4 5
			.when('/candidates/:profileId', {
				templateUrl: '',
				controller: 'profileController'
			})
			//slide 6
			.when('/candidates/:profileId/specnotes', {
				templateUrl: '',
				controller: 'specnotesController'
			})
			//slide 6 (history bitton click result)
			.when('/candidates/:profileId/specnotes/history', {
				templateUrl: '',
				controller: 'specnotesController'
			})
			//slide 8
			.when('/candidates/:profileId/interview', {
				templateUrl: '',
				controller: 'interviewController'
			})
			//slide 8 (history bitton click result)
			.when('/candidates/:profileId/interview/history', {
				templateUrl: '',
				controller: 'interviewController'
			})
			//slide 7
			.when('/candidates/:profileId/testwork', {
				templateUrl: '',
				controller: 'testworkController'
			})
			//slide 7 (history bitton click result)
			.when('/candidates/:profileId/testwork/history', {
				templateUrl: '',
				controller: 'testworkController'
			})
	}]);