import React from 'react';
import { Navigate } from "react-router";
import { AllRoles, GetClaims, ReadOnlyDictionary, adminCommonStyles } from "../../generalUtils";

const authStyles: ReadOnlyDictionary<string, string> = [
    { key: 'auth', value: '/assets/admin/css/pages/auth.css' }
];

export function SetAuthStyles() {
    const allStyles = [...adminCommonStyles, ...authStyles];
    allStyles.forEach(style => {
        const linkElement = document.createElement('link');
        linkElement.rel = 'stylesheet';
        linkElement.href = style.value;
        document.head.appendChild(linkElement);
    });
}

export interface AuthGeneralState
{
    errorMessages: { [key: string]: string[] };
}

/**
 * This function is for manage redirects from auth modules to the pages if there is a logged user.
 */
export function Redirect(): React.JSX.Element {
    let claims = GetClaims();
    if (claims && claims.role.find(x => x == AllRoles['Editor'] || x == AllRoles['Admin']))
        return <Navigate to="/admin/home" replace={true} />
    else return <Navigate to="/" replace={true} />
}