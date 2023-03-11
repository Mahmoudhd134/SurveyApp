import Navbar from "./Navbar"
import { Outlet } from 'react-router-dom'
import RouteProtector from "./RouteProtector";

const Layout = () => {
    return (
        <>
            <Navbar />
            <Outlet />
        </>
    )
}

export default Layout