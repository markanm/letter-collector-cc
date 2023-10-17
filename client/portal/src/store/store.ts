import {
  AnyAction,
  ThunkAction,
  combineReducers,
  configureStore,
} from "@reduxjs/toolkit";
import { currentMapSlice } from "../features/currentMap";

const rootReducer = combineReducers({ currentMapSlice });

const store = configureStore({
  reducer: {
    currentMapSlice: currentMapSlice,
  },
});

export type RootState = ReturnType<typeof rootReducer>;
export type AppDispatch = typeof store.dispatch

export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  AnyAction
>;

export default store;
