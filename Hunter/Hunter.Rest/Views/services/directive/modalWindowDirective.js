(function () {
    'use strict';

    angular
        .module('hunter-app')
        .directive('modalWindow', ModalWindow);

    ModalWindow.$ingect = [

    ];

    function ModalWindow() {
        return {
            template: '<div class="modal fade">' +
                          '<div class="modal-dialog">' +
                            '<div class="modal-content">' +
                              '<div class="modal-header">' +
                                '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
                                '<h4 class="modal-title">Название модали</h4>' +
                              '</div>' +
                              '<div class="modal-body">' +
                                '<p>One fine body&hellip;</p>' +
                              '</div>' +
                              '<div class="modal-footer">' +
                                '<button type="button" class="btn btn-default" data-dismiss="modal">Закрыть</button>' +
                                '<button type="button" class="btn btn-primary">Сохранить изменения</button>' +
                              '</div>' +
                            '</div><!-- /.modal-content -->' +
                          '</div><!-- /.modal-dialog -->' +
                        '</div><!-- /.modal -->',
            link: link
        };
    };

       
    function link(scope, element, attrs) {
        console.log("scope:");
        console.log(scope);
        console.log("element:");
        console.log(element);
        console.log("attrs:");
        console.log(attrs);
    };


})();