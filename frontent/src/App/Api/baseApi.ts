import { BaseQueryFn, FetchArgs, FetchBaseQueryError, createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { setCredentials, logout } from '../../Feutures/Auth/authSlice'
import { RootState } from '../store'
import useRefreshToken from '../../Hookes/useRefreshToken'
import jwtDecode from 'jwt-decode'


export const BASE_URL = 'https://localhost:7108/api/'

export const sendDefualt = fetchBaseQuery({
    baseUrl: BASE_URL,
    credentials: 'include',
    prepareHeaders: (headers, { getState }) => {
        const token = (getState() as RootState).auth.token
        if (token != null)
            headers.append('authorization', `Bearer ${token}`)
    }
})

const baseQuery: BaseQueryFn<
    string | FetchArgs,
    unknown,
    FetchBaseQueryError
> = async (args, api, extraOptions) => {
    const tokenExp = (api.getState() as RootState).auth.tokenExp
    if (tokenExp != null) {
        const now = +new Date().getTime().toString().slice(0, -3)
        if (now >= tokenExp) {
            const refrshToken = useRefreshToken()
            const data = await refrshToken()
            if (data) {
                api.dispatch(setCredentials(data))
            } else {
                api.dispatch(logout())
            }
        }
    }
    return await sendDefualt(args, api, extraOptions)
}

export const baseApi = createApi({
    reducerPath: 'api',
    baseQuery,
    tagTypes: ['user', 'category', 'survey'],
    endpoints: builder => ({})
})