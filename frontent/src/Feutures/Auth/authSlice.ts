import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import TokenModel from '../../Models/Auth/TokenModel'
import jwtDecode from 'jwt-decode'

const initialState: TokenModel = {
    roles: null,
    token: null,
    tokenExp: null
}

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        setCredentials: (state, action: PayloadAction<Omit<TokenModel, 'tokenExp'>>) => {
            state.roles = action.payload.roles
            state.token = action.payload.token
            state.tokenExp = (jwtDecode(action.payload.token!) as { exp: number })?.exp
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