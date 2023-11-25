import React from "react";
import { Link } from "react-router-dom";
import { SetErrorStyles } from "./utils";
import { ClearStyles } from "../../generalUtils";

/**
 * The 500 error page.
 */
export class Error500 extends React.Component
{
    componentDidMount(){
        document.title = "Basic CMS - Internal Server Error!";
        SetErrorStyles();
    }

    componentWillUnmount(){
        ClearStyles('admin');
    }

    render() {
        return (
            <div id="error">
                <div className="error-page container">
                    <div className="col-md-8 col-12 offset-md-2">
                        <img className="img-error" src="/assets/admin/images/samples/error-500.png" alt="Internal Server Error" />
                        <div className="text-center">
                            <h1 className="error-title">System Error</h1>
                            <p className="fs-5 text-gray-600">The website is currently unaivailable. Try again later or contact the
                                developer.</p>
                            <Link to="/" className="btn btn-lg btn-outline-primary mt-3">Go Home</Link>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}