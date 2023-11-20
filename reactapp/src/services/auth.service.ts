import { AxiosInstance, AxiosResponse } from "axios";
import { CreateRequest } from "./axios.config";
import {
    LoginRequest, PasswordUpdRequest,
    ResponseErrorObject, ResponseObject, TokenResponse
} from "../models";
import { HandleAxiosGenericError, HandleAxiosWithBadRequestError } from "./serviceUtils";
import { SetAuthToken } from "../generalUtils";

/**
 * A contract for manage requests to API auth endpoints.
 */
export interface IAuthService
{
    /**
     * Sends a request to Login and retrieve the session token.
     * Receives void if request was successful; a ResponseErrorObject if request data didn't satisfy validations;
     * a ResponseObject for other types of responses (not 200 or 400).
     * @param request Login data to send to the API.
     */
    LoginAsync(request: LoginRequest): Promise<void | ResponseErrorObject | ResponseObject>;

    /**
     * Assigns a role to an application user. If an api error ocurred, it returns a ResponseObject.
     * @param userId The user identificator.
     * @param roleId The role identificator.
     */
    AssignRoleAsync(userId: number, roleId: number): Promise<void | ResponseObject>;

    /**
     * Removes a role from an application user. If an api error ocurred, it returns a ResponseObject.
     * @param userId The user identificator.
     * @param roleId The role identificator.
     */
    RemoveRoleAsync(userId: number, roleId: number): Promise<void | ResponseObject>;

    /**
     * Updates user password.
     * Receives void if request was successful; a ResponseErrorObject if request data didn't satisfy validations;
     * a ResponseObject for other types of responses (not 200 or 400).
     * @param request Request data to send to the API.
     */
    UpdatePasswordAsync(request: PasswordUpdRequest): Promise<void | ResponseErrorObject | ResponseObject>;
}

class AuthService implements IAuthService
{
    private axiosInst: AxiosInstance;

    constructor() {
        this.axiosInst = CreateRequest('auth');
    }

    async LoginAsync(request: LoginRequest): Promise<void | ResponseErrorObject | ResponseObject> {
        try {
            let result = await this.axiosInst.postForm<LoginRequest, AxiosResponse<TokenResponse>>('login', request);

            SetAuthToken(result.data.token);
        }
        catch (error: any) {
            return HandleAxiosWithBadRequestError(error);
        }
    }

    async AssignRoleAsync(userId: number, roleId: number): Promise<void | ResponseObject> {
        try {
            await this.axiosInst.put(`assign/${userId}?roleId=${roleId}`);
        }
        catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }

    async RemoveRoleAsync(userId: number, roleId: number): Promise<void | ResponseObject> {
        try {
            await this.axiosInst.put(`remove/${userId}?roleId=${roleId}`);
        }
        catch (error: any) {
            return HandleAxiosGenericError(error);
        }
    }

    async UpdatePasswordAsync(request: PasswordUpdRequest): Promise<void | ResponseErrorObject | ResponseObject> {
        try {
            await this.axiosInst.putForm<PasswordUpdRequest>('user/change-password', request);
        }
        catch (error: any) {
            return HandleAxiosWithBadRequestError(error);
        }
    }
}

export function CreateAuthService(): IAuthService { return new AuthService(); }