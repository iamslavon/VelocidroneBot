import { getApiHeatmapForPilot, HeatmapEntry } from "@/api/client";
import { createAppSlice } from "@/lib/createAppSlice";
import { PayloadAction } from "@reduxjs/toolkit";

export interface HeatmapSliceState {
  state: "Idle" | "Loading" | "Loaded" | "Error";
  currentPilot: string | null;
  heatmap: HeatmapEntry[];
}

const initialState: HeatmapSliceState = {
  state: "Idle",
  heatmap: [],
  currentPilot: null,
};

export const heatmapSlice = createAppSlice({
  name: "heatmap",
  initialState: initialState,
  reducers: (create) => ({
    fetchHeatmap: create.asyncThunk(
      async (pilotName: string) => {
        const result = await getApiHeatmapForPilot({
          query: { pilotName: pilotName },
        });
        return result.data;
      },
      {
        pending: (state) => {
          state.state = "Loading";
        },
        fulfilled: (state, action) => {
          state.state = "Loaded";
          state.heatmap = action.payload || [];
        },
        rejected: (state) => {
          state.state = "Error";
          state.heatmap = [];
        },
      }
    ),

    choosePilot: create.reducer((state, action: PayloadAction<string>) => {
      state.currentPilot = action.payload;
      state.heatmap = [];
    }),
  }),

  selectors: {
    selectHeatmapState: (state) => state.state,
    selectCurrentHeatmap: (state) => state.heatmap,
    selectCurrentPilot: (state) => state.currentPilot,
  },
});

export const { fetchHeatmap, choosePilot } = heatmapSlice.actions;

export const { selectHeatmapState, selectCurrentHeatmap, selectCurrentPilot } =
  heatmapSlice.selectors;
