import { createSlice } from "@reduxjs/toolkit";

const initialState = { isLoggedIn: false };

export const loginSlice = createSlice({
    name: 'loginState',
    initialState,
    reducers: {
        setLoginState: (state, action) => {
            state.isLoggedIn = action.payload;
        }
    }
});

export const { setLoginState } = loginSlice.actions;
export default loginSlice.reducer;