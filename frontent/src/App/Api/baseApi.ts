import { BaseQueryFn, FetchArgs, FetchBaseQueryError, createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { setCredentials, logout } from '../../Feutures/Auth/authSlice'
import { RootState } from '../store'
import TokenModel from '../../Models/Auth/TokenModel'
import useRefreshToken from '../../Hookes/useRefreshToken'

export interface AppError {
    code: string,
    message: string
}

export const BASE_URL = 'https://localhost:7108/api/'

export const sendDefualt = fetchBaseQuery({
    baseUrl: BASE_URL,
    credentials: 'include',
    prepareHeaders: (headers, { getState }) => {
        const token = (getState() as RootState).auth.token
        if (token != null)
            headers.append('authorization', `Bearer ${token}`)
        // headers.append('Content-Type','application/json')
    }
})

const baseQuery: BaseQueryFn<
    string | FetchArgs,
    unknown,
    FetchBaseQueryError
> = async (args, api, extraOptions) => {
    let result = await sendDefualt(args, api, extraOptions)
    if (result.error && result.error.status === 401) {
        const refrshToken = useRefreshToken()
        const data = await refrshToken()
        if (data) {
            api.dispatch(setCredentials(data as TokenModel))
            result = await sendDefualt(args, api, extraOptions)
        } else {
            api.dispatch(logout())
        }
    }
    return result
}

export const baseApi = createApi({
    reducerPath: 'api',
    baseQuery,
    tagTypes: ['user','category','survey'],
    endpoints: builder => ({})
})