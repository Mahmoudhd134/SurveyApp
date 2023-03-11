import { baseApi } from "../../App/Api/baseApi";

export const SurveyAnswerApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        addSurveyAnswer: builder.mutation<boolean, { surveyId: number, optionId: number }>({
            query: (arg) => `surveyanswer/add/${arg.surveyId}/${arg.optionId}`,
            invalidatesTags: (r, e, a) => [
                'survey',
                { type: 'survey', id: a.surveyId }
            ]
        }),
        removeSurveyAnswer: builder.mutation<boolean, number>({
            query: arg => ({
                url: `surveyanswer/remove/` + arg,
                method: 'delete'
            }),
            invalidatesTags: (r, e, a) => [
                'survey',
                { type: 'survey', id: a }
            ]
        })
    })
})

export const
    { useAddSurveyAnswerMutation,
        useRemoveSurveyAnswerMutation } = SurveyAnswerApi