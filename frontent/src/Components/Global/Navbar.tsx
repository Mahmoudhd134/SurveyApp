import React from "react";
import { Link } from "react-router-dom";
import useAppSelector from "../../Hookes/useAppSelector";
import useAppDispatch from "../../Hookes/useAppDispatch";
import { logout } from "../../Feutures/Auth/authSlice";

const Navbar = () => {
    const token = useAppSelector(s => s.auth.token)
    const dispatch = useAppDispatch()
    const submitHandler = (e: React.FormEvent) => {
        e.preventDefault()
    }
    const handleLogout = () => {
        dispatch(logout())
    }

    return (
        <nav className="navbar navbar-expand-lg bg-light">
            <div className="container-fluid">
                <Link to={'/'} className="navbar-brand">Navbar</Link>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse"
                    data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                        {token == null ?
                            (<>
                                <li className="nav-item">
                                    <Link to={'/auth/login'} className="nav-link">Login</Link>
                                </li>
                                <li className="nav-item">
                                    <Link to={'/auth/reg'} className="nav-link">Signup</Link>
                                </li>
                            </>) : (
                                <>
                                    <li className="nav-item">
                                        <Link to={'/profile'} className="nav-link">Profile</Link>
                                    </li>

                                    <li className="nav-item">
                                        <span onClick={handleLogout} className="nav-link" style={{cursor:"pointer"}}>
                                            Logout
                                        </span>
                                    </li>
                                </>)}
                        <li className="nav-item dropdown">
                            <Link to={'/'} className="nav-link dropdown-toggle" role="button" data-bs-toggle="dropdown"
                                aria-expanded="false">
                                Topics
                            </Link>
                            <ul className="dropdown-menu">
                                <li><Link to={'/category/page/0'} className="dropdown-item">Categories</Link></li>
                                <li><Link to={'/survey/page/-1/0'} className="dropdown-item">Surveys</Link></li>
                                <li>
                                    <hr className="dropdown-divider" />
                                </li>
                                <li><Link to={'/'} className="dropdown-item">Something else here</Link></li>
                            </ul>
                        </li>
                        <li className="nav-item">
                            <Link to={'/'} className="nav-link disabled">Disabled</Link>
                        </li>
                    </ul>
                    <form onSubmit={submitHandler} className="d-flex" role="search">
                        <input className="form-control me-2" type="search" placeholder="Search" aria-label="Search" />
                        <button className="btn btn-outline-success" type="submit">Search</button>
                    </form>
                </div>
            </div>
        </nav>
    )
}

export default Navbar