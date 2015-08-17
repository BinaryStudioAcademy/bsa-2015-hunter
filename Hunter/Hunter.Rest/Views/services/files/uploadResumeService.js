(function() {
    
    'use strict';

    angular
        .module('hunter-app')
        .factory("UploadResumeService", UploadResumeService);

    UploadResumeService.$inject = ['Upload', 'EnumConstants'];

    function UploadResumeService(upload, enumConstants) {
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
                console.log(_file.name);
                upload.upload({
                    url: "api/fileupload",
                    method: "POST",
                    data: {
                        fileType: enumConstants.fileType.Resume,
                        fileName: candidate.firstName + '_' + candidate.lastName + '.' + _file.name.split('.')[1],
                        candidateid: candidate.id
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