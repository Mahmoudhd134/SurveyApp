import { useFormik } from "formik"
import { AddSurveyModel } from "../../Models/Survey/AddSurveyModel"
import React, { useEffect, useState } from "react"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faX } from "@fortawesome/free-solid-svg-icons"
import { useAddSurveyMutation } from "../../Feutures/Survey/SurveyApi"
import useGetAppError from "../../Hookes/useGetAppError"
import { useLocation, useNavigate } from "react-router-dom"

const AddSurvey = () => {
    const [addSurvey, addSurveyResult] = useAddSurveyMutation()
    const navigator = useNavigate()
    const loc = useLocation()

    const formik = useFormik<AddSurveyModel>({
        initialValues: {
            survey: '',
            options: [],
            categories: []
        },
        onSubmit: values => {
            addSurvey(values)
        }
    })

    const [option, setOption] = useState('')
    const [category, setCategory] = useState('')

    useEffect(() => {
        if (addSurveyResult.isSuccess)
            navigator(loc.state?.from ?? '/category/page/0')
    }, [addSurveyResult.isSuccess])

    const addOption = (e: React.MouseEvent) => {
        e.preventDefault()
        if (formik.values.options.some(o => o.trim() == option) == false && option.trim().length != 0)
            formik.setValues(p => ({ ...p, options: [...p.options, option.trim()] }))
        setOption('')
    }

    const removeOption = (optionToRemove: string) => {
        return (e: React.MouseEvent) => {
            e.preventDefault()
            formik.setValues(p => ({ ...p, options: p.options.filter(o => o != optionToRemove) }))
        }
    }

    const addCategory = (e: React.MouseEvent) => {
        e.preventDefault()
        if (formik.values.categories.some(c => c.trim() == category) == false && category.trim().length != 0)
            formik.setValues(p => ({ ...p, categories: [...p.categories, category.trim()] }))
        setCategory('')
    }

    const removeCategory = (categoryToRemove: string) => {
        return (e: React.MouseEvent) => {
            e.preventDefault()
            formik.setValues(p => ({ ...p, categories: p.categories.filter(c => c != categoryToRemove) }))
        }
    }

    return (
        <form className="container" onSubmit={formik.handleSubmit}>
            <div className="row justify-content-center">
                {addSurveyResult.isError && <div className="col-12 col-md-7 text-danger h4">
                    {useGetAppError(addSurveyResult.error)?.message}
                </div>}

                <div className="col-12 col-md-7">
                    <label htmlFor="question" className="col-form-label d-block">Question</label>
                    <input type="text" id="question" className="form-control"
                        name='survey'
                        onChange={formik.handleChange} />
                </div>

                <div className="col-12 col-md-7 my-3">
                    <div className="input-group mb-3">
                        <input type="text" className="form-control" placeholder="Add Option"
                            value={option} onChange={e => setOption(e.currentTarget.value)} />
                        <button className="btn btn-outline-secondary" onClick={addOption} type="button">Add</button>
                    </div>
                    <ul className="list-group">
                        {formik.values.options.map(o => <li key={o} className="list-group-item d-flex justify-content-between align-items-center">
                            {o}
                            <FontAwesomeIcon icon={faX} onClick={removeOption(o)} style={{ cursor: 'pointer' }} />
                        </li>)}
                    </ul>
                </div>

                <div className="col-12 col-md-7 my-3">
                    <div className="input-group mb-3">
                        <input type="text" className="form-control" placeholder="Add Category"
                            value={category} onChange={e => setCategory(e.currentTarget.value)} />
                        <button className="btn btn-outline-secondary" onClick={addCategory} type="button">Add</button>
                    </div>
                    <ul className="list-group">
                        {formik.values.categories.map(c => <li key={c} className="list-group-item d-flex justify-content-between align-items-center">
                            {c}
                            <FontAwesomeIcon icon={faX} onClick={removeCategory(c)} style={{ cursor: 'pointer' }} />
                        </li>)}
                    </ul>
                </div>

                <div className="col-12 col-md-7 my-3 d-flex justify-content-center">
                    <button className="btn btn-outline-dark">Done</button>
                </div>
            </div>
        </form>
    )
}

export default AddSurvey