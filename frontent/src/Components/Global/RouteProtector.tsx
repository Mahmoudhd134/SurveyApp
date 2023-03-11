import { Navigate, Outlet, useLocation } from "react-router-dom";
import useAppSelector from "../../Hookes/useAppSelector";

const RouteProtector = (props: { allwedRoles: string[] }) => {
    const { token, roles } = useAppSelector(s => s.auth)
    const loc = useLocation()

    return (
        <>
            {token ? (
                <>
                    {props.allwedRoles.length > 0 ?
                        roles?.some(r =>
                            props.allwedRoles.some(ar =>
                                ar.trim().toLowerCase() === r.trim().toLowerCase())) ? <Outlet /> : <h1>Not Allowed</h1>
                        :
                        <>
                            <Outlet />
                        </>
                    }
                </>) : (
                <>
                    <Navigate to={'/auth/login'} state={{ from: loc }} />
                </>)}
        </>
    );
};

export default RouteProtector;