import { RootState } from "@/lib/store";
import {
  getApiPilotsAll,
  getApiResultsForPilot,
  PilotResult,
} from "../../../api/client";
import { createAppSlice } from "../../createAppSlice";

export interface PilotsSliceState {
  state: "Idle" | "Loading" | "Loaded" | "Error";
  pilots: string[];
  pilotResults: Record<string, PilotResult[]>;
  selectPilotResultLoadingState: Record<
    string,
    "Idle" | "Loading" | "Loaded" | "Error"
  >;
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
} = pilotsSlice.selectors;
