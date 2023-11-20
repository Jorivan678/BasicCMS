import Cookies from "js-cookie";
import { ReadOnlyDictionary } from "./dictionary";

export const authTokenName: string = 'auth_token';

export const adminCommonStyles: ReadOnlyDictionary<string, string> = [
    { key: 'nunito', value: '/assets/admin/css/google/Nunito.css' },
    { key: 'bootstrap', value: '/assets/admin/css/bootstrap.css' },
    { key: 'bootstrap-icons', value: '/assets/admin/vendors/bootstrap-icons/bootstrap-icons.css' },
    { key: 'app', value: '/assets/admin/css/app.css' }
];

/**
 * Clears all styles added dynamically to the DOM by the component.
 */
export function ClearStyles() {
    const existingStyles = document.querySelectorAll('link[rel="stylesheet"]');
    existingStyles.forEach(style => {
        style.parentNode?.removeChild(style);
    });
}

/**
 * Clears all scripts added dynamically to the DOM by the component.
 */
export function ClearScripts() {
    const scriptElements = document.querySelectorAll('script[src^="/assets"]');
    scriptElements.forEach((element) => {
        element.parentNode?.removeChild(element);
    });
}

/**
 * Obtains the authentication token of this application.
 * @returns
 */
export function GetAuthToken(): string | null {
    return Cookies.get(authTokenName) ?? null;
}

/**
 * Sets the authentication token for this application.
 * @param token The authentication token.
 */
export function SetAuthToken(token: string) {
    var claims = JSON.parse(decodeToken(token.split('.')[1])) as JWTClaims;
    Cookies.set(authTokenName, token, { sameSite: 'Strict', secure: true, expires: new Date(claims.exp * 1000) });
}

export interface JWTClaims
{
    nameid: number;
    role: string[];
    exp: number;
    iat: number;
}

/** Encoding UTF-8 ⇢ base64 */
export function b64EncodeUnicode(str: string) {
    return btoa(encodeURIComponent(str).replace(/%([0-9A-F]{2})/g, function (match, p1) {
        return String.fromCharCode(parseInt(p1, 16))
    }))
}

/** Decoding base64 ⇢ UTF-8 */
export function b64DecodeUnicode(str: string) {
    return decodeURIComponent(Array.prototype.map.call(atob(str), function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
    }).join(''))
}

function decodeToken(str: string) {
    let output = str.replace('-', '+').replace('_', '/');
    switch (output.length % 4) {
        case 0:
            break;
        case 2:
            output += '==';
            break;
        case 3:
            output += '=';
            break;
        default:
            throw new Error('Invalid token');
    }
    return b64DecodeUnicode(output);
}

export function GetClaims(): JWTClaims | null {
    let token = GetAuthToken();
    if (!token) return null;

    var result = JSON.parse(decodeToken(token.split('.')[1]));

    return { nameid: result.nameid, role: result.role, exp: result.exp, iat: result.iat };
}


export const AllRoles = {
    'Admin': 'admin',
    'Editor': 'editor',
    'User': 'user'
};