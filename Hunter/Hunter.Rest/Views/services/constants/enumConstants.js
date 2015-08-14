(function () {
    'use strict';

    angular
        .module('hunter-app')
        .constant('EnumConstants', {
            origins: [
                { id: 0, name: 'Sourced'},
                { id: 1, name: 'Applied' }
            ],
            resolutions: [
                { id: 0, name: 'None' },
                { id: 1, name: 'Hired' },
                { id: 2, name: 'Available' },
                { id: 3, name: 'Not Now' },
                { id: 4, name: 'Not Interested' },
                { id: 5, name: 'Unfit' },
            ],
            vacancyStates: [
                { id: 0, name: 'Open' },
                { id: 1, name: 'Closed' },
                { id: 2, name: 'Burning' }
            ],
            substatuses: [
                { id: 0, name: 'Test Failed' },
                { id: 1, name: 'Interview Failed' },
                { id: 2, name: 'Passed' },
            ],
            cardStage: [
                { id: 0, name: 'Test Send' },
                { id: 1, name: 'Test Done' },
                { id: 2, name: 'Interview' },
                { id: 3, name: 'Failed' },
                { id: 4, name: 'Test Failed' },
                { id: 5, name: 'Interview Failed' },
                { id: 6, name: 'Passed' },
            ]
        });
})();