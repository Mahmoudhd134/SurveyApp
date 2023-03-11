import { baseApi } from "../../App/Api/baseApi";

export const SurveyCategoryApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        addSurveyCategory: builder.mutation<boolean, { surveyId: number, categoryId: number }>({
            query: args => `surveycategory/add/${args.surveyId}/${args.categoryId}`,
            invalidatesTags: (result, error, args) => [{ type: 'survey', id: args.surveyId }]
        }),
        addSurveyCategoryByName: builder.mutation<boolean, { surveyId: number, categoryName: string }>({
            query: args => `surveycategory/addByName/${args.surveyId}/${args.categoryName}`,
            invalidatesTags: (result, error, args) => [{ type: 'survey', id: args.surveyId }]
        }),
        removeSurveyCategory: builder.mutation<boolean, { surveyId: number, categoryId: number }>({
            query: args => ({
                url: `surveycategory/remove/${args.surveyId}/${args.categoryId}`,
                method: 'delete'
            }),
            invalidatesTags: (result, error, args) => [{ type: 'survey', id: args.surveyId }]
        })
    })
})

export const { useAddSurveyCategoryMutation,
    useAddSurveyCategoryByNameMutation,
    useRemoveSurveyCategoryMutation } = SurveyCategoryApi