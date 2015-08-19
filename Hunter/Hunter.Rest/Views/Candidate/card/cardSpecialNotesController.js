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
        vm.candidateNoteShow = getCandidateNote;
        vm.userNoteShow = getUserNote;

        vm.toggleReadOnly = toggleReadOnly;

        vm.vacancy;
        vm.specialNote;
        vm.userName = localStorageService.get('authorizationData').userName;
        console.log(vm.userName);
        vm.newNoteText;

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
            specialNoteHttpService.addSpecialNote(note,$routeParams.vid, $routeParams.cid, successAdd);
        }

        function SaveOldSpecialNote(note) {
            specialNoteHttpService.updateSpecialNote(note, note.id, successEdit);
        }

        function successAdd() {
            //debugger;
            //$location.url('/candidate/list');'
            vm.newNoteText = "";
            getAllCardNote();
        }
        function successEdit() {
            getAllCardNote();
        }
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
            specialNoteHttpService.getUserSpecialNote(vm.userName, $routeParams.vid, $routeParams.cid).then(function (result) {
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
                    'userLogin': note.login,
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