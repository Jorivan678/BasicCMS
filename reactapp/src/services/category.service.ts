/* eslint-disable no-dupe-class-members */
import { AxiosInstance, AxiosResponse } from "axios";
import { CreateRequest } from "./axios.config";
import {
    CategoriaResponse, CategoryAddRequest, CategoryUpdRequest,
    CountResponse, NewIdResponse, ResponseErrorObject, ResponseObject
} from "../models";
import { HandleAxiosGenericError, HandleAxiosWithBadRequestError } from "./serviceUtils";

/**
 * A contract for manage requests to API category endpoints.
 */
export interface ICategoryService
{
    /**
     * Gets paginated categories.
     * If an api error ocurred, it returns a ResponseObject.
     * @param page The page number.
     * @param size The size of every page.
    */
    GetAsync(page: number, size: number): Promise<CategoriaResponse[] | ResponseObject>;

    /**
     * Gets a specific category based on its id.
     * If an api error ocurred, it returns a ResponseObject.
     * @param id Category id.
     */
    GetAsync(id: number): Promise<CategoriaResponse | ResponseObject>;

    /**
     * Gets a count of how many categories are in the database.
     * If an api error ocurred, it returns a ResponseObject.
     */
    GetCountAsync(): Promise<CountResponse | ResponseObject>;

    /**
     * Sends a request add a new category to the database to the API.
     * Receives void if request was successful; a ResponseErrorObject if request data didn't satisfy validations;
     * a ResponseObject for other types of responses (not 200 or 400).
     * @param request Contains request information.
     */
    PostNewAsync(request: CategoryAddRequest): Promise<NewIdResponse | ResponseErrorObject | ResponseObject>;

    /**
     * Sends a request to edit an existing category to the API.
     * Receives void if request was successful; a ResponseErrorObject if request data didn't satisfy validations;
     * a ResponseObject for other types of responses (not 200 or 400).
     * @param request Contains request information.
     */
    EditAsync(request: CategoryUpdRequest): Promise<void | ResponseErrorObject | ResponseObject>;

    /**
     * Sends a request to delete an existing category to the API.
     * If an api error ocurred, it returns a ResponseObject.
     * @param id The category id.
     */
    DeleteAsync(id: number): Promise<void | ResponseObject>;
}

class CategoryService implements ICategoryService
{
    private axiosInst: AxiosInstance;

    constructor() {
        this.axiosInst = CreateRequest('categories');
    }

    GetAsync(page: number, size: number): Promise<ResponseObject | CategoriaResponse[]>;
    GetAsync(id: number): Promise<CategoriaResponse | ResponseObject>;
    GetAsync(page: number, size?: number): Promise<ResponseObject | CategoriaResponse[]> | Promise<CategoriaResponse | ResponseObject> {
        if (size) {
            return this.GetPaginatedAsync(page, size);
        }
        return this.GetByIdAsync(page);
    }

    async GetCountAsync(): Promise<ResponseObject | CountResponse> {
        try {
            return (await this.axiosInst.get<any, AxiosResponse<CountResponse>>('get-count')).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }

    async PostNewAsync(request: CategoryAddRequest): Promise<ResponseObject | NewIdResponse | ResponseErrorObject> {
        try {
            return (await this.axiosInst.postForm<CategoryAddRequest, AxiosResponse<NewIdResponse>>('add-new', request)).data;
        } catch (error: any) {
            return HandleAxiosWithBadRequestError(error);
        }
    }

    async EditAsync(request: CategoryUpdRequest): Promise<void | ResponseObject | ResponseErrorObject> {
        try {
            await this.axiosInst.putForm<CategoryUpdRequest, void>('edit', request);
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

    private async GetByIdAsync(id: number): Promise<CategoriaResponse | ResponseObject> {
        try {
            return (await this.axiosInst.get<any, AxiosResponse<CategoriaResponse>>(id.toString())).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }

    private async GetPaginatedAsync(page: number, size: number): Promise<ResponseObject | CategoriaResponse[]> {
        try {
            return (await this.axiosInst.get<any, AxiosResponse<CategoriaResponse[]>>(`all?page=${page}&size=${size}`)).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }
}

export function CreateCategoryService(): ICategoryService { return new CategoryService(); }