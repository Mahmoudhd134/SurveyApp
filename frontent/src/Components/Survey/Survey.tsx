import { Link, useLocation, useNavigate, useParams } from "react-router-dom"
import { useGetSurveyQuery, useRemoveSurveyMutation } from "../../Feutures/Survey/SurveyApi"
import { useRemoveSurveyOptionMutation } from "../../Feutures/SurveyOption/SurveyOptionApi"
import SurveyOption from "./SurveyOption"
import { useAddSurveyAnswerMutation, useRemoveSurveyAnswerMutation } from "../../Feutures/SurveyAnswer/SurveyAnswerApi"
import { useEffect, useState } from "react"
import useGetAppError from "../../Hookes/useGetAppError"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faPlus } from "@fortawesome/free-solid-svg-icons"
import SurveyAnswers from "./SurveyAnswers"
import SurveyCategories from "./SurveyCategories"

const Survey = () => {
    const { id } = useParams()

    const [removeAnswer, removeAnswerResult] = useRemoveSurveyAnswerMutation()
    const [addAsnwer, addAsnwerResult] = useAddSurveyAnswerMutation()
    const [removeSurvey, removeSurveyResult] = useRemoveSurveyMutation()
    const [removeSurveyOption, removeSurveyOptionResult] = useRemoveSurveyOptionMutation()
    const result = useGetSurveyQuery(Number(id))
    const loc = useLocation()
    const navigator = useNavigate()
    const [errors, setErrors] = useState('')

    useEffect(() => {
        setErrors([useGetAppError(addAsnwerResult.error),
        useGetAppError(removeAnswerResult.error),
        useGetAppError(removeSurveyResult.error)]
            .map(e => '\n' + (e != undefined ? e.message : ''))
            .reduce((a, n) => a + '\n' + n, '')
            .trim())
    }, [removeAnswerResult.isError, addAsnwerResult.isError, removeSurveyResult.isError])

    useEffect(() => {
        if (removeSurveyResult.isSuccess)
            navigator(loc.state?.from ?? '/survey/page/-1/0')
    }, [removeSurveyResult.isSuccess])

    const addSurveyAsnwer = (surveyId: number, optionId: number) => {
        return (e: React.MouseEvent) => {
            e.preventDefault()
            addAsnwer({ surveyId, optionId })
        }
    }

    if (result.isFetching)
        return <h1>Loading...</h1>

    if (result.isError)
        return <h1>{JSON.stringify(result.error, null, '\n')}</h1>

    return (
        <div className='container'>
            {errors.length > 0 && <h3 className="text-center text-danger">{errors}</h3>}
            <div
                className='d-flex flex-column border border-3 rounded rounded-3 p-3 my-3 justify-content-center'
                style={{ backgroundColor: '#eee' }}
            >
                <div className="border-bottom mb-2 d-flex justify-content-between">
                    <h3>{result.data?.question}</h3>
                    <div className="d-flex gap-2">
                        {result.data?.isAnswered && <span style={{ cursor: 'pointer' }}
                            onClick={e => removeAnswer(result.data.answerId)}
                        >revote</span>}

                        {result.data?.isTheMaker && <span style={{ cursor: 'pointer' }}
                            className='text-danger'
                            onClick={e => removeSurvey(result.data.id)}
                        >remove</span>}

                        {result.data?.isTheMaker &&
                            <Link to={'/survey/edit/' + result.data.id} state={{ from: loc }}
                                className='text-decoration-none text-black'>
                                edit
                            </Link>}
                    </div>
                </div>
                {result.data?.optionWithUsers.map(o =>
                    <SurveyOption key={o.id}
                        option={o}
                        total={result.data?.totalAnswers}
                        onClick={addSurveyAsnwer(result.data?.id, o.id)}
                        onRemove={removeSurveyOption}
                        isOwner={result.data?.isTheMaker}
                        canAnswer={result.data?.isAnswered == false} />)}
                {result.data?.isTheMaker &&
                    <div
                        className="p-3 border border-3 my-1 position-relative d-flex align-items-center fw-bolder gap-3"
                        style={{ backgroundColor: 'whitesmoke', cursor: 'pointer' }}
                        onClick={e => navigator('/survey/addOption/' + result.data?.id, { state: { from: loc } })}
                    >
                        <FontAwesomeIcon className="border border-3 p-1" icon={faPlus} /> Add
                    </div>}

            </div>

            {result.isSuccess && <SurveyCategories survey={result.data}/>}

            {result.isSuccess && <SurveyAnswers survey={result.data}/>}

        </div>
    )
}

export default Survey