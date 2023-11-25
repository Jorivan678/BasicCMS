import React from "react";
import { AreScriptsAndStylesSet, ClearStyles } from "../../generalUtils";
import { Link } from "react-router-dom";
import { SetErrorStyles } from "./utils";

/**
 * The 404 error page.
 */
export class Error404 extends React.Component
{
    componentDidMount() {
        document.title = "Basic CMS - Not Found!";
        if (!AreScriptsAndStylesSet('admin', false, true))
        {
            SetErrorStyles();
        }
    }

    componentWillUnmount() {
        ClearStyles('admin');
    }

    render(): React.JSX.Element {
        return (
            <div id="error">
                <div className="error-page container">
                    <div className="col-md-8 col-12 offset-md-2">
                        <img className="img-error" src="/assets/admin/images/samples/error-404.png" alt="Not Found" />
                        <div className="text-center">
                            <h1 className="error-title">NOT FOUND</h1>
                            <p className='fs-5 text-gray-600'>The page you are looking not found.</p>
                            <Link to="/" className="btn btn-lg btn-outline-primary mt-3">Go Home</Link>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}