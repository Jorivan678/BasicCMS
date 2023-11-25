import React, { Component } from "react";
import { Link } from "react-router-dom";
import { AuthGeneralState, Redirect, SetAuthStyles, } from "../utils";
import { ClearStyles, GetClaims, JWTClaims } from "../../../generalUtils";
import { CreateAuthService, CreateUserService, IAuthService } from "../../../services";
import Swal from "sweetalert2";
import { ResponseErrorObject, ResponseObject, UserResponse } from "../../../models";

interface LoginProps
{
    setUser: (payload: UserResponse) => any;
    setLogInState: (payload: boolean) => any;
    setClaims: (payload: JWTClaims) => any;
    isLoggedIn: boolean;
}

export class Login extends Component<LoginProps, AuthGeneralState>
{
    private service: IAuthService;
    private usernameRef: React.RefObject<HTMLInputElement>;
    private passwordRef: React.RefObject<HTMLInputElement>;

    constructor(props: LoginProps) {
        super(props);

        this.service = CreateAuthService();
        this.usernameRef = React.createRef();
        this.passwordRef = React.createRef();
        this.state = { errorMessages: {} };
    }

    componentDidMount() {
        document.title = 'Basic CMS - Login';
        SetAuthStyles();
    }

    componentWillUnmount() {
        ClearStyles('admin');
    }

    private handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();

        const userNameInput = this.usernameRef.current?.value;
        const passwordInput = this.passwordRef.current?.value;

        let result = await this.service.LoginAsync({ username: userNameInput ?? '', password: passwordInput ?? '' });
        this.manageApiResponse(result);
    }

    private manageApiResponse(result: void | ResponseErrorObject | ResponseObject) {
        if (!result) {
            this.retrieveUserData();
            this.props.setLogInState(true);
            this.props.setClaims(GetClaims()!);
            this.setState(() => ({ errorMessages: {} }))
        }
        else
        if ('errors' in result) {
            const errorMessages: { [key: string]: string[] } = {};

            result.errors.forEach((modelError) => {
                errorMessages[modelError.key.toLowerCase()] = modelError.messages;
            });

            this.setValidationErrors(typeof errorMessages['username'] !== 'undefined', typeof errorMessages['password'] !== 'undefined')

            this.props.setLogInState(false);
            this.setState(() => ({ errorMessages }));
        } else {
            Swal.fire({
                title: result.title,
                text: result.message,
                confirmButtonText: 'Ok!'
            });
        }
    }

    private async retrieveUserData() {
        let userService = CreateUserService();
        let claims = GetClaims();
        if (claims) {
            let user = await userService.GetAsync(claims.nameid);
            if ('type' in user) {
                this.manageApiResponse(user);
            } else {
                this.props.setUser(user);
            }
        }
    }

    private setValidationErrors(isUsernameError: boolean, isPasswordError: boolean) {
        if (isUsernameError && this.usernameRef.current) {
            this.usernameRef.current.classList.add('is-invalid');
        }
        if (isPasswordError && this.passwordRef.current) {
            this.passwordRef.current.classList.add('is-invalid');
        }
    }

    private handleInputOnChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.value.length > 0)
            event.target.classList.remove('is-invalid');
        else
            event.target.classList.add('is-invalid');
    }

    render(): React.JSX.Element {

        if (this.props.isLoggedIn) {
            return <Redirect />
        }

        return (
            <div id="auth">
                <div className="row h-100">
                    <div className="col-lg-5 col-12">
                        <div id="auth-left">
                            <div className="auth-logo">
                                <Link to="/"><img src="/assets/admin/images/logo/logo.png" alt="Logo" /></Link>
                            </div>
                            <h1 className="auth-title">Log in.</h1>
                            <p className="auth-subtitle mb-5">Log in with your data that you entered during registration.</p>

                            <form onSubmit={this.handleSubmit} noValidate={true}>
                                <div className="form-group position-relative has-icon-left mb-4">
                                    <input onChange={this.handleInputOnChange} type="text" className="form-control form-control-xl" placeholder="Username" ref={this.usernameRef} />
                                    <div className="form-control-icon">
                                        <i className="bi bi-person"></i>
                                    </div>
                                    {this.state.errorMessages['username']?.length > 0 ?
                                        this.state.errorMessages['username'].map((message) => <div key={message} className="invalid-feedback" style={{ position: 'fixed' }}>{message}</div>)
                                        : null
                                    }
                                </div>
                                <div className="form-group position-relative has-icon-left mb-4">
                                    <input onChange={this.handleInputOnChange} type="password" className="form-control form-control-xl" placeholder="Password" ref={this.passwordRef} />
                                    <div className="form-control-icon">
                                        <i className="bi bi-shield-lock"></i>
                                    </div>
                                    {this.state.errorMessages['password']?.length > 0 ?
                                        this.state.errorMessages['password'].map((message) => <div key={message} className="invalid-feedback" style={{ position: 'fixed' }}>{message}</div>)
                                        : null
                                    }
                                </div>
                                <button className="btn btn-primary btn-block btn-lg shadow-lg mt-5" type="submit">Log in</button>
                            </form>
                            <div className="text-center mt-5 text-lg fs-4">
                                <p className="text-gray-600">Don&apos;t have an account? <Link to="/register"
                                    className="font-bold">Sign up</Link>.</p>
                            </div>
                        </div>
                    </div>
                    <div className="col-lg-7 d-none d-lg-block">
                        <div id="auth-right">
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}