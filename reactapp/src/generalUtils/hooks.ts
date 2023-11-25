import { useCallback } from "react";
import { useNavigate} from "react-router";
import { authTokenName } from "./utils";
import Cookies from "js-cookie";
import { useDispatch } from "react-redux";
import { removeClaims, removeUser, setLoginState} from "../redux";

/**
 * Deletes the authentication token.
 */
function GetRidOfToken() {
    Cookies.remove(authTokenName, { sameSite: 'Strict', secure: true });
}

/**
 * Hook to handle user log out.
 */
export function useLogOut() {
    const navigate = useNavigate();
    const dispatch = useDispatch();

    return useCallback(() => {
        GetRidOfToken();
        dispatch(removeClaims());
        dispatch(removeUser());
        dispatch(setLoginState(false));

        navigate('/', { replace: true });
    }, [navigate, dispatch]);
}