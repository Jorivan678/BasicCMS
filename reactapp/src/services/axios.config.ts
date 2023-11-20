import axios, { AxiosInstance } from "axios";
import { GetAuthToken } from "../generalUtils";

function ManageAuthResponseErrors(error: any) {
    return Promise.reject(error);
}

function ManageArticleResponseErrors(error: any) {
    let statusCode = error.response.status as number;
    console.log(statusCode);
    return Promise.reject(error);
}

function ManageCategoryResponseErrors(error: any) {
    let statusCode = error.response.status as number;
    console.log(statusCode);
    return Promise.reject(error);
}

function ManageCommentResponseErrors(error: any) {
    let statusCode = error.response.status as number;
    console.log(statusCode);
    return Promise.reject(error);
}

function ManageRoleResponseErrors(error: any) {
    let statusCode = error.response.status as number;
    console.log(statusCode);
    return Promise.reject(error);
}

function ManageUserResponseErrors(error: any) {
    let statusCode = error.response.status as number;
    console.log(statusCode);
    return Promise.reject(error);
}

/**
 * Creates an axios instance.
 * @param controller is the api controller name, e.g. 'api/auth' where 'auth' is the controller name.
 * @returns {AxiosInstance}
 */
export function CreateRequest(controller: string): AxiosInstance
{
    const axiosInstance = axios.create({
        baseURL: `/api/${controller}/`
    });

    let errorCallback: (err: any) => Promise<never>;

    switch (controller) {
        case 'auth':
            errorCallback = ManageAuthResponseErrors;
            break;
        case 'articles':
            errorCallback = ManageArticleResponseErrors;
            break;
        case 'categories':
            errorCallback = ManageCategoryResponseErrors;
            break;
        case 'comments':
            errorCallback = ManageCommentResponseErrors;
            break;
        case 'roles':
            errorCallback = ManageRoleResponseErrors;
            break;
        case 'users':
            errorCallback = ManageUserResponseErrors;
            break;
        default:
            throw new Error("Invalid controller.");
    }

    axiosInstance.interceptors.request.use(
        config => {
            const token = GetAuthToken();
            if (token) {
                config.headers.Authorization = `Bearer ${token}`;
            }

            return config;
        }
    );

    axiosInstance.interceptors.response.use(
        config => {
            return config;
        },
        errorCallback
    );

    return axiosInstance;
}