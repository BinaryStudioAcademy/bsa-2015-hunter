(function() {
    
    'use strict';

    angular
        .module('hunter-app')
        .factory("UploadResumeService", UploadResumeService);

    UploadResumeService.$inject = ['Upload'];

    function UploadResumeService(upload) {
        var service = {
            onFileSelect: onFileSelect,
            uploadResume: uploadResume,
            uploadWithLatenc: uploadWithLatenc
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
                        id: candidate.id,
                        email: candidate.imail,
                        FirstName: candidate.firstName,
                        LastName: candidate.lastName

            },
                    file: _file
                })
                .success(function (data){console.log(data)});
            }
        }

        function uploadWithLatenc(candidate) {
            setTimeout(function() {
                uploadResume(candidate);
            }, 1000);
        }


        return service;
    }
})();