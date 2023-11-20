import React from "react";
import { AddActiveClass } from "../utils";
import { Link } from "react-router-dom";

export function AdminNavbarLayout(): React.JSX.Element
{
    return (
        <div id="sidebar" className="active">
            <div className="sidebar-wrapper active">
                <div className="sidebar-header">
                    <div className="d-flex justify-content-between">
                        <div className="logo">
                            <Link to="/admin/home"><img src="/assets/admin/images/logo/logo.png" alt="Logo" srcSet="" /></Link>
                        </div>
                        <div className="toggler">
                            <a href="#" className="sidebar-hide d-xl-none d-block"><i className="bi bi-x bi-middle"></i></a>
                        </div>
                    </div>
                </div>
                <div className="sidebar-menu">
                    <ul className="menu">
                        <li className="sidebar-title">Menu</li>

                        <li className={`sidebar-item ${AddActiveClass('admin/home')}`}>
                            <Link to="/admin/home" className='sidebar-link'>
                                <i className="bi bi-grid-fill"></i>
                                <span>Home</span>
                            </Link>
                        </li>

                        <li className={`sidebar-item ${AddActiveClass('admin/view-articles')}${AddActiveClass('admin/post-article')} has-sub`}>
                            <a href="#" className='sidebar-link'>
                                <i className="bi bi-stack"></i>
                                <span>Manage Articles</span>
                            </a>
                            <ul className={`submenu ${AddActiveClass('admin/view-articles')}${AddActiveClass('admin/post-article')}`}>
                                <li className={`submenu-item ${AddActiveClass('admin/view-articles')}`}>
                                    <Link to="/admin/view-articles">View articles</Link>
                                </li>
                                <li className={`submenu-item ${AddActiveClass('admin/post-article')}`}>
                                    <Link to="/admin/post-article">Post new article</Link>
                                </li>
                            </ul>
                        </li>

                        <li className={`sidebar-item ${AddActiveClass('admin/view-categories')}${AddActiveClass('admin/add-category')} has-sub`}>
                            <a href="#" className='sidebar-link'>
                                <i className="bi bi-collection-fill"></i>
                                <span>Manage Categories</span>
                            </a>
                            <ul className={`submenu ${AddActiveClass('admin/view-categories')}${AddActiveClass('admin/add-category')}`}>
                                <li className={`submenu-item ${AddActiveClass('admin/view-categories')}`}>
                                    <Link to="/admin/view-categories">View categories</Link>
                                </li>
                                <li className={`submenu-item ${AddActiveClass('admin/add-category')}`}>
                                    <Link to="/admin/add-category">Add new category</Link>
                                </li>
                            </ul>
                        </li>

                        <li className={`sidebar-item ${AddActiveClass('admin/view-images')}${AddActiveClass('admin/add-image')} has-sub`}>
                            <a href="#" className='sidebar-link'>
                                <i className="bi bi-grid-1x2-fill"></i>
                                <span>Manage Images</span>
                            </a>
                            <ul className={`submenu ${AddActiveClass('admin/view-images')}${AddActiveClass('admin/add-image')}`}>
                                <li className={`submenu-item ${AddActiveClass('admin/view-images')}`}>
                                    <Link to="/admin/view-images">View images</Link>
                                </li>
                                <li className={`submenu-item ${AddActiveClass('admin/add-image')}`}>
                                    <Link to="/admin/add-image">Add new image</Link>
                                </li>
                            </ul>
                        </li>

                        <li className={`sidebar-item ${AddActiveClass('admin/manage-roles')}`}>
                            <Link to="/admin/manage-roles" className='sidebar-link'>
                                <i className="bi bi-grid-fill"></i>
                                <span>Manage Roles</span>
                            </Link>
                        </li>

                        <li className={`sidebar-item ${AddActiveClass('admin/manage-users')}`}>
                            <Link to="/admin/manage-users" className='sidebar-link'>
                                <i className="bi bi-grid-fill"></i>
                                <span>Manage Users</span>
                            </Link>
                        </li>
                    </ul>
                </div>
                <button className="sidebar-toggler btn x"><i data-feather="x"></i></button>
            </div>
        </div>
    );
}