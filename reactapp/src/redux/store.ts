import { configureStore } from "@reduxjs/toolkit";
import userReducer from './userSlice';
import claimsSlice from "./claimsSlice";
import loginSlice from "./loginSlice";
import { JWTClaims, b64DecodeUnicode, b64EncodeUnicode } from "../generalUtils";
import Cookies from "js-cookie";

const appStateName = '_8bcsk999r778311o_s_app';

function loadState() {
    try {
        const base64State = Cookies.get(appStateName);
        if (base64State === undefined) {
            return undefined;
        }
        const serializedState = b64DecodeUnicode(base64State);
        return JSON.parse(serializedState);
    } catch (e: any) {
        console.error('Error loading application state.', e);
        return undefined;
    }
}

function saveState(state: any) {
    const serializedB64State = b64EncodeUnicode(JSON.stringify(state));
    if ((state.claims as JWTClaims).exp != 0)
        Cookies.set(appStateName, serializedB64State, { sameSite: 'strict', secure: true, expires: new Date((state.claims as JWTClaims).exp * 1000) });
    else
        Cookies.set(appStateName, serializedB64State, { sameSite: 'strict', secure: true });
}

export const store = configureStore({
    reducer: {
        user: userReducer,
        claims: claimsSlice,
        loginState: loginSlice
    },
    preloadedState: loadState()
});

store.subscribe(() => {
    saveState(store.getState());
});

export type RootState = ReturnType<typeof store.getState>;