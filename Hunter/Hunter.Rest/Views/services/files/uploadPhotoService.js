(function () {
    angular.module('hunter-app').
        factory('UploadPhotoService', UploadPhotoService);

    UploadPhotoService.$inject = ['Upload', 'HttpHandler', '$q'];

    function UploadPhotoService(Upload, httpHandler, $q) {

        var service = {
            uploadPicture: uploadPicture,
            validatePicture: validatePicture,
            getPicture: getPicture
        }

        function uploadPicture(file, id) {
            if (validatePicture(file)) {
                console.log(file);

                Upload.upload({
                    url: 'api/fileupload/pictures/' + id,
                    file: file,
                    method: 'POST'
                });
            }
        }

        function getPicture(id) {
            var deferred = $q.defer();
            httpHandler.sendRequest({
                verb: 'GET',
                url: '/api/fileupload/pictures/' + id,
                successCallback: function (response) {
                    deferred.resolve(response);
                },
                errorCallback: function (status) {
                    console.log("getting photo error");
                    console.log(status);
                }
            });
            return deferred.promise;
        }

        function validatePicture(file) {
            if (file != null) {
                if (file.size > 500000) {
                    alert("File size too big");
                    return false;
                }
                if (file.type.split("/")[0] != "image") {
                    alert("File is not an image");
                    return false;
                }
                return true;
            }
            return false;
        }

        return service;
    }
})();