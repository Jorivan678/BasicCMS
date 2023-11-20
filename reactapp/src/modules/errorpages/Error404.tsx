import React, { useEffect } from "react";
import { ClearStyles, adminCommonStyles } from "../../generalUtils";
import { Link } from "react-router-dom";

/**
 * The 404 error page.
 */
export function Error404(): React.JSX.Element {
    useEffect(() => {
        document.title = "Basic CMS - Not Found!";
        SetError404Styles();

        return ClearStyles;
    }, []);

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

function SetError404Styles() {
    const styles = adminCommonStyles.filter((style) => style);
    styles.push({ key: 'error', value: '/assets/admin/css/pages/error.css' });
    styles.forEach(style => {
        const linkElement = document.createElement('link');
        linkElement.rel = 'stylesheet';
        linkElement.href = style.value;
        document.head.appendChild(linkElement);
    });
}