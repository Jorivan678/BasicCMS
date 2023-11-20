import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { AdminHome, Error404, LandingHome, Login, Register } from './modules';
import { AllRoles, JWTClaims } from './generalUtils';
import { AuthVerify, ProtectedRoute } from './components';
import { useDispatch, useSelector } from 'react-redux';
import { RootState, setClaims, setLoginState, setUser } from './redux';
import { ToastContainer } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";

export default function Routing(): React.JSX.Element
{
    const isLoggedIn = useSelector<RootState, boolean>((state) => state.loginState.isLoggedIn);
    const claims = useSelector<RootState, JWTClaims>((state) => state.claims);
    const dispatch = useDispatch();

    return (
        <>
            <Routes>
                {/* Landing area */}
                <Route path="/" Component={LandingHome} />
                {/* Admin area */}
                <Route path="/admin/home" element={<ProtectedRoute
                    redirectPath="/" useSpecificRoles={true} user={claims} mustHaveAllRoles={false}
                    specificRoles={[AllRoles['Admin'], AllRoles['Editor']]}>
                    <AdminHome />
                </ProtectedRoute>} />
                {/* Auth Area */}
                <Route path="/login" element={<Login
                    setClaims={(payload) => dispatch(setClaims(payload))}
                    setLogInState={(payload) => dispatch(setLoginState(payload))}
                    setUser={(payload) => dispatch(setUser(payload))}
                    isLoggedIn={isLoggedIn} />} />
                <Route path="/register" element={<Register isLoggedIn={isLoggedIn} />} />
                {/* Manage error routes */}
                <Route path="*" Component={Error404} />
            </Routes>
            <AuthVerify />
            <ToastContainer />
        </>);
}