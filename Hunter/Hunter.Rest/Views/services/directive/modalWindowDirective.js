(function () {
    'use strict';

    angular
        .module('hunter-app')
        .directive('modalwindow', ModalWindow);

    ModalWindow.$ingect = [

    ];

    function ModalWindow() {
        return {
            link: link
        };
    };

       
    function link(scope, element, attrs) {
        element.on('click', clickBtn);

        function clickBtn(event) {
            console.log(scope.testCtrl.techExpert);
            var container = document.createElement('div');
            container.innerHTML = '<div id="myModal" class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">\
                                      <div class="modal-dialog modal-lg">\
                                        <div class="modal-content">\
                                          <h1>{{testCtrl.techExpert[0].name}}</h1>  \
                                        </div>\
                                      </div>\
                                    </div>';
            document.getElementsByTagName('body')[0].appendChild(container);
            $('#myModal').modal();

        }
    };

    


})();