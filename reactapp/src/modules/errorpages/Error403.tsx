import React from "react";
import { Link } from "react-router-dom";
import { SetErrorStyles } from "./utils";
import { ClearStyles } from "../../generalUtils";

/**
 * The 403 error page.
 */
export class Error403 extends React.Component
{ 
    componentDidMount() {
        document.title = "Basic CMS - Forbidden!";
        SetErrorStyles();
    }

    componentWillUnmount() {
        ClearStyles('admin');
    }

    render() {
        return (
            <div id="error">
                <div className="error-page container">
                    <div className="col-md-8 col-12 offset-md-2">
                        <img className="img-error" src="/assets/admin/images/samples/error-403.png" alt="Forbidden" />
                        <div className="text-center">
                            <h1 className="error-title">Forbidden</h1>
                            <p className="fs-5 text-gray-600">You are unauthorized to see this page.</p>
                            <Link to="/" className="btn btn-lg btn-outline-primary mt-3">Go Home</Link>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}