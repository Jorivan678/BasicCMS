/* eslint-disable no-dupe-class-members */
import { AxiosInstance, AxiosResponse } from "axios";
import { CreateRequest } from "./axios.config";
import {
    ArticleAddRequest, ArticleUpdRequest, ArticuloResponse,
    CountResponse, NewIdResponse, ResponseErrorObject, ResponseObject
} from "../models";
import { HandleAxiosGenericError, HandleAxiosWithBadRequestError } from "./serviceUtils";

/**
 * A contract for manage requests to API article endpoints.
 */
export interface IArticleService
{
    /**
     * Gets paginated articles based on the parameters.
     * If an api error ocurred, it returns a ResponseObject.
     * @param page The page number.
     * @param size The size of every page.
     * @param categories Categories that must match with articles (optional).
     * @param authorId Articles author that must match with articles (optional).
     */
    GetAsync(page: number, size: number, categories: string[], authorId: number): Promise<ArticuloResponse[] | ResponseObject>;

    /**
     * Gets a specific article based on its id.
     * If an api error ocurred, it returns a ResponseObject.
     * @param id Article id.
     */
    GetAsync(id: number): Promise<ArticuloResponse | ResponseObject>;
    
    /**
     * Gets a count of how many articles match categories and/or an author.
     * If an api error ocurred, it returns a ResponseObject.
     * @param categories Categories that must match with articles (optional).
     * @param authorId Articles author that must match with articles (optional).
     */
    GetCountAsync(categories: string[], authorId: number): Promise<CountResponse | ResponseObject>;

    /**
     * Sends a request add a new article to the database to the API.
     * Receives void if request was successful; a ResponseErrorObject if request data didn't satisfy validations;
     * a ResponseObject for other types of responses (not 200 or 400).
     * @param request Contains request information.
     */
    PostNewAsync(request: ArticleAddRequest): Promise<NewIdResponse | ResponseErrorObject | ResponseObject>;

    /**
     * Sends a request to edit an existing article to the API.
     * Receives void if request was successful; a ResponseErrorObject if request data didn't satisfy validations;
     * a ResponseObject for other types of responses (not 200 or 400).
     * @param request Contains request information.
     */
    EditAsync(request: ArticleUpdRequest): Promise<void | ResponseErrorObject | ResponseObject>;

    /**
     * Sends a request to delete an existing article to the API.
     * If an api error ocurred, it returns a ResponseObject.
     * @param id The article id.
     */
    DeleteAsync(id: number): Promise<void | ResponseObject>;
}

class ArticleService implements IArticleService
{
    private axiosInst: AxiosInstance;

    constructor() {
        this.axiosInst = CreateRequest('articles');
    }

    GetAsync(page: number, size: number, categories: string[], authorId: number): Promise<ResponseObject | ArticuloResponse[]>;
    GetAsync(id: number): Promise<ArticuloResponse | ResponseObject>;
    GetAsync(page: number, size?: number, categories?: string[], authorId?: number): Promise<ResponseObject | ArticuloResponse[]> | Promise<ArticuloResponse | ResponseObject> {
        if (size && categories && authorId != undefined) {
            return this.GetPaginatedAsync(page, size, categories, authorId);
        }
        return this.GetByIdAsync(page);
    }

    async GetCountAsync(categories: string[], authorId: number): Promise<ResponseObject | CountResponse> {
        try {
            let url: string = 'get-count';

            if (categories.length > 0) {
                const categoriesStr: string = categories.join('+');
                url += `?categories=${categoriesStr}`;
            }

            if (authorId > 0)
                url += url.includes('categories') ? `&authorId=${authorId}` : `?authorId=${authorId}`;

            return (await this.axiosInst.get<any, AxiosResponse<CountResponse>>(url)).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }

    async PostNewAsync(request: ArticleAddRequest): Promise<ResponseObject | NewIdResponse | ResponseErrorObject> {
        try {
            return (await this.axiosInst.postForm<ArticleAddRequest, AxiosResponse<NewIdResponse>>('add-new', request)).data;
        } catch (error: any) {
            return HandleAxiosWithBadRequestError(error);
        }
    }

    async EditAsync(request: ArticleUpdRequest): Promise<void | ResponseObject | ResponseErrorObject> {
        try {
            await this.axiosInst.putForm<ArticleUpdRequest, void>('edit', request);
        } catch (e: any) {
            return HandleAxiosWithBadRequestError(e);
        }
    }

    async DeleteAsync(id: number): Promise<void | ResponseObject> {
        try {
            await this.axiosInst.delete(`delete/${id}`);
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }

    private async GetByIdAsync(id: number): Promise<ArticuloResponse | ResponseObject>{
        try {
            return (await this.axiosInst.get<any, AxiosResponse<ArticuloResponse>>(id.toString())).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }

    private async GetPaginatedAsync(page: number, size: number, categories: string[], authorId: number): Promise<ResponseObject | ArticuloResponse[]> {
        try {
            let url: string = `all?page=${page}&size=${size}`;

            if (categories.length > 0) {
                const categoriesStr: string = categories.join('+');
                url += `&categories=${categoriesStr}`;
            }

            if (authorId > 0)
                url += `&authorId=${authorId}`;

            return (await this.axiosInst.get<any, AxiosResponse<ArticuloResponse[]>>(url)).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }
}

export function CreateArticleService(): IArticleService { return new ArticleService(); }