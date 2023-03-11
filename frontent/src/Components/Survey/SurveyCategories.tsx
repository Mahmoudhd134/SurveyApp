import { useLocation, useNavigate } from "react-router-dom"
import { SurveyModel } from "../../Models/Survey/SurveyModel"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faPlus, faTrash, faX } from "@fortawesome/free-solid-svg-icons"
import { useRemoveSurveyCategoryMutation } from "../../Feutures/SurveyCategory/SurveyCategoryApi"
import React from "react"
import useGetAppError from "../../Hookes/useGetAppError"

const SurveyCategories = (props: { survey: SurveyModel }) => {
    const { survey } = props
    const navigator = useNavigate()
    const loc = useLocation()

    const [remvoeSurveyCategory, remvoeSurveyCategoryResult] = useRemoveSurveyCategoryMutation()

    const remove = (categoryId: number) => {
        return (e: React.MouseEvent) => remvoeSurveyCategory({ surveyId: survey.id, categoryId })
    }

    return (
        <div
            className='d-flex flex-column border border-3 rounded rounded-3 p-3 my-3 justify-content-center'
            style={{ backgroundColor: '#eee' }}
        >
            <h3 className='text-center'>Categories</h3>
            {remvoeSurveyCategoryResult.isError && <h4 className="text-center text-danger">
                {useGetAppError(remvoeSurveyCategoryResult.error)?.message}
            </h4>}
            <div className="row justify-content-center gap-1 fw-bold">
                {survey.categories.map(c => <div key={c.id} className="col-auto py-2 px-3 border border-3 rounded rounded-3 d-flex justify-content-between align-items-center gap-3"
                    style={{ backgroundColor: 'whitesmoke' }}
                >
                    <span style={{ cursor: 'pointer' }}
                        onClick={e => navigator('/survey/page/' + c.id + '/0')}
                    >{c.name}</span>
                    {survey.isTheMaker &&
                        <FontAwesomeIcon icon={faTrash} className="text-danger"
                            style={{ cursor: 'pointer' }}
                            onClick={remove(c.id)}
                        />}
                </div>)}

                {survey.isTheMaker &&
                    <div className="col-auto py-2 px-3 border border-3 rounded rounded-3 d-flex justify-content-between align-items-center gap-3"
                        onClick={e => navigator('/survey/addsurveycategory/' + survey.id, { state: { from: loc } })}
                        style={{ cursor: 'pointer' }}
                    >
                        <FontAwesomeIcon icon={faPlus} className="border border-3 rounded rounded-3 p-2"
                            style={{ backgroundColor: 'whitesmoke' }}
                        /> Add
                    </div>}

            </div>
        </div >
    )
}

export default SurveyCategories