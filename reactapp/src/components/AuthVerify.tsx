import React, { useEffect, useState } from "react";
import { useLocation } from "react-router";
import { JWTClaims, useLogOut } from "../generalUtils";
import { toast } from "react-toastify";
import { useSelector } from "react-redux";
import { RootState } from "../redux";

/**
 * This component handles token expiration.
 */
export function AuthVerify(): React.JSX.Element | null {
    const location = useLocation();
    //Controls if 5 minutes expiration alert was launched.
    const [wasLaunched, setLaunched] = useState<boolean>(false);
    const claims = useSelector<RootState, JWTClaims>((state) => state.claims);
    const logOut = useLogOut();

    useEffect(() => {
        const timer = setInterval(() => {
            if (claims.exp != 0) {
                const timeToSendToast = claims.exp - (5 * 60);
                const dateNow = Math.floor(Date.now() / 1000);
                if (!wasLaunched && dateNow == timeToSendToast) {
                    toast<string>('Session will be expired in 5 minutes.', {
                        position: "top-right",
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

                if (claims.exp <= dateNow)
                    logOut();
            }
        }, 500);

        return () => clearInterval(timer);
    }, [location, wasLaunched, claims, logOut]);

    return null;
}