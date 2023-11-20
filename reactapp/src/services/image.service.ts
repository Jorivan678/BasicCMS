/* eslint-disable no-dupe-class-members */
import { AxiosInstance, AxiosResponse } from "axios";
import {
    CountResponse, ImageAddRequest, ImageUpdRequest,
    ImagenResponse, NewIdResponse, ResponseErrorObject, ResponseObject
} from "../models";
import { HandleAxiosGenericError, HandleAxiosWithBadRequestError } from "./serviceUtils";
import { CreateRequest } from "./axios.config";

/**
 * A contract for manage requests to API image endpoints.
 */
export interface IImageService
{
    /**
     * Gets paginated images.
     * If an api error ocurred, it returns a ResponseObject.
     * @param page The page number.
     * @param size The size of every page.
    */
    GetAsync(page: number, size: number): Promise<ImagenResponse[] | ResponseObject>;

    /**
     * Gets a specific image based on its id.
     * If an api error ocurred, it returns a ResponseObject.
     * @param id Category id.
     */
    GetAsync(id: number): Promise<ImagenResponse | ResponseObject>;

    /**
     * Gets a count of how many images are in the database.
     * If an api error ocurred, it returns a ResponseObject.
     */
    GetCountAsync(): Promise<CountResponse | ResponseObject>;

    /**
     * Sends a request add a new image to the database to the API.
     * Receives void if request was successful; a ResponseErrorObject if request data didn't satisfy validations;
     * a ResponseObject for other types of responses (not 200 or 400).
     * @param request Contains request information.
     */
    PostNewAsync(request: ImageAddRequest): Promise<NewIdResponse | ResponseErrorObject | ResponseObject>;

    /**
     * Sends a request to edit an existing image to the API.
     * Receives void if request was successful; a ResponseErrorObject if request data didn't satisfy validations;
     * a ResponseObject for other types of responses (not 200 or 400).
     * @param request Contains request information.
     */
    EditAsync(request: ImageUpdRequest): Promise<void | ResponseErrorObject | ResponseObject>;

    /**
     * Sends a request to delete an existing image to the API.
     * If an api error ocurred, it returns a ResponseObject.
     * @param id The category id.
     */
    DeleteAsync(id: number): Promise<void | ResponseObject>;
}

class ImageService implements IImageService
{
    private axiosInst: AxiosInstance;

    constructor() {
        this.axiosInst = CreateRequest('images');
    }

    GetAsync(page: number, size: number): Promise<ResponseObject | ImagenResponse[]>;
    GetAsync(id: number): Promise<ImagenResponse | ResponseObject>;
    GetAsync(page: number, size?: number): Promise<ResponseObject | ImagenResponse[]> | Promise<ImagenResponse | ResponseObject> {
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

    async PostNewAsync(request: ImageAddRequest): Promise<ResponseObject | NewIdResponse | ResponseErrorObject> {
        try {
            return (await this.axiosInst.postForm<ImageAddRequest, AxiosResponse<NewIdResponse>>('add-new', request)).data;
        } catch (error: any) {
            return HandleAxiosWithBadRequestError(error);
        }
    }

    async EditAsync(request: ImageUpdRequest): Promise<void | ResponseObject | ResponseErrorObject> {
        try {
            await this.axiosInst.putForm<ImageUpdRequest, void>('edit', request);
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

    private async GetByIdAsync(id: number): Promise<ImagenResponse | ResponseObject> {
        try {
            return (await this.axiosInst.get<any, AxiosResponse<ImagenResponse>>(id.toString())).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }

    private async GetPaginatedAsync(page: number, size: number): Promise<ResponseObject | ImagenResponse[]> {
        try {
            return (await this.axiosInst.get<any, AxiosResponse<ImagenResponse[]>>(`all?page=${page}&size=${size}`)).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }
}

export function CreateImageService(): IImageService { return new ImageService(); }