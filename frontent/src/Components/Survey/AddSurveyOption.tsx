import React, { useEffect, useState } from 'react'
import { useLocation, useNavigate, useParams } from 'react-router-dom'
import useGetAppError from '../../Hookes/useGetAppError'
import { useAddSurveyOptionMutation } from '../../Feutures/SurveyOption/SurveyOptionApi'
import { AddSurveyOptionModel } from '../../Models/SurveyOption/AddSurveyOptionModel'

const AddSurveyOption = () => {
    const { surveyId } = useParams()
    const [addOption, addOptionResult] = useAddSurveyOptionMutation()
    const [addOptionModel, setAddOptionModel] = useState<AddSurveyOptionModel>({ surveyId: Number(surveyId), option: '' })
    const navigator = useNavigate()
    const loc = useLocation()

    useEffect(() => {
        if (addOptionResult.isSuccess)
            navigator(loc.state?.from ?? 'survey/page/-1/0')
    }, [addOptionResult.isSuccess])

    const handelEdit = (e: React.FormEvent) => {
        e.preventDefault()
        addOption(addOptionModel)
    }


    return (
        <main className='container mt-5'>
            <form onSubmit={handelEdit}>
                <div className="w-100 row justify-content-center my-3">
                    <div className="col-12 col-sm-8 col-md-6 d-flex justify-content-center text-center text-danger">
                        <p>{addOptionResult.isError && useGetAppError(addOptionResult.error)?.message}</p>
                    </div>
                </div>

                <div className="w-100 row justify-content-center">
                    <div className="col-12 col-sm-8 col-md-6 p-3 border border-3 rounded rounded-3 shadow-lg">
                        <label htmlFor="question" className="form-label">Option</label>
                        <input type="text"
                            id="question"
                            className="form-control"
                            value={addOptionModel.option}
                            onChange={e => setAddOptionModel(p => ({ ...p, option: e.target.value }))} />
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

export default AddSurveyOption