import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { Map } from "../../types";
import { AppThunk } from "../../store";

const initialState: Map[] = JSON.parse(
  localStorage.getItem("savedMaps") || "[]"
);

const savedMaps = createSlice({
  name: "savedMaps",
  initialState: {
    maps: initialState,
  },
  reducers: {
    add(state, action: PayloadAction<Map>) {
      if (!state.maps.find((item) => item.mapText === action.payload.mapText)) {
        state.maps.push(action.payload);
      }
    },
    remove(state, action: PayloadAction<Map>) {
      state.maps.filter((item) => item.mapText != action.payload.mapText);
    },
    clear(state) {
      return { ...state, maps: [] };
    },
  },
});

export const { add, remove, clear } = savedMaps.actions;

export const selectSaved = (state: { savedMaps: { maps: Map[] } }) =>
  state.savedMaps.maps;

export const saveMap = (): AppThunk => {
  return async (dispatch, getState) => {
    const initialState = getState();
    dispatch(add(initialState.currentMap));

    localStorage.setItem(
      "savedMaps",
      JSON.stringify(getState().savedMaps.maps)
    );
  };
};

export default savedMaps.reducer;
