/* eslint-disable no-dupe-class-members */
import { AxiosInstance, AxiosResponse } from "axios";
import { CreateRequest } from "./axios.config";
import { ResponseErrorObject, ResponseObject, RoleResponse, RoleUpdRequest } from "../models";
import { HandleAxiosGenericError, HandleAxiosWithBadRequestError } from "./serviceUtils";

/**
 * A contract for manage requests to API role endpoints.
 */
export interface IRoleService
{
    /**
     * Gets all authorization roles from API.
     * If an api error ocurred, it returns a ResponseObject.
     */
    GetAsync(): Promise<RoleResponse[] | ResponseObject>;

    /**
     * Gets a specific role from API.
     * If an api error ocurred, it returns a ResponseObject.
     * @param id The role id.
     */
    GetAsync(id: number): Promise<RoleResponse | ResponseObject>;

    /**
     * Sends a request to edit role information (just the description).
     * Receives void if request was successful; a ResponseErrorObject if request data didn't satisfy validations;
     * a ResponseObject for other types of responses (not 200 or 400).
     * @param request Contains request information.
     */
    EditAsync(request: RoleUpdRequest): Promise<void | ResponseErrorObject | ResponseObject>;
}

class RoleService implements IRoleService
{
    private axiosInst: AxiosInstance;

    constructor() {
        this.axiosInst = CreateRequest('roles');
    }

    GetAsync(): Promise<ResponseObject | RoleResponse[]>;
    GetAsync(id: number): Promise<RoleResponse | ResponseObject>;

    GetAsync(id?: number): Promise<ResponseObject | RoleResponse[]> | Promise<RoleResponse | ResponseObject>
    {
        if (id != undefined) {
            return this.GetByIdAsync(id);
        }
        return this.GetAllAsync();
    }

    async EditAsync(request: RoleUpdRequest): Promise<void | ResponseObject | ResponseErrorObject>
    {
        try {
            await this.axiosInst.putForm<RoleUpdRequest, void>('edit', request);
        } catch (error: any) {
            return HandleAxiosWithBadRequestError(error);
        }
    }

    private async GetAllAsync(): Promise<ResponseObject | RoleResponse[]>
    {
        try {
            return (await this.axiosInst.get<any, AxiosResponse<RoleResponse[]>>('all')).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }

    private async GetByIdAsync(id: number): Promise<RoleResponse | ResponseObject>
    {
        try {
            return (await this.axiosInst.get<any, AxiosResponse<RoleResponse>>(id.toString())).data;
        } catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }
}

export function CreateRoleService(): IRoleService { return new RoleService(); }