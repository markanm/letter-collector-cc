import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { AppThunk } from "../../store";
import { analyzeService } from "../../services/AnalyzerService";
import { Map } from "../../types/Map";

const currentMapSlice = createSlice({
  name: "currentMap",
  initialState: {
    mapText: "",
    analyzed: false,
    valid: false,
    letters: "",
    pathAsCharacters: "",
    errorMessage: "",
  },
  reducers: {
    textUpdated: {
      reducer(state, action: PayloadAction<{ message: string }>) {
        return {
          ...state,
          mapText: action.payload.message,
          analyzed: false,
          valid: false,
          letters: "",
          pathAsCharacters: "",
          errorMessage: "",
        };
      },
      prepare(message: string) {
        return {
          payload: {
            message: message,
          },
        };
      },
    },
    pathAnalyzedOk: {
      reducer(
        state,
        action: PayloadAction<{
          letters: string;
          pathAsCharacters: string;
        }>
      ) {
        return {
          ...state,
          analyzed: true,
          valid: true,
          letters: action.payload.letters,
          pathAsCharacters: action.payload.pathAsCharacters,
          errorMessage: "",
        };
      },
      prepare(letters: string, pathAsCharacters: string) {
        return {
          payload: {
            letters: letters,
            pathAsCharacters: pathAsCharacters,
          },
        };
      },
    },
    pathAnalyzedErr: {
      reducer(
        state,
        action: PayloadAction<{
          errorMessage: string;
        }>
      ) {
        state.errorMessage = action.payload.errorMessage;
        state.letters = "";
        state.pathAsCharacters = "";
        state.valid = false;
        state.analyzed = true;
      },
      prepare(errorMessage: string) {
        return {
          payload: {
            errorMessage: errorMessage,
          },
        };
      },
    },
  },
});

export const { textUpdated, pathAnalyzedOk, pathAnalyzedErr } =
  currentMapSlice.actions;

export const selectCurrent = (state: { currentMapSlice: Map }) =>
  state.currentMapSlice;

export const analyzeMap = (): AppThunk => {
  return async (dispatch, getState) => {
    try {
      const stateBefore = getState();
      const result = await analyzeService.analyzePath(
        stateBefore.currentMapSlice.mapText.split("\n")
      );

      dispatch(pathAnalyzedOk(result.letters, result.pathAsCharacters));
    } catch (err) {
      if (
        typeof err === "object" &&
        err &&
        "message" in err &&
        typeof err.message === "string"
      ) {
        dispatch(pathAnalyzedErr(err.message));
      }
    }
  };
};

export default currentMapSlice.reducer;
