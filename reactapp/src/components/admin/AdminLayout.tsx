import React from "react";
import { AdminNavbarLayout } from "./AdminNavbarLayout";
import { ClearScripts, ClearStyles, Dictionary, ReadOnlyDictionary, adminCommonStyles } from "../../generalUtils";
import { AdminMainHeader } from "./AdminMainHeader";
import { AdminFooter } from "./AdminFooter";

interface AdminLayoutProps
{
    children: React.ReactNode;
    scripts: Dictionary<string, string>;
    stylesDir: Dictionary<string, string>;
}

interface AdminLayoutState
{
    wasAlreadyRendered: boolean;
}

export class AdminLayout extends React.Component<AdminLayoutProps, AdminLayoutState>
{
    private static scripts: ReadOnlyDictionary<string, string> = [
        { key: 'psjs', value: '/assets/admin/vendors/perfect-scrollbar/perfect-scrollbar.min.js' },
        { key: 'boostrap', value: '/assets/admin/js/bootstrap.bundle.min.js' },
        { key: 'mainjs', value: '/assets/admin/js/main.js' }
    ];

    constructor(props: AdminLayoutProps) {
        super(props);

        this.state = { wasAlreadyRendered: false };
    }

    componentDidMount() {
        if (!this.state.wasAlreadyRendered) {
            document.title = 'BasicCMS - Admin';
            this.setExternalStyles();
            this.setExternalScripts();
            this.setState((prevState) => ({ wasAlreadyRendered: !prevState.wasAlreadyRendered }));
        }
    }

    private setExternalScripts() {
        const allScripts = [...AdminLayout.scripts, ...this.props.scripts];
        allScripts.forEach(script => {
            const scriptElement = document.createElement('script');
            scriptElement.src = script.value;
            scriptElement.async = false;
            document.body.appendChild(scriptElement);
        });
    }

    private setExternalStyles() {
        const common = adminCommonStyles.filter((item) => item);
        common.splice(2, 0, { key: 'perfect-scrollbar', value: '/assets/admin/vendors/perfect-scrollbar/perfect-scrollbar.css' });
        const allStyles = [...common, ...this.props.stylesDir];
        allStyles.forEach(style => {
            const linkElement = document.createElement('link');
            linkElement.rel = 'stylesheet';
            linkElement.href = style.value;
            document.head.appendChild(linkElement);
        });
    }

    componentWillUnmount() {
        ClearScripts();
        ClearStyles();
    }

    render(): React.JSX.Element {
        return (
            <div id="app">
                <AdminNavbarLayout />
                <div id="main" className="layout-navbar">
                    <AdminMainHeader />
                    <div id="main-content">
                        {this.props.children}
                        <AdminFooter />
                    </div>
                </div>
            </div>
        );
    }
}