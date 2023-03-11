import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import TokenModel from '../../Models/Auth/TokenModel'

const initialState: TokenModel = {
    roles: null,
    token: null
}

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        setCredentials: (state, action: PayloadAction<TokenModel>) => {
            state.roles = action.payload.roles
            state.token = action.payload.token
        },
        logout: (state) => {
            localStorage.removeItem('staylogin')
            state.roles = null
            state.token = null
        }
    }
})

export default authSlice.reducer
export const { logout, setCredentials } = authSlice.actions