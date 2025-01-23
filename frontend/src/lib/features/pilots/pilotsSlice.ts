import { getApiPilotsAll } from "../../../api/client";
import { createAppSlice } from "../../createAppSlice";

export interface PilotsSliceState {
  state: "Idle" | "Loading" | "Loaded" | "Error";
  pilots: string[];
}

const initialState: PilotsSliceState = {
  state: "Idle",
  pilots: [],
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
  }),

  selectors: {
    selectPilotsState: (state) => state.state,
    selectPilots: (state) => state.pilots,
  },
});

export const { fetchPilots } = pilotsSlice.actions;

export const { selectPilotsState, selectPilots } = pilotsSlice.selectors;
