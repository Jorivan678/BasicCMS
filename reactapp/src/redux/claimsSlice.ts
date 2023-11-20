import { createSlice } from "@reduxjs/toolkit";
import { JWTClaims } from "../generalUtils";

const initialState: JWTClaims = {
    exp: 0,
    iat: 0,
    nameid: 0,
    role: []
};

export const claimsSlice = createSlice({
    name: 'claims',
    initialState,
    reducers: {
        setClaims: (state, action) => {
            const payload: JWTClaims = action.payload;
            state.nameid = payload.nameid;
            state.iat = payload.iat;
            state.exp = payload.exp;
            state.role = payload.role;
        },
        removeClaims: (state) => {
            state.nameid = initialState.nameid;
            state.iat = initialState.iat;
            state.exp = initialState.exp;
            state.role = initialState.role;
        }
    }
});

export const { setClaims, removeClaims } = claimsSlice.actions;
export default claimsSlice.reducer;