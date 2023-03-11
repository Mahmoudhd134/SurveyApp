import { baseApi } from "../../App/Api/baseApi";
import { AddSurveyOptionModel } from "../../Models/SurveyOption/AddSurveyOptionModel";

export const SurveyOptionApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        addSurveyOption: builder.mutation<boolean, AddSurveyOptionModel>({
            query: args => ({
                url: 'surveyoption/add',
                method: 'post',
                body: args
            }),
            invalidatesTags: (result, error, args) => [{ type: 'survey', id: args.surveyId }]
        }),
        removeSurveyOption: builder.mutation<boolean, number>({
            query: args => ({
                url: 'surveyoption/remove/' + args,
                method: 'delete'
            }),
            invalidatesTags: ['survey'] //todo specify the id for the survey
        })
    })
})

export const {
    useAddSurveyOptionMutation,
    useRemoveSurveyOptionMutation } = SurveyOptionApi