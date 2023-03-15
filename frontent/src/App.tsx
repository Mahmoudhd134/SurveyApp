import { Route, Routes, useLocation, useNavigate } from 'react-router-dom'
import Layout from './Components/Global/Layout'
import './app.css'
import Profile from "./Components/Profile/Profile";
import Login from "./Components/Auth/Login";
import Signup from "./Components/Auth/Signup";
import AuthLayout from "./Components/Auth/AuthLayout";
import RouteProtector from './Components/Global/RouteProtector';
import useRefreshToken from './Hookes/useRefreshToken';
import { logout, setCredentials } from './Feutures/Auth/authSlice';
import TokenModel from './Models/Auth/TokenModel';
import useAppDispatch from './Hookes/useAppDispatch';
import { useEffect } from 'react';
import { CategoryList } from './Components/Category/CategoryList';
import { AddCategoryForm } from './Components/Category/AddCategoryForm';
import EditCategoryForm from './Components/Category/EditCategoryForm';
import SurveyList from './Components/Survey/SurveyList';
import AddSurvey from './Components/Survey/AddSurvey';
import Survey from './Components/Survey/Survey';
import EditSurvey from './Components/Survey/EditSurvey';
import AddSurveyOption from './Components/Survey/AddSurveyOption';
import AddSurveyCategory from './Components/Survey/AddSurveyCategory';

const App = () => {
    const dispatch = useAppDispatch()
    const loc = useLocation()
    const navigator = useNavigate()

    useEffect(() => {
        const stayLogin = JSON.parse(localStorage.getItem('stayLogin') ?? 'false')
        if (stayLogin) {
            (async () => {
                const refresh = useRefreshToken()
                const data = await refresh()
                if (data) {
                    console.log('login done');
                    dispatch(setCredentials(data as TokenModel))
                    navigator(loc.pathname)
                } else {
                    console.log('login faild');
                    dispatch(logout())
                }
            })()
        }
    }, [])

    return (
        <Routes>
            <Route path='/' element={<Layout />}>
                <Route element={<RouteProtector allwedRoles={[]} />} >
                    <Route path={'profile'} element={<Profile />} />
                </Route>


                <Route path={'auth'} element={<AuthLayout />}>
                    <Route path={'login'} element={<Login />} />
                    <Route path={'reg'} element={<Signup />} />
                </Route>

                <Route path='category'>
                    <Route path={'page/:pageIndex'} element={<CategoryList />} />
                    <Route element={<RouteProtector allwedRoles={['Admin']} />}>
                        <Route path='add' element={<AddCategoryForm />} />
                        <Route path='edit/:id' element={<EditCategoryForm />} />
                    </Route>
                </Route>

                <Route path='survey' element={<RouteProtector allwedRoles={[]} />}>
                    <Route path={'page/:categoryId/:pageIndex'} element={<SurveyList />} />
                    <Route element={<RouteProtector allwedRoles={['Admin']} />}>
                        <Route path='add' element={<AddSurvey />} />
                        <Route path=':id' element={<Survey />} />
                        <Route path='edit/:id' element={<EditSurvey />} />
                        <Route path='addOption/:surveyId' element={<AddSurveyOption />} />
                        <Route path='addSurveyCategory/:surveyId' element={<AddSurveyCategory />} />
                    </Route>
                </Route>

                <Route path='*' element={<h1>No Page</h1>} />
            </Route>
        </Routes >

    )
}

export default App
