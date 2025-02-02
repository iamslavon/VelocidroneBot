// This file is auto-generated by @hey-api/openapi-ts

export type CompetitionModel = {
    id: string;
    startedOn: string;
    state: CompetitionState;
    mapName: string;
    trackName: string;
    trackId: number;
    mapId: number;
};

export type CompetitionModel2 = {
    id: string;
    startedOn: string;
    state: CompetitionState;
    mapName: string;
    trackName: string;
    trackId: number;
    mapId: number;
} | null;

export type CompetitionState = number;

export type DashboardModel = {
    competition?: CompetitionModel2;
    results: Array<TrackTimeModel>;
    leaderboard: Array<SeasonResultModel>;
};

export type PilotResult = {
    date: string;
    points: number;
    trackTime: number;
};

export type SeasonResultModel = {
    playerName: string;
    points: number;
};

export type TrackTimeModel = {
    playerName: string;
    time: number;
    globalRank: number;
    localRank: number;
};

export type GetApiMigrationPilotsData = {
    body?: never;
    path?: never;
    query?: never;
    url: '/api/migration/pilots';
};

export type GetApiMigrationPilotsResponses = {
    /**
     * OK
     */
    200: unknown;
};

export type GetApiPilotsAllData = {
    body?: never;
    path?: never;
    query?: never;
    url: '/api/pilots/All';
};

export type GetApiPilotsAllResponses = {
    /**
     * OK
     */
    200: Array<string>;
};

export type GetApiPilotsAllResponse = GetApiPilotsAllResponses[keyof GetApiPilotsAllResponses];

export type GetApiResultsForPilotData = {
    body?: never;
    path?: never;
    query?: {
        pilotName?: string;
    };
    url: '/api/results/ForPilot';
};

export type GetApiResultsForPilotResponses = {
    /**
     * OK
     */
    200: Array<PilotResult>;
};

export type GetApiResultsForPilotResponse = GetApiResultsForPilotResponses[keyof GetApiResultsForPilotResponses];

export type GetApiCompetitionsCurrentData = {
    body?: never;
    path?: never;
    query?: never;
    url: '/api/competitions/current';
};

export type GetApiCompetitionsCurrentResponses = {
    /**
     * OK
     */
    200: Array<CompetitionModel>;
};

export type GetApiCompetitionsCurrentResponse = GetApiCompetitionsCurrentResponses[keyof GetApiCompetitionsCurrentResponses];

export type GetApiDashboardData = {
    body?: never;
    path?: never;
    query?: never;
    url: '/api/dashboard';
};

export type GetApiDashboardResponses = {
    /**
     * OK
     */
    200: DashboardModel;
};

export type GetApiDashboardResponse = GetApiDashboardResponses[keyof GetApiDashboardResponses];