import { createAppSlice } from "../../createAppSlice";
import api from "../../../api/api";
import { DashboardModel } from "../../../api/client";
import { LoadingStates } from "@/lib/loadingStates";

export interface DashboardSliceState {
  state: LoadingStates;
  data: DashboardModel | null;
}

const initialState: DashboardSliceState = {
  state: "Idle",
  data: null,
};

export const dashboardSlice = createAppSlice({
  name: "dashboard",
  initialState: initialState,
  reducers: (create) => ({
    fetch: create.asyncThunk(
      async () => {
        const response = await api.getDashboard();
        return response.data;
      },
      {
        pending: (state) => {
          state.state = "Loading";
        },
        fulfilled: (state, action) => {
          state.state = "Loaded";
          state.data = action.payload!;
        },
        rejected: (state) => {
          state.state = "Error";
          state.data = null;
        },
      }
    ),
  }),

  selectors: {
    selectState: (state) => state.state,
    selectData: (state) => state.data,
  },
});

export const { fetch } = dashboardSlice.actions;

export const { selectState, selectData } = dashboardSlice.selectors;
