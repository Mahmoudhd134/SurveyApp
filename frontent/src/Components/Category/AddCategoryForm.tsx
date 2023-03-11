import React, { useEffect, useState } from "react"
import { useAddCategoryMutation } from "../../Feutures/Category/categoryApi"
import AddCategory from "../../Models/Category/AddCategory"
import useGetAppError from "../../Hookes/useGetAppError"
import { useNavigate } from "react-router-dom"

export const AddCategoryForm = () => {
    const [add, result] = useAddCategoryMutation()
    const [category, setCategory] = useState<AddCategory>({ name: '' })
    const navigator = useNavigate()

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault()
        add(category)
    }

    useEffect(() => {
        if (result.isSuccess)
            navigator('/category/page/0')
    }, [result.isSuccess])

    return (
        <main className='container mt-5'>
            <form onSubmit={handleSubmit} >
                <div className="w-100 row justify-content-center my-3">
                    <div className="col-12 col-sm-8 col-md-6 d-flex justify-content-center text-center text-danger">
                        <p>{result.isError && useGetAppError(result.error).message}</p>
                    </div>
                </div>

                <div className="w-100 row justify-content-center">
                    <div className="col-12 col-sm-8 col-md-6 p-3 border border-3 rounded rounded-3 shadow-lg">
                        <label htmlFor="name" className="form-label">Name</label>
                        <input type="text"
                            id="name"
                            className="form-control"
                            value={category.name}
                            onChange={e => setCategory({ name: e.target.value })} />
                    </div>
                </div>

                <div className="w-100 row justify-content-center my-3">
                    <div className="col-12 col-sm-8 col-md-6 d-flex justify-content-center">
                        <button className="btn btn-outline-dark w-50">Add</button>
                    </div>
                </div>
            </form>
        </main>
    )
}
