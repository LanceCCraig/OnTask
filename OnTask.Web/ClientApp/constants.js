const Constants = {
    TOKEN_STORAGE_KEY: 'token',

    ACCOUNT_API_URL: 'api/Account',
    EVENT_API_URL: 'api/Event',
    EVENT_GROUP_API_URL: 'api/EventGroup',
    EVENT_PARENT_API_URL: 'api/EventParent',
    EVENT_TYPE_API_URL: 'api/EventType',
    RECOMMENDATION_API_URL: 'api/Recommendation',

    MODE_BY_TYPE: 'Type',
    MODE_BY_GROUP: 'Group',
    MODE_BY_PARENT: 'Parent',

    MOMENT_DATE_FORMAT: 'YYYY-MM-DD',
    MOMENT_TIME_FORMAT: 'HH:mm',

    DAYS_OF_WEEK: [
        'Sunday',
        'Monday',
        'Tuesday',
        'Wednesday',
        'Thursday',
        'Friday',
        'Saturday'
    ],

    DEFAULT_RECALCULATION_MODE: 'MinimalClustering',
    RECALCULATION_MODES: [
        {
            name: 'Minimize Events Per Day',
            value: 'MinimalClustering'
        },
        {
            name: 'Prioritize Importance',
            value: 'PriorityRespective'
        }
    ],

    WEIGHTS: [
        {
            name: 'Very Low',
            value: 5
        },
        {
            name: 'Low',
            value: 4
        },
        {
            name: 'Medium',
            value: 3
        },
        {
            name: 'High',
            value: 2
        },
        {
            name: 'Very High',
            value: 1
        }
    ]
};

export default Constants;
