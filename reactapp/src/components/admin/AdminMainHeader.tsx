import React, { useEffect, useRef, useState } from "react";
import { useLogOut } from "../../generalUtils";
import { UserResponse } from "../../models";
import { useSelector } from "react-redux";
import { RootState } from "../../redux/store";

/**
 * The header that goes after the NavBar but within the main div body of the page.
 * @returns
 */
export function AdminMainHeader(): React.JSX.Element
{
    const user = useSelector<RootState, UserResponse>((state) => state.user);
    const [isDropdownOpen, setState] = useState<boolean>(false);
    const divUserDropdown = useRef<HTMLDivElement>(null);
    const logOut = useLogOut();

    const handleUserDropdown = (event: React.MouseEvent<HTMLAnchorElement>) => {
        event.preventDefault();
        setState((prevState) => !prevState);
    };

    const handleLogOut = (event: React.MouseEvent<HTMLAnchorElement>) => {
        event.preventDefault();
        logOut();
    };

    //Manages Dropdown 'cause bootstrap is not working in that sense.
    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
            if (
                divUserDropdown.current &&
                !divUserDropdown.current.contains(event.target as Node)
            ) {
                setState(() => false);
            }
        };

        document.addEventListener("mousedown", handleClickOutside);

        return () => {
            document.removeEventListener("mousedown", handleClickOutside);
        };
    }, [divUserDropdown]);

    const setDropdownShow: string = isDropdownOpen ? 'show' : '';

    return (
        <header className='mb-3'>
            <nav className="navbar navbar-expand navbar-light ">
                <div className="container-fluid">
                    <a href="#" className="burger-btn d-block">
                        <i className="bi bi-justify fs-3"></i>
                    </a>

                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse"
                        data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul className="navbar-nav ms-auto mb-2 mb-lg-0">

                        </ul>
                        <div className="dropdown" ref={divUserDropdown}>
                            <a href="#" onClick={handleUserDropdown} aria-expanded={isDropdownOpen} className={setDropdownShow}>
                                <div className="user-menu d-flex">
                                    <div className="user-name text-end me-3">
                                        <h6 className="mb-0 text-gray-600">{`${user.nombre} ${user.apellidoP} ${user.apellidoM}`}</h6>
                                        <p className="mb-0 text-sm text-gray-600">Administrator</p>
                                    </div>
                                    <div className="user-img d-flex align-items-center">
                                        <div className="avatar avatar-md">
                                            <img src="/assets/admin/images/faces/1.jpg" />
                                        </div>
                                    </div>
                                </div>
                            </a>
                            <ul className={`dropdown-menu dropdown-menu-end ${setDropdownShow}`} aria-labelledby="dropdownMenuButton">
                                <li>
                                    <h6 className="dropdown-header">Hello, @{user.nombreUsuario}!</h6>
                                </li>
                                <li><a className="dropdown-item" href="#"><i className="icon-mid bi bi-person me-2"></i> My
                                    Profile</a></li>
                                <li><a className="dropdown-item" href="#"><i className="icon-mid bi bi-gear me-2"></i>
                                    Settings</a></li>
                                <li><a className="dropdown-item" href="#"><i className="icon-mid bi bi-wallet me-2"></i>
                                    Wallet</a></li>
                                <li>
                                    <hr className="dropdown-divider" />
                                </li>
                                <li><a className="dropdown-item" href="#" onClick={handleLogOut}><i
                                    className="icon-mid bi bi-box-arrow-left me-2"></i> Logout</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </nav>
        </header>
    );
}