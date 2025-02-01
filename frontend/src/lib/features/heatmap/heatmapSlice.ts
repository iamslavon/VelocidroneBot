import { createAppSlice } from "@/lib/createAppSlice";
import { PayloadAction } from "@reduxjs/toolkit";

export interface HeatmapSliceState {
  state: "Idle" | "Loading" | "Loaded" | "Error";
  currentPilot: string | null;
}

const initialState: HeatmapSliceState = {
  state: "Idle",
  currentPilot: null,
};

export const heatmapSlice = createAppSlice({
  name: "heatmap",
  initialState: initialState,
  reducers: (create) => ({
    choosePilot: create.reducer((state, action: PayloadAction<string>) => {
      state.currentPilot = action.payload;
    }),
  }),

  selectors: {
    selectCurrentPilot: (state) => state.currentPilot,
  },
});

export const { choosePilot } = heatmapSlice.actions;

export const { selectCurrentPilot } = heatmapSlice.selectors;
