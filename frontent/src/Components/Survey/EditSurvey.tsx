import { useLocation, useNavigate, useParams } from "react-router-dom"
import { useEditSurveyMutation, useGetSurveyQuestionQuery } from "../../Feutures/Survey/SurveyApi"
import React, { useEffect, useState } from "react"
import { EditSurveyModel } from "../../Models/Survey/EditSurveyModel"
import useGetAppError from "../../Hookes/useGetAppError"

const EditSurvey = () => {
    const { id } = useParams()
    const result = useGetSurveyQuestionQuery(Number(id))
    const [editSurvey, setEditSurvey] = useState<EditSurveyModel>({ id: Number(id), question: '' })
    const [edit, editResult] = useEditSurveyMutation()
    const navigator = useNavigate()
    const loc = useLocation()

    useEffect(() => {
        if (result.isSuccess)
            setEditSurvey({ id: result.data.id, question: result.data.question })
    }, [result.isSuccess])

    useEffect(() => {
        if (editResult.isSuccess)
            navigator(loc.state.from ?? '/category/page/0')
    }, [editResult.isSuccess])

    const handelEdit = (e: React.FormEvent) => {
        e.preventDefault()
        edit(editSurvey)
    }

    if (result.isFetching)
        return <h1>Loading...</h1>

    return (
        <main className='container mt-5'>
            <form onSubmit={handelEdit}>
                <div className="w-100 row justify-content-center my-3">
                    <div className="col-12 col-sm-8 col-md-6 d-flex justify-content-center text-center text-danger">
                        <p>{result.isError && useGetAppError(result.error)?.message}</p>
                        <p>{editResult.isError && useGetAppError(editResult.error)?.message}</p>
                    </div>
                </div>

                <div className="w-100 row justify-content-center">
                    <div className="col-12 col-sm-8 col-md-6 p-3 border border-3 rounded rounded-3 shadow-lg">
                        <label htmlFor="question" className="form-label">Question</label>
                        <input type="text"
                            id="question"
                            className="form-control"
                            value={editSurvey.question}
                            onChange={e => setEditSurvey(p => ({ ...p, question: e.target.value }))} />
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

export default EditSurvey