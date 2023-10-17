import {
  AnyAction,
  ThunkAction,
  combineReducers,
  configureStore,
} from "@reduxjs/toolkit";
import { currentMap } from "../features/currentMap";
import { savedMaps } from "../features/savedMaps";

const rootReducer = combineReducers({ currentMap, savedMaps });

const store = configureStore({
  reducer: {
    currentMap: currentMap,
    savedMaps: savedMaps,
  },
});

export type RootState = ReturnType<typeof rootReducer>;
export type AppDispatch = typeof store.dispatch;

export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  AnyAction
>;

export default store;
