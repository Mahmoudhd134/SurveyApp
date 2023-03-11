import React, { useEffect, useState } from 'react'
import { useLocation, useNavigate, useParams } from 'react-router-dom'
import useGetAppError from '../../Hookes/useGetAppError'
import { useAddSurveyCategoryByNameMutation } from '../../Feutures/SurveyCategory/SurveyCategoryApi'

const AddSurveyCategory = () => {
    const { surveyId } = useParams()
    const [addSurveyCategory, addSurveyCategoryResult] = useAddSurveyCategoryByNameMutation()
    const [category, setCategory] = useState('')
    const navigator = useNavigate()
    const loc = useLocation()

    useEffect(() => {
        if (addSurveyCategoryResult.isSuccess)
            navigator(loc.state?.from ?? 'survey/page/-1/0')
    }, [addSurveyCategoryResult.isSuccess])

    const add = (e: React.FormEvent) => {
        e.preventDefault()
        addSurveyCategory({ surveyId: Number(surveyId), categoryName: category })
    }


    return (
        <main className='container mt-5'>
            <form onSubmit={add}>
                <div className="w-100 row justify-content-center my-3">
                    <div className="col-12 col-sm-8 col-md-6 d-flex justify-content-center text-center text-danger">
                        <p>{addSurveyCategoryResult.isError && useGetAppError(addSurveyCategoryResult.error)?.message}</p>
                    </div>
                </div>

                <div className="w-100 row justify-content-center">
                    <div className="col-12 col-sm-8 col-md-6 p-3 border border-3 rounded rounded-3 shadow-lg">
                        <label htmlFor="category" className="form-label">Category</label>
                        <input type="text"
                            id="category"
                            className="form-control"
                            value={category}
                            onChange={e => setCategory(e.currentTarget.value)} />
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

export default AddSurveyCategory