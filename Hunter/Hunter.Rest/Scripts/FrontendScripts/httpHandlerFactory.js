angular.module('hunter-handlers', [])
	.factory('httpHandler', function($http){
		var handler = {
			'send': sendRequest
		};

		function sendRequest(url, verb, successCall, errorCall){
			if(!checkUrl(url)){
				return;
			}

			verb = (verb == '' || verb == undefined) ? 'GET' : verb;

			var config = {
				'method': verb,
				'url': url
			};

			$http(config).then(
				function(response){
					processResult(response, successCall);
				},
				function(reason){
					processResult(reason, errorCall);
				}
			);
		}

		return handler;
	});

function processResult(response, callBack){
	var result = {
		'responseData': response.data,
		'statusCode': response.status
	};

	callBack(result);
}

function checkUrl(url){
	if (url == undefined) {
		console.log('url does not specified');
		processResult({'data': 'url does not specified', 'status': 0});

		return false;
	}

	return true;
}