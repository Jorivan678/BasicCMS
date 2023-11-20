import { AxiosError } from "axios";
import { ResponseErrorObject, ResponseObject } from "../models";

export function HandleAxiosWithBadRequestError(error: any): ResponseErrorObject | ResponseObject {
    if (!(error instanceof AxiosError))
        throw error;

    if (error.response?.status === 400)
        return error.response.data as ResponseErrorObject;

    return error.response?.data as ResponseObject;
}

export function HandleAxiosGenericError(error: any): ResponseObject {
    if (!(error instanceof AxiosError))
        throw error;

    return error.response?.data as ResponseObject;
}