import { baseApi } from "../../App/Api/baseApi";
import { AddSurveyModel } from "../../Models/Survey/AddSurveyModel";
import { EditSurveyModel } from "../../Models/Survey/EditSurveyModel";
import { SurveyForPageModel } from "../../Models/Survey/SurveyForPageModel";
import { SurveyModel } from "../../Models/Survey/SurveyModel";
import SurveyQuestionModel from "../../Models/Survey/SurveyQuestionModel";

export const SurveyApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getSurveysPage: builder.query<SurveyForPageModel[], { categoryId: number, pageIndex: number, pageSize: number }>({
            query: ({ categoryId, pageIndex, pageSize }) => `survey/page/${categoryId}/${pageIndex}/${pageSize}`,
            providesTags: (result = [], error, args) => [
                'survey',
                ...result.map(({ id }) => ({ type: 'survey' as const, id }))
            ]
        }),
        getSurvey: builder.query<SurveyModel, number>({
            query: arg => 'survey/get/' + arg,
            providesTags: (result, error, args) => [{ type: 'survey', id: args }]
        }),
        getSurveyQuestion: builder.query<SurveyQuestionModel, number>({
            query: arg => 'survey/getquestion/' + arg,
            providesTags: (result, error, args) => [{ type: 'survey', id: args }]
        }),
        addSurvey: builder.mutation<boolean, AddSurveyModel>({
            query: arg => ({
                url: 'survey/add',
                method: 'Post',
                body: arg
            }),
            invalidatesTags: ['survey']
        }),
        editSurvey: builder.mutation<boolean, EditSurveyModel>({
            query: arg => ({
                url: 'survey/edit',
                method: 'put',
                body: arg
            }),
            invalidatesTags: (result, error, arg) => [{ type: 'survey', id: arg.id }]
        }),
        removeSurvey: builder.mutation<boolean, number>({
            query: arg => ({
                url: 'survey/remove/' + arg,
                method: 'delete'
            }),
            invalidatesTags: (result, error, arg) => [{ type: 'survey', id: arg }]
        })
    })
})

export const
    { useGetSurveysPageQuery,
        useGetSurveyQuery,
        useGetSurveyQuestionQuery,
        useAddSurveyMutation,
        useEditSurveyMutation,
        useRemoveSurveyMutation } = SurveyApi