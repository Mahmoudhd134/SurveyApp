import { useLocation, useNavigate, useParams } from "react-router-dom"
import { useDeleteCategoryMutation, useGetCategoryPageQuery } from "../../Feutures/Category/categoryApi"
import './categoryList.css'
import { useRef, useState } from "react"
import { faPen, faTrash } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import useAppSelector from "../../Hookes/useAppSelector"

export const CategoryList = () => {
    const PAGE_SIZE = 10
    const { pageIndex } = useParams()
    const [page, setPage] = useState(+(pageIndex ?? '0'))
    const [prefixSearch, setPrefixSearch] = useState('')
    const prefix = useRef() as React.MutableRefObject<HTMLInputElement>
    const result = useGetCategoryPageQuery({ pageIndex: page, pageSize: PAGE_SIZE, prefix: prefixSearch })
    const roles = useAppSelector(s => s.auth.roles)
    const isAdmin = roles?.some(r => r.toUpperCase() == 'ADMIN')
    const [remove, removeResult] = useDeleteCategoryMutation()
    const navigator = useNavigate()
    const loc = useLocation()

    return (
        <main className='container mt-5'>
            {isAdmin && <div className="row justify-content-center">
                <button className="btn btn-outline-dark w-25"
                    onClick={e => navigator('/category/add', { state: { from: loc } })}
                >Add New Category</button>
            </div>}

            <div className="input-group my-3 mt-5">
                <input type="text" ref={prefix} className="form-control" placeholder="Search Py Prefix" />
                <button className="btn btn-outline-dark" type="button"
                    onClick={e => {
                        setPrefixSearch(prefix.current.value)
                        setPage(0)
                    }}
                >Search</button>
            </div>

            <div className="row gap-4 p-4 justify-content-evenly border border-3 rounded rounded-3 my-3"
                style={{ backgroundColor: '#eee' }}>
                {result.isSuccess && result.data.map(c =>
                    <div key={c.id} className="col-12 col-sm-8 col-md-5 col-lg-3 shadow-lg rounded rounded-3 d-flex justify-content-center align-items-center p-3 categroy-list-card position-relative"
                        onClick={ e => navigator(`/survey/page/${c.id}/0`)}
                    >
                        <p className="m-0 text-center">{c.name}</p>

                        {isAdmin && <>
                            <span className="position-absolute top-0 end-0 me-1 mt-1">
                                <FontAwesomeIcon icon={faPen} style={{ cursor: 'pointer' }} onClick={e => navigator('/category/edit/' + c.id)} />
                            </span>

                            <span className="position-absolute top-0 start-0 me-1 ms-1">
                                <FontAwesomeIcon icon={faTrash} style={{ cursor: 'pointer', color: 'red' }} onClick={e => remove(c.id)} />
                            </span>
                        </>}
                    </div>)}
            </div>
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
