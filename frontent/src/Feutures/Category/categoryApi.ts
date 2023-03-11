import { baseApi } from "../../App/Api/baseApi";
import AddCategory from "../../Models/Category/AddCategory";
import Category from "../../Models/Category/Category";
import CategoryForPage from "../../Models/Category/CategoryForPage";
import EditCategory from "../../Models/Category/EditCategory";

export const categoryApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getCategoryPage: builder.query<CategoryForPage[], { pageSize: number, pageIndex: number, prefix: string }>({
            query: ({ pageIndex, pageSize, prefix }) => `category/page/${pageIndex}/${pageSize}/${prefix}`,
            providesTags: (result = [], error, arg) =>
                [
                    ...result.map(({ id }) => ({ type: 'category' as const, id })),
                    { type: 'category' }
                ]
        }),
        getCategoryPyId: builder.query<Category, number>({
            query: (arg) => 'category/' + arg,
            providesTags: (result, error, arg) => [{ type: 'category', id: arg }]
        }),
        addCategory: builder.mutation<boolean, AddCategory>({
            query: (arg) => ({
                url: 'category/add',
                method: 'Post',
                body: arg
            }),
            invalidatesTags: ['category']
        }),
        editCategory: builder.mutation<boolean, EditCategory>({
            query: (arg) => ({
                url: 'category/edit',
                method: 'put',
                body: arg
            }),
            invalidatesTags: (result, error, { id }) => [{ type: 'category', id }]
        }),
        deleteCategory: builder.mutation<boolean, number>({
            query: (arg) => ({
                url: 'category/remove/' + arg,
                method: 'delete'
            }),
            invalidatesTags: (result, error, arg) => [{ type: 'category', id: arg }]
        })
    })
})

export const {
    useGetCategoryPageQuery,
    useGetCategoryPyIdQuery,
    useAddCategoryMutation,
    useEditCategoryMutation,
    useDeleteCategoryMutation } = categoryApi