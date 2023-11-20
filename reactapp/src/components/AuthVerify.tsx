import React, { useEffect, useState } from "react";
import { useLocation } from "react-router";
import { JWTClaims, useLogOut } from "../generalUtils";
import { toast } from "react-toastify";
import { useDispatch, useSelector } from "react-redux";
import { removeUser } from "../redux/userSlice";
import { RootState } from "../redux";

/**
 * This component handles token expiration.
 */
export function AuthVerify(): React.JSX.Element | null {
    const location = useLocation();
    //Controls if 5 minutes expiration alert was launched.
    const [wasLaunched, setLaunched] = useState<boolean>(false);
    const dispatch = useDispatch();
    const claims = useSelector<RootState, JWTClaims>((state) => state.claims);
    const logOut = useLogOut();

    useEffect(() => {
        const timer = setInterval(() => {
            if (claims.exp != 0) {
                const timeToSendToast = (claims.exp) - (5 * 60);
                const dateNow = Math.floor(Date.now() / 1000);
                if (!wasLaunched && dateNow <= timeToSendToast) {
                    toast('Session will be expired in 5 minutes.', {
                        position: "top-right",
                        autoClose: 5000,
                        hideProgressBar: false,
                        closeOnClick: true,
                        pauseOnHover: true,
                        draggable: true,
                        progress: undefined,
                        theme: "dark",
                        type: 'info'
                    });
                    setLaunched(() => true);
                }

                if (claims.exp <= dateNow) {
                    dispatch(removeUser());
                    logOut();
                }
                console.log("It's executing :D");
            }
        }, 500);

        return () => clearInterval(timer);
    }, [location, wasLaunched, dispatch, claims, logOut]);

    return null;
}