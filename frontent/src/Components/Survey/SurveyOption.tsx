import { BaseQueryFn, FetchArgs, FetchBaseQueryError, MutationDefinition } from "@reduxjs/toolkit/dist/query"
import useAppSelector from "../../Hookes/useAppSelector"
import { SurveyOptionModel } from "../../Models/Survey/SurveyOptionModel"
import { MutationTrigger } from "@reduxjs/toolkit/dist/query/react/buildHooks"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faTrash, faX } from "@fortawesome/free-solid-svg-icons"

const SurveyOption = (props: {
    option: SurveyOptionModel,
    total: number,
    onClick: (e: React.MouseEvent) => void,
    canAnswer: boolean,
    onRemove: undefined | MutationTrigger<MutationDefinition<number, BaseQueryFn<string | FetchArgs, unknown, FetchBaseQueryError>, "user" | "category" | "survey", boolean, "api">>
    isOwner: boolean
}) => {
    const { option, total, onClick, canAnswer, onRemove, isOwner } = props
    const radio = option.count / total * 100

    const isAdmin = useAppSelector(s => s.auth.roles)?.some(r => r === 'Admin')

    return (
        <div
            className="p-3 border border-3 my-1 row gap-1 d-flex justify-content-between align-items-center fw-bolder"
            style={{ backgroundColor: 'whitesmoke', }}
            onMouseMove={e => e.preventDefault()}
        >
            <div className="col-10 d-flex justify-content-between position-relative"
                style={{ cursor: `${canAnswer ? 'pointer' : 'default'}` }}
                onClick={e => canAnswer && onClick(e)}
            >
                <b>{option.option}</b>
                {isAdmin &&
                    <span>{isNaN(radio) ? 0 : radio}%({option.count} {option.count > 1 ? 'votes' : 'vote'})</span>}

                {
                    option.isAnswered &&
                    <div className="position-absolute h-100 top-0 start-0"
                        style={{ width: `${isAdmin ? radio : 100}%`, background: ' rgb(144, 238, 144,.5)' }}
                    ></div>
                }
            </div>

            <div className="col-1">
                {isOwner && <FontAwesomeIcon className="text-danger col" style={{ cursor: 'pointer' }} icon={faTrash}
                    onClick={e => onRemove != undefined && onRemove(option.id)}
                />}
            </div>

        </div >
    )
}

export default SurveyOption