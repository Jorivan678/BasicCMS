/* eslint-disable no-dupe-class-members */
import { AxiosInstance, AxiosResponse } from "axios";
import {
    CountResponse, NewIdResponse, ResponseErrorObject,
    ResponseObject, UserAddRequest, UserResponse, UserUpdRequest
} from "../models";
import { CreateRequest } from "./axios.config";
import { HandleAxiosGenericError, HandleAxiosWithBadRequestError } from "./serviceUtils";

/**
 * A contract for manage requests to API user endpoints.
 */
export interface IUserService
{
    /**
     * Gets paginated users.
     * If an api error ocurred, it returns a ResponseObject.
     * @param page The page number.
     * @param size The size of every page.
    */
    GetAsync(page: number, size: number): Promise<UserResponse[] | ResponseObject>;

    /**
     * Gets a specific user based on its id.
     * If an api error ocurred, it returns a ResponseObject.
     * @param id Category id.
     */
    GetAsync(id: number): Promise<UserResponse | ResponseObject>;

    /**
     * Gets all users that have the 'editor' role.
     * If an api error ocurred, it returns a ResponseObject.
     */
    GetAuthorsAsync(): Promise<UserResponse[] | ResponseObject>;

    /**
     * Gets a count of how many users are registered.
     * If an api error ocurred, it returns a ResponseObject.
     */
    GetCountAsync(): Promise<CountResponse | ResponseObject>;

    /**
     * Sends a request to register a new user to the database to the API.
     * Receives void if request was successful; a ResponseErrorObject if request data didn't satisfy validations;
     * a ResponseObject for other types of responses (not 200 or 400).
     * @param request Contains request information.
     */
    RegisterAsync(request: UserAddRequest): Promise<NewIdResponse | ResponseErrorObject | ResponseObject>;

    /**
     * Sends a request to edit an existing user to the API.
     * Receives void if request was successful; a ResponseErrorObject if request data didn't satisfy validations;
     * a ResponseObject for other types of responses (not 200 or 400).
     * @param request Contains request information.
     */
    EditAsync(request: UserUpdRequest): Promise<void | ResponseErrorObject | ResponseObject>;

    /**
     * Sends a request to delete an existing image to the API.
     * If an api error ocurred, it returns a ResponseObject.
     * @param id The category id.
     */
    DeleteAsync(id: number): Promise<void | ResponseObject>;
}

class UserService implements IUserService
{
    private axiosInst: AxiosInstance;

    constructor() {
        this.axiosInst = CreateRequest('users');
    }

    GetAsync(page: number, size: number): Promise<ResponseObject | UserResponse[]>;
    GetAsync(id: number): Promise<UserResponse | ResponseObject>;
    GetAsync(page: number, size?: number): Promise<ResponseObject | UserResponse[]> | Promise<UserResponse | ResponseObject> {
        if (size != undefined) {
            return this.GetPaginatedAsync(page, size);
        }
        return this.GetByIdAsync(page);
    }

    async GetAuthorsAsync(): Promise<ResponseObject | UserResponse[]> {
        try {
            return (await this.axiosInst.get<any, AxiosResponse<UserResponse[]>>('get-authors')).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }

    async GetCountAsync(): Promise<ResponseObject | CountResponse> {
        try {
            return (await this.axiosInst.get<any, AxiosResponse<CountResponse>>('get-count')).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }

    async RegisterAsync(request: UserAddRequest): Promise<ResponseObject | NewIdResponse | ResponseErrorObject> {
        try {
            return (await this.axiosInst.postForm<UserAddRequest, AxiosResponse<NewIdResponse>>('add-new', request)).data;
        } catch (error: any) {
            return HandleAxiosWithBadRequestError(error);
        }
    }

    async EditAsync(request: UserUpdRequest): Promise<void | ResponseObject | ResponseErrorObject> {
        try {
            await this.axiosInst.putForm<UserUpdRequest, void>('edit', request);
        } catch (error: any) {
            return HandleAxiosWithBadRequestError(error);
        }
    }

    async DeleteAsync(id: number): Promise<void | ResponseObject> {
        try {
            await this.axiosInst.delete(`delete/${id}`);
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }

    private async GetByIdAsync(id: number): Promise<UserResponse | ResponseObject> {
        try {
            let result = (await this.axiosInst.get<any, AxiosResponse<UserResponse>>(id.toString())).data;
            return result;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }

    private async GetPaginatedAsync(page: number, size: number): Promise<ResponseObject | UserResponse[]> {
        try {
            return (await this.axiosInst.get<any, AxiosResponse<UserResponse[]>>(`all?page=${page}&size=${size}`)).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }
}

export function CreateUserService(): IUserService { return new UserService(); }