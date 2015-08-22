(function () {
    'use strict';

    angular
        .module('hunter-app')
        .constant('EnumConstants', {
            origins: [
                { id: 0, name: 'Sourced', color: 'White', colorCode: 'rgb(255, 255, 255)' },
                { id: 1, name: 'Applied', color: 'Green', colorCode: 'rgb(44, 201, 99)' }
            ],
            resolutions: [
                { id: 0, name: 'None', color: '', colorCode: '' },
                { id: 1, name: 'Hired', color: 'Blue', colorCode: 'rgb(87, 166, 235)' },
                { id: 2, name: 'Available' },
                { id: 3, name: 'Not Now', color: 'Violet', colorCode: 'rgb(188, 140, 250)' },
                { id: 4, name: 'Not Interested', color: 'Red', colorCode: 'rgb(240, 88, 88)' },
                { id: 5, name: 'Unfit', color: 'Grey', colorCode: 'rgb(238, 238, 238)' }
            ],
            vacancyStates: [
                { id: 0, name: 'Draft' },    // - is used when the vacancy is not yet opened.
                { id: 1, name: 'Open' },     // - we can add new candidates, publish\post active landings in future
                { id: 2, name: 'On Hold' },  // - vacancy is not relevant AT THE MOMENT. But will be IN SOME TIME. When vacancy is on hold we still can add candidates, feedbacks etc.
                { id: 3, name: 'Filled' },   // - candidate has been hired and the vacancy is officially closed VACANCY MOVES TO ARCHIVE
                { id: 4, name: 'Cancelled' } // - vacancy is no longer valid and not needed any more, no one is hired. VACANCY MOVES TO ARCHIVE
            ],
            substatuses: [
                { id: 0, name: 'Test Failed' },
                { id: 1, name: 'Interview Failed' },
                { id: 2, name: 'Passed' }
            ],
            cardStages: [
                { id: 0, name: 'Long Listed' },
                { id: 1, name: 'Test Send' },
                { id: 2, name: 'Test Done' },
                { id: 3, name: 'Interview' },
                { id: 4, name: 'Test Failed' },
                { id: 5, name: 'Interview Failed' },
                { id: 6, name: 'Passed' }
            ],
            feedbackTypes: [
                { id: 0, name: 'English' },
                { id: 1, name: 'Personal' },
                { id: 2, name: 'Expertise' },
                { id: 3, name: 'Tech Feedback' },
                { id: 4, name: 'Test Feedback' },
                { id: 5, name: 'Summary' }
            ],
            poolBackgroundColors: [
                { id: 0, color: 'Green', colorCode: 'rgb(44, 201, 99)' },
                { id: 1, color: 'Yellow', colorCode: 'rgb(293, 250, 85)' },
                { id: 2, color: 'Red', colorCode: 'rgb(240, 88, 88)' },
                { id: 3, color: 'Orange', colorCode: 'rgb(245,122 , 14)' },
                { id: 4, color: 'Aquamarine', colorCode: 'rgb(76, 222, 181)' },
                { id: 5, color: 'Purple', colorCode: 'rgb(181, 60, 181)' },
                { id: 6, color: 'Blue', colorCode: 'rgb(87, 166, 235)' }
            ],
            fileType: {
                Resume: 0,
                Test: 1,
                Other: 2
            },
            successStatus: {
                'None': 0,
                'Like': 1,
                'Dislike': 2
            },
            voteColors: {
                'None': 'grey',
                'Like': 'green',
                'Dislike': 'red'
            }
        });
})();