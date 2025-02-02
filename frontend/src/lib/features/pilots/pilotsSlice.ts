import { RootState } from "@/lib/store";
import {
  getApiPilotsAll,
  getApiResultsForPilot,
  PilotResult,
} from "../../../api/client";
import { createAppSlice } from "../../createAppSlice";
import { LoadingStates } from "@/lib/loadingStates";

export interface PilotsSliceState {
  state: LoadingStates;
  pilots: string[];
  pilotResults: Record<string, PilotResult[]>;
  selectPilotResultLoadingState: Record<string, LoadingStates>;
}

const initialState: PilotsSliceState = {
  state: "Idle",
  pilots: [],
  pilotResults: {},
  selectPilotResultLoadingState: {},
};

export const pilotsSlice = createAppSlice({
  name: "pilots",
  initialState: initialState,
  reducers: (create) => ({
    /**
     * Load list of all pilots.
     */
    fetchPilots: create.asyncThunk(
      async () => {
        const result = await getApiPilotsAll();
        return result.data;
      },
      {
        pending: (state) => {
          state.state = "Loading";
        },
        fulfilled: (state, action) => {
          state.state = "Loaded";
          state.pilots = action.payload || [];
        },
        rejected: (state) => {
          state.state = "Error";
          state.pilots = [];
        },
      }
    ),

    /**
     * Loads results for pilot. If results are presented in store,
     * cached data is used.
     */
    fetchPilotResults: create.asyncThunk(
      async (pilotName: string) => {
        const result = await getApiResultsForPilot({
          query: { pilotName: pilotName },
        });
        return result.data;
      },
      {
        pending: (state, action) => {
          const pilotName = action.meta.arg;
          state.selectPilotResultLoadingState[pilotName] = "Loading";
        },
        fulfilled: (state, action) => {
          const pilotName = action.meta.arg;
          state.selectPilotResultLoadingState[pilotName] = "Loaded";
          state.pilotResults[pilotName] = action.payload || [];
        },
        rejected: (state, action) => {
          const pilotName = action.meta.arg;
          state.selectPilotResultLoadingState[pilotName] = "Error";
          state.pilotResults[pilotName] = [];
        },
        options: {
          condition: (pilotName, thunkApi) => {
            const state = (thunkApi.getState() as RootState).pilots;

            if (state.selectPilotResultLoadingState[pilotName] == "Loaded") {
              return false;
            }
            if (state.selectPilotResultLoadingState[pilotName] == "Loading") {
              return false;
            }

            return true;
          },
        },
      }
    ),
  }),

  selectors: {
    selectPilotsState: (state) => state.state,

    selectPilots: (state) => state.pilots,

    selectPilotResults: (state, pilotName: string | null) =>
      pilotName ? state.pilotResults[pilotName] : [],

    selectPilotsResults: (state, pilots: (string | null)[]) => {
      return pilots.map((p) => (p ? state.pilotResults[p] || [] : []));
    },

    selectPilotResultsLoadingState: (
      state,
      pilots: (string | null)[]
    ): LoadingStates => {
      const states = pilots
        .filter((p) => p != null)
        .map((p) => state.selectPilotResultLoadingState[p]);

      if (states.length === 0) return "Idle";
      if (states.find((s) => s === "Loading")) return "Loading";

      return "Loaded";
    },

    selectPilotResultLoadingState: (state, pilotName: string | null) =>
      pilotName ? state.selectPilotResultLoadingState[pilotName] : "Idle",
  },
});

export const { fetchPilots, fetchPilotResults } = pilotsSlice.actions;

export const {
  selectPilotsState,
  selectPilots,
  selectPilotResults,
  selectPilotResultLoadingState,
  selectPilotsResults,
  selectPilotResultsLoadingState,
} = pilotsSlice.selectors;
