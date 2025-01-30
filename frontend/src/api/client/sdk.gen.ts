// This file is auto-generated by @hey-api/openapi-ts

import { createClient, createConfig, type Options } from '@hey-api/client-fetch';
import type { GetApiPilotsAllData, GetApiPilotsAllResponse, GetApiResultsForPilotData, GetApiResultsForPilotResponse, GetApiCompetitionsCurrentData, GetApiCompetitionsCurrentResponse, GetApiDashboardData, GetApiDashboardResponse } from './types.gen';

export const client = createClient(createConfig());

export const getApiPilotsAll = <ThrowOnError extends boolean = false>(options?: Options<GetApiPilotsAllData, ThrowOnError>) => {
    return (options?.client ?? client).get<GetApiPilotsAllResponse, unknown, ThrowOnError>({
        ...options,
        url: '/api/pilots/All'
    });
};

export const getApiResultsForPilot = <ThrowOnError extends boolean = false>(options?: Options<GetApiResultsForPilotData, ThrowOnError>) => {
    return (options?.client ?? client).get<GetApiResultsForPilotResponse, unknown, ThrowOnError>({
        ...options,
        url: '/api/results/ForPilot'
    });
};

export const getApiCompetitionsCurrent = <ThrowOnError extends boolean = false>(options?: Options<GetApiCompetitionsCurrentData, ThrowOnError>) => {
    return (options?.client ?? client).get<GetApiCompetitionsCurrentResponse, unknown, ThrowOnError>({
        ...options,
        url: '/api/competitions/current'
    });
};

export const getApiDashboard = <ThrowOnError extends boolean = false>(options?: Options<GetApiDashboardData, ThrowOnError>) => {
    return (options?.client ?? client).get<GetApiDashboardResponse, unknown, ThrowOnError>({
        ...options,
        url: '/api/dashboard'
    });
};