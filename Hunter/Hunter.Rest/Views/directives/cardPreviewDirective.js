(function () {
    angular
        .module('hunter-app')
        .directive('cardPreview', CardPreviewDirective);

    CardPreviewDirective.$inject = [];

    function CardPreviewDirective() {
        return {
        	restrict: 'E',
            templateUrl: './Views/Candidate/profilePartial/profilePartial.html'/*function(elem, attr){
            		switch(attr.type) {
					    case 'pool':
					        return './Views/Candidate/profilePartial/profilePartial.html';
					    case 'longlist':
					        return './Views/Vacancy/longlist/longlistPreview.html';
					        break;
					}
    			}*/,
    		scope: {
		      	//show: '=',
		      	//candidate: '='
		    },
    		link: function(scope, elem, attr) {
    			    $(document).click(function(event) {
		                if (!firstUse) {
		                    var some = $(event.target),
		                        parentLength = some.parents(".card-container-partial").length,
		                        itemLength = some.parents(".candidate-list").length;
		                    if (!parentLength && !itemLength) {
		                        scope.hideView();
		                        if (!scope.$$phase) {
					                scope.$digest();
					            } else {
					                setTimeout(function () {
					                    scope.$digest();
					                }, 100);
					            }
		                        return;
		                    }
		                }
		                firstUse = false;
		            });
			    
		        /*function closeModal() {
		            scope.show = false;
		            firstUse = true;
		            $('.container-partial-in-pool').hide();
		            if (!$scope.$$phase) {
		                $rootScope.$digest();
		            } else {
		                setTimeout(function () {
		                    $rootScope.$digest();
		                }, 100);
		            }
		        }*/    
		        //scope.showView = scope.show;
		        //cardPreviewDirectiveService.candidate = scope.candidate;
		        firstUse = false;
		        scope.hideView = function(){
		        	scope.show = false;
		        	firstUse = true;
		        }

		        scope.$on('candidateSelected',function(event,item){
		        	scope.show = true;
		        	scope.candidate = item;
		        })

            }
     		
    }
}
})()