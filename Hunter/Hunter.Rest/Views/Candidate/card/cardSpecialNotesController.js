(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardSpecialNotesController', CardSpecialNotesController);

    CardSpecialNotesController.$inject = [
        'SpecialNoteHttpService',
        'VacancyHttpService',
        '$routeParams',
        'localStorageService'
    ];

    function CardSpecialNotesController(specialNoteHttpService, VacancyHttpService, $routeParams, localStorageService) {
        var vm = this;
        vm.templateName = 'Special Notes';

        vm.cardNoteShow = getAllCardNote;
        //vm.candidateNoteShow = getCandidateNote;
        //vm.userNoteShow = getUserNote;
        vm.saveOldNote = saveOldNote;
        vm.saveNewNote = saveNewSpecialNote;

        vm.vacancy;
        vm.notes;
        vm.userName = localStorageService.get('authorizationData').userName;
        console.log(vm.userName);
        vm.newNoteText;

        // TODO: Initialization Should Be Covered with self invoke function
        VacancyHttpService.getLongList($routeParams.vid).then(function (result) {
            console.log(result);
            vm.vacancy = result;
        });

        getAllCardNote();

        function saveNewSpecialNote() {
            var note = {
                text: vm.newNoteText
            };
            specialNoteHttpService.addSpecialNote(note, $routeParams.vid, $routeParams.cid)
            .then(function (data) {
                vm.notes.push(data);
            });
        }

        function saveOldSpecialNote(note) {
            specialNoteHttpService.updateSpecialNote(note, note.id, successEdit)
            .then(function (data) {
                note.id = data.id;
                note.lastEdited = data.update;
                note.userLogin = data.userName;
            });
        }

        function successAdd(response) {
            //debugger;
            //$location.url('/candidate/list');'
            vm.newNoteText = "";
            getAllCardNote();
        }

        function successEdit(response) {
            //getAllCardNote();
        }

        // TODO: Data Functions (not user event functions) Should Be In Services
        function getAllCardNote() {
            specialNoteHttpService.getCardSpecialNote($routeParams.vid, $routeParams.cid).then(function (result) {
                console.log(result.data);
                vm.notes = result.data;
            });
        }

        /*function getCandidateNote() {
            specialNoteHttpService.getCandidateSpecialNote($routeParams.cid).then(function (result) {
                console.log(result.data);
                vm.notes = result.data;
            });
        }

        function getUserNote() {
            specialNoteHttpService.getUserSpecialNote(vm.userName, $routeParams.vid, $routeParams.cid).then(function (result) {
                console.log(result.data);
                vm.notes = result.data;
            });
        }*/

        function saveOldNote(note) {
            saveOldSpecialNote(note);
        };
    }
})();