import React from "react";

/**
 * Admin-Footer. Goes into the main div body, but after the rest of the content.
 * It has credits to the author of this template.
 */
export function AdminFooter(): React.JSX.Element {
    return (
        <footer>
            <div className="footer clearfix mb-0 text-muted">
                <div className="float-start">
                    <p>2021 &copy; Mazer</p>
                </div>
                <div className="float-end">
                    <p>Crafted with <span className="text-danger"><i className="bi bi-heart"></i></span> by <a
                        href="http://ahmadsaugi.com">A. Saugi</a></p>
                </div>
            </div>
        </footer>
    );
}