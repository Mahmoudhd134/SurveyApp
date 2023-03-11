import React, { useState } from "react"
import { Link, useLocation, useNavigate, useParams } from "react-router-dom"
import { useGetSurveysPageQuery } from "../../Feutures/Survey/SurveyApi"
import SurveyOption from "./SurveyOption"
import { useAddSurveyAnswerMutation, useRemoveSurveyAnswerMutation } from "../../Feutures/SurveyAnswer/SurveyAnswerApi"
import useAppSelector from "../../Hookes/useAppSelector"
import { useGetCategoryPyIdQuery } from "../../Feutures/Category/categoryApi"

const SurveyList = () => {
    const PAGE_SIZE = 10
    const { categoryId, pageIndex } = useParams()
    const [page, setPage] = useState(Number(pageIndex ?? '0'))
    const categoryResult = useGetCategoryPyIdQuery(Number(categoryId))
    const loc = useLocation()

    const result = useGetSurveysPageQuery({ categoryId: Number(categoryId), pageIndex: page, pageSize: PAGE_SIZE })
    const [removeAnswer, removeAnswerResult] = useRemoveSurveyAnswerMutation()
    const [addAsnwer, addAsnwerResult] = useAddSurveyAnswerMutation()

    const isAdmin = useAppSelector(s => s.auth.roles)?.some(r => r == 'Admin')

    const addSurveyAsnwer = (surveyId: number, optionId: number) => {
        return (e: React.MouseEvent) => {
            e.preventDefault()
            addAsnwer({ surveyId, optionId })
        }
    }

    if (result.isError)
        return <h3>{JSON.stringify(result.error, null, '\n')}</h3>

    if (result.isFetching)
        return <h3>Loading...</h3>
    return (
        <main className="container d-flex flex-column justify-content-center">
            {isAdmin && <Link to='/survey/add' state={{ from: loc }} className="btn btn-outline-dark">Add New Survey</Link>}
            {categoryResult.isSuccess && <h3 className='text-center my-3'>
                Category "{categoryResult.data.name}"
            </h3>}
            {result.data?.map(s => <div key={s.id}
                className='d-flex flex-column border border-3 rounded rounded-3 p-3 my-3 justify-content-center'
                style={{ backgroundColor: '#eee' }}
            >
                <div className="border-bottom mb-2 d-flex justify-content-between">
                    <h3>{s.question}</h3>
                    {s.isAnswered && <span style={{ cursor: 'pointer' }}
                        onClick={e => removeAnswer(s.answerId)}
                    >revote</span>}
                </div>
                {s.optionDtos.map(o =>
                    <SurveyOption key={o.id}
                        option={o}
                        total={s.totalAnswers}
                        onClick={addSurveyAsnwer(s.id, o.id)}
                        canAnswer={s.isAnswered == false}
                        isOwner={false}
                        onRemove={undefined} />)}
                {isAdmin && <Link to={'/survey/' + s.id} className="text-center mt-1 align-self-center"
                    style={{ width: 'fit-content', color: 'gray' }}
                ><b>More</b></Link>}
            </div>)}

            <nav className="row justify-content-center align-items-baseline"
                onMouseMove={e => e.preventDefault()}
            >
                <ul className="pagination col-10 col-sm-8 col-md-6 justify-content-center">
                    <li className={`page-item ${page == 0 && 'disabled'}`}>
                        <button className={'page-link'} onClick={e => setPage(p => p - 1)}>dec</button>
                    </li>
                    <li className="page-item"><button className="page-link">{page}</button></li>
                    <li className="page-item active" aria-current="page">
                        <button className="page-link">{page + 1}</button>
                    </li>
                    <li className="page-item"><button className="page-link">{page + 2}</button></li>
                    <li className={`page-item ${((result.data?.length ?? 0) == 0 || (result.data?.length ?? 0) < PAGE_SIZE) && 'disabled'}`}>
                        <button className={'page-link'} onClick={e => setPage(p => p + 1)}>inc</button>
                    </li>
                </ul>
            </nav>
        </main>
    )
}

export default SurveyList