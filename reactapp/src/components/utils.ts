import { useLocation } from "react-router-dom";

/**
 * Adds active class if the current pathname is the same <a></a> href property.
 * @param pathname The current pathname. Don't put '/' at the beginning of the string, it will be automatically added.
 */
export function AddActiveClass(pathname: string): string {
    const reactLocation = useLocation();
    return reactLocation.pathname == `/${pathname}` ? 'active' : '';
}