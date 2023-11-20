/* eslint-disable no-dupe-class-members */
import { AxiosInstance, AxiosResponse } from "axios";
import {
    ComentarioResponse, CommentAddRequest, CommentUpdRequest,
    CountResponse, NewIdResponse, ResponseErrorObject, ResponseObject
} from "../models";
import { CreateRequest } from "./axios.config";
import { HandleAxiosGenericError, HandleAxiosWithBadRequestError } from "./serviceUtils";

/**
 * A contract for manage requests to API comment endpoints.
 */
export interface ICommentService
{
    /**
     * Gets paginated comments.
     * If an api error ocurred, it returns a ResponseObject.
     * @param articleId The article that owns the comments.
     * @param page The page number.
     * @param size The size of every page.
    */
    GetAsync(articleId: number, page: number, size: number): Promise<ComentarioResponse[] | ResponseObject>;

    /**
     * Gets a specific comment based on its id.
     * If an api error ocurred, it returns a ResponseObject.
     * @param id The comment id.
     */
    GetAsync(id: number): Promise<ComentarioResponse | ResponseObject>;

    /**
     * Gets a count of how many comments of a specific article are in the database.
     * If an api error ocurred, it returns a ResponseObject.
     */
    GetCountAsync(articleId: number): Promise<CountResponse | ResponseObject>;

    /**
     * Sends a request add a new comment to the database to the API.
     * Receives void if request was successful; a ResponseErrorObject if request data didn't satisfy validations;
     * a ResponseObject for other types of responses (not 200 or 400).
     * @param request Contains request information.
     */
    PostNewAsync(request: CommentAddRequest): Promise<NewIdResponse | ResponseErrorObject | ResponseObject>;

    /**
     * Sends a request to edit an existing comment to the API.
     * Receives void if request was successful; a ResponseErrorObject if request data didn't satisfy validations;
     * a ResponseObject for other types of responses (not 200 or 400).
     * @param request Contains request information.
     */
    EditAsync(request: CommentUpdRequest): Promise<void | ResponseErrorObject | ResponseObject>;

    /**
     * Sends a request to delete an existing comment to the API.
     * If an api error ocurred, it returns a ResponseObject.
     * @param id The category id.
     */
    DeleteAsync(id: number): Promise<void | ResponseObject>;
}

class CommentService implements ICommentService
{
    private axiosInst: AxiosInstance;

    constructor() {
        this.axiosInst = CreateRequest('comments');
    }

    GetAsync(articleId: number, page: number, size: number): Promise<ResponseObject | ComentarioResponse[]>;
    GetAsync(id: number): Promise<ComentarioResponse | ResponseObject>;
    GetAsync(articleId: number, page?: number, size?: number): Promise<ResponseObject | ComentarioResponse[]> | Promise<ComentarioResponse | ResponseObject> {
        if (size && page) {
            return this.GetPaginatedAsync(articleId, page, size);
        }
        return this.GetByIdAsync(articleId);
    }

    async GetCountAsync(articleId: number): Promise<ResponseObject | CountResponse> {
        try {
            return (await this.axiosInst.get<any, AxiosResponse<CountResponse>>(`get-count/${articleId}`)).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }

    async PostNewAsync(request: CommentAddRequest): Promise<ResponseObject | NewIdResponse | ResponseErrorObject> {
        try {
            return (await this.axiosInst.postForm<CommentAddRequest, AxiosResponse<NewIdResponse>>('add-new', request)).data;
        } catch (error: any) {
            return HandleAxiosWithBadRequestError(error);
        }
    }

    async EditAsync(request: CommentUpdRequest): Promise<void | ResponseObject | ResponseErrorObject> {
        try {
            await this.axiosInst.putForm<CommentUpdRequest, void>('edit', request);
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

    private async GetByIdAsync(id: number): Promise<ComentarioResponse | ResponseObject> {
        try {
            return (await this.axiosInst.get<any, AxiosResponse<ComentarioResponse>>(id.toString())).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }

    private async GetPaginatedAsync(articleId: number, page: number, size: number): Promise<ResponseObject | ComentarioResponse[]> {
        try {
            return (await this.axiosInst.get<any, AxiosResponse<ComentarioResponse[]>>(`${articleId}?page=${page}&size=${size}`)).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }
}

export function CreateCommentService(): ICommentService { return new CommentService(); }