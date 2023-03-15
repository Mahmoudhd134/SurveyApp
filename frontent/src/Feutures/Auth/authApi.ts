import { baseApi } from "../../App/Api/baseApi";
import LoginModel from "../../Models/Auth/LoginModel";
import TokenModel from "../../Models/Auth/TokenModel";
import RegisterModel from "../../Models/Auth/RegisterModel";

export const authApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        login: builder.mutation<TokenModel, LoginModel>({
            query: (cred) => ({
                url: '/auth/login',
                method: 'Post',
                body: cred
            }),
        }),
        signup: builder.mutation<boolean, RegisterModel>({
            query: (data) => ({
                url: '/auth/register',
                method: 'Post',
                body: data
            }),
        }),
        getStatus: builder.query<{ message: string }, void>({
            query: () => '/auth/status',
            keepUnusedDataFor: 0
        })
    })
})

export const { useLoginMutation, useSignupMutation, useGetStatusQuery } = authApi