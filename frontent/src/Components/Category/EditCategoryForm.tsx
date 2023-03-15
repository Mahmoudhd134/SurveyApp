import { useEffect, useState } from "react"
import EditCategory from "../../Models/Category/EditCategory"
import { useEditCategoryMutation, useGetCategoryPyIdQuery } from "../../Feutures/Category/categoryApi"
import { useNavigate, useParams } from "react-router-dom"
import useGetAppError from "../../Hookes/useGetAppError"

const EditCategoryForm = () => {
    const { id } = useParams()
    const categoryResult = useGetCategoryPyIdQuery(+id!)
    const [edit, result] = useEditCategoryMutation()
    const [category, setCategory] = useState<EditCategory>({ id: 0, name: '' })
    const navigator = useNavigate()

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault()
        edit(category)
    }

    useEffect(() => {
        if (result.isSuccess)
            navigator('/category/page/0')
    }, [result.isSuccess])

    useEffect(() => {
        if (categoryResult.isSuccess)
            setCategory({ id: categoryResult.data.id, name: categoryResult.data.name })
    }, [categoryResult.isSuccess])

    if (categoryResult.isFetching)
        return <h1>Loading...</h1>


    return (
        <main className='container mt-5'>
            <form onSubmit={handleSubmit} >
                <div className="w-100 row justify-content-center my-3">
                    <div className="col-12 col-sm-8 col-md-6 d-flex justify-content-center text-center text-danger">
                        <p>{result.isError && useGetAppError(result.error)?.message}</p>
                    </div>
                </div>

                <div className="w-100 row justify-content-center">
                    <div className="col-12 col-sm-8 col-md-6 p-3 border border-3 rounded rounded-3 shadow-lg">
                        <label htmlFor="name" className="form-label">Name</label>
                        <input type="text"
                            id="name"
                            className="form-control"
                            value={category.name}
                            onChange={e => setCategory(p => ({ ...p, name: e.target.value }))} />
                    </div>
                </div>

                <div className="w-100 row justify-content-center my-3">
                    <div className="col-12 col-sm-8 col-md-6 d-flex justify-content-center">
                        <button className="btn btn-outline-dark w-50">Edit</button>
                    </div>
                </div>
            </form>
        </main>
    )
}

export default EditCategoryForm
