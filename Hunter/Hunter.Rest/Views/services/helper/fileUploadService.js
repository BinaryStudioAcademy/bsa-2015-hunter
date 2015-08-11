(function() {
    
    'use strict';

    angular
        .module('hunter-app')
        .factory("FileUploadService", FileUploadService);

    FileUploadService.$inject = ['Upload'];

    function FileUploadService(upload) {
        var service = {
            onFileSelect: onFileSelect,
            uploadResume: uploadResume
        };

        var _file;

        function onFileSelect($file) {
            _file = $file;
        }

        function uploadResume(candidate) {
            if (!!_file) {
                upload.upload({
                    url: "api/fileupload",
                    method: "POST",
                    data: {
                        directory: "resume//",
                        id: candidate.Id,
                        email: candidate.Email
            },
                    file: _file
                })
                .success(function (data){console.log(data)});
            }
            
        }

        return service;
    }
})();