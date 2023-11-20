import React from 'react';
import { Navigate } from 'react-router';
import { JWTClaims } from '../generalUtils';

interface ProtectedRouteProps
{
    user: JWTClaims;
    useSpecificRoles?: boolean;
    specificRoles?: string[];
    mustHaveAllRoles?: boolean;
    redirectPath: string;
    children: React.ReactNode;
}

export class ProtectedRoute extends React.Component<ProtectedRouteProps>
{
    private handleSpecificRoles(): React.JSX.Element | null {
        if (this.props.mustHaveAllRoles == undefined) {
            throw new Error("When useSpecificRoles is true, mustHaveAllRoles must be provided.");
        }

        let hasSpecificRoles: boolean;
        if (!this.props.mustHaveAllRoles) {
            hasSpecificRoles = false;
            this.props.specificRoles?.forEach((role) => {
                hasSpecificRoles = this.props.user.role.includes(role) || hasSpecificRoles;
            });
        }
        else {
            hasSpecificRoles = true;
            this.props.specificRoles?.forEach((role) => {
                hasSpecificRoles = hasSpecificRoles && this.props.user.role.includes(role);
            });
        }

        if (!hasSpecificRoles)
            return <Navigate to={this.props.redirectPath} replace={true} />;

        return null;
    }

    render(): React.JSX.Element | React.ReactNode {
        if (this.props.user.exp == 0) {
            return <Navigate to="/login" replace={true} />;
        }

        if (typeof this.props.useSpecificRoles !== 'undefined' && this.props.useSpecificRoles) {
            if (!this.props.specificRoles) throw new Error('When useSpecificRoles is true, specificRoles must be provided.');

            let result = this.handleSpecificRoles();

            if (result) return result;
        }

        return this.props.children;
    }
}