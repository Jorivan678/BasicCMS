import { createSlice } from "@reduxjs/toolkit";
import { UserResponse } from "../models";

const initialState: UserResponse = {
    id: 0,
    nombreUsuario: '',
    nombre: '',
    apellidoP: '',
    apellidoM: ''
};

export const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        setUser: (state, action) => {
            const payload: UserResponse = action.payload;
            state.id = payload.id;
            state.nombre = payload.nombre;
            state.apellidoP = payload.apellidoP;
            state.apellidoM = payload.apellidoM;
            state.nombreUsuario = payload.nombreUsuario;
        },
        removeUser: (state) => {
            state.id = initialState.id;
            state.nombreUsuario = initialState.nombreUsuario;
            state.apellidoP = initialState.apellidoP;
            state.apellidoM = initialState.apellidoM;
            state.nombre = initialState.nombre;
        },
    }
});

export const { setUser, removeUser } = userSlice.actions;
export default userSlice.reducer;