import { SurveyModel } from "../../Models/Survey/SurveyModel"

const SurveyAnswers = (props:{survey:SurveyModel}) => {
    const {survey} = props
  return (
            <div
                className='d-flex flex-column border border-3 rounded rounded-3 p-3 my-3 justify-content-center'
                style={{ backgroundColor: '#eee' }}
            >
                {survey.optionWithUsers.map(o => <div key={o.id} className="my-3">
                    <h3>{o.option} ({o.usernames.length})</h3>
                    <ul className='row'>
                        {o.usernames.map(u => <li key={u} className="col">
                            {u}
                        </li>)}
                    </ul>
                </div>)}
            </div>
  )
}

export default SurveyAnswers