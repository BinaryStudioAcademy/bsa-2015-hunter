(function () {

    'use strict';

    angular
        .module('hunter-app')
        .factory("UploadTestService", UploadTestService);

    UploadResumeService.$inject = [
        'Upload',
        'EnumConstants'
    ];

    function UploadTestService(upload, enumConstants) {
        var service = {
            onFileSelect: onFileSelect,
            uploadResume: uploadResume,
        };

        var _file;

        function onFileSelect($file) {
            _file = $file;
        }

        function uploadTest(candidateId, vacancyId) {
            if (!!_file) {
                console.log(_file.name);
                upload.upload({
                    url: "api/fileupload",
                    method: "POST",
                    data: {
                        fileType: enumConstants.fileType.Test,
                        fileName: _file.name,
                        candidateid: candidateId,
                        vacancyid: vacancyId
                    },
                    file: _file
                })
                .success(function (data) { console.log(data) });
            }
        }

        return service;
    }
})();