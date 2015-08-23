(function () {
    'use strict';

    angular
        .module('hunter-app')
        .controller('CardSpecialNotesController', CardSpecialNotesController);

    CardSpecialNotesController.$inject = [
        'SpecialNoteHttpService',
        'VacancyHttpService',
        '$routeParams'
    ];

    function CardSpecialNotesController(specialNoteHttpService, VacancyHttpService, $routeParams, localStorageService) {
        var vm = this;
        vm.templateName = 'Special Notes';

        vm.cardNoteShow = getAllCardNote;
        vm.candidateNoteShow = getCandidateNote;
        vm.userNoteShow = getUserNote;

        vm.toggleReadOnly = toggleReadOnly;

        vm.vacancy;
        vm.specialNote;
        vm.newNoteText;

        // TODO: Initialization Should Be Covered with self invoke function
        VacancyHttpService.getLongList($routeParams.vid).then(function (result) {
            console.log(result);
            vm.vacancy = result;
        });

        getAllCardNote();


        vm.saveNewNote = SaveNewSpecialNote;
        vm.saveOldNote = SaveOldSpecialNote;

        function SaveNewSpecialNote(newText) {
            var note = {
                text : newText
            }
            specialNoteHttpService.addSpecialNote(note, $routeParams.vid, $routeParams.cid)
            .then(function(data) {
                note.id = data.id;
                note.lastEdited = data.update;
                note.userAlias = data.userAlias;
                note.cardId = data.cardId;
                note.noteConfig = {
                    'buttonName': 'Edit',
                    'readOnly': true
                };

                vm.specialNote.push(note);
            });
        }

        function SaveOldSpecialNote(note) {
            specialNoteHttpService.updateSpecialNote(note, note.id, successEdit)
            .then(function(data) {
                note.id = data.id;
                note.lastEdited = data.update;
                note.userAlias = data.userAlias;
            });
        }

        function successAdd(response) {
            //debugger;
            //$location.url('/candidate/list');'
            vm.newNoteText = "";
            getAllCardNote();
        }
        function successEdit(response) {
            getAllCardNote();
        }

        // TODO: Data Functions (not user event functions) Should Be In Services
        function getAllCardNote() {
            specialNoteHttpService.getCardSpecialNote($routeParams.vid, $routeParams.cid).then(function (result) {
                console.log(result.data);
                vm.specialNote = result.data;

                vm.specialNote.forEach(function(note) {
                    note.noteConfig = {
                        'buttonName': 'Edit',
                        'readOnly': true
                    }
                });
            });
        }

        function getCandidateNote() {
            specialNoteHttpService.getCandidateSpecialNote($routeParams.cid).then(function (result) {
                console.log(result.data);
                vm.specialNote = result.data;
            });
        }

        function getUserNote() {
            specialNoteHttpService.getUserSpecialNote(vm.userAlias, $routeParams.vid, $routeParams.cid).then(function (result) {
                console.log(result.data);
                vm.specialNote = result.data;
            });
        }

        function toggleReadOnly(note) {
            note.noteConfig.readOnly = note.text != '' ? !note.noteConfig.readOnly : note.noteConfig.readOnly;

            if (note.noteConfig.readOnly) {
                note.noteConfig.buttonName = 'Edit';
                SaveOldSpecialNote({
                    'id': note.id,
                    'userAlias': note.login,
                    'text': note.text,
                    'lastEdited': note.lastEdited,
                    'cardId': note.cardId
                });

            } else {
                note.noteConfig.buttonName = 'Save';
            }
        }
    }
})();