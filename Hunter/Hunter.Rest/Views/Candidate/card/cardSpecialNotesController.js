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

        vm.saveOldNote = saveOldNote;
        vm.saveNewNote = saveNewSpecialNote;
        vm.loadCardNotes = loadCardNotes;
        vm.loadAllNotes = loadAllNotes;
        vm.loadMyNotes = loadMyNotes;

        vm.vacancy;
        vm.notes;
        vm.newNoteText = '';
        vm.specialNote;
        vm.newNoteText;

        // TODO: Initialization Should Be Covered with self invoke function
        VacancyHttpService.getLongList($routeParams.vid).then(function(result) {
            console.log(result);
            vm.vacancy = result;
        });

        function saveNewSpecialNote() {
            var note = {
                text: vm.newNoteText
            };
            specialNoteHttpService.addSpecialNote(note, $routeParams.vid, $routeParams.cid)
                .then(function(data) {
                    note.id = data.id;
                    note.lastEdited = data.update;
                    note.userAlias = data.userAlias;
                    note.cardId = data.cardId;
                    note.lastEdited = data.update;
                    vm.notes.unshift(note);
                    vm.newNoteText = '';
                });
        }

        function saveOldNote(note) {
            specialNoteHttpService.updateSpecialNote(note, note.id)
                .then(function(data) {
                    note.id = data.id;
                    note.lastEdited = data.update;
                    note.userLogin = data.userName;
                });
        }

        // TODO: Data Functions (not user event functions) Should Be In Services
        function loadCardNotes() {
            specialNoteHttpService.getCardSpecialNote($routeParams.vid, $routeParams.cid)
                .then(function(result) {
                    console.log(result.data);
                    vm.notes = result.data;
                });
        };

        function loadAllNotes() {
            specialNoteHttpService.getCandidateSpecialNote($routeParams.cid).then(function(result) {
                console.log(result.data);
                vm.notes = result.data;
            });
        }

        function loadMyNotes() {
            specialNoteHttpService.getUserSpecialNote(vm.userName, $routeParams.vid, $routeParams.cid).then(function(result) {
                console.log(result.data);
                vm.notes = result.data;
            });
        }

        loadCardNotes();

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