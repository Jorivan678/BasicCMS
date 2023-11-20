import React from "react";
import { AdminLayout } from "../../../components";

interface AdminHomeState
{
    articlesCount: number;
}

const initialState: AdminHomeState = {
    articlesCount: 0
} 

export function AdminHome(): React.JSX.Element
{
    return (
        <AdminLayout scripts={[]} stylesDir={[]}>
                <div className="page-heading">
                    <div className="page-title">
                        <div className="row">
                            <div className="col-12 col-md-6 order-md-1 order-last">
                                <h3>Vertical Layout with Navbar</h3>
                                <p className="text-subtitle text-muted">Navbar will appear in top of the page.</p>
                            </div>
                            <div className="col-12 col-md-6 order-md-2 order-first">
                                <nav aria-label="breadcrumb" className="breadcrumb-header float-start float-lg-end">
                                    <ol className="breadcrumb">
                                        <li className="breadcrumb-item"><a href="index.html">Dashboard</a></li>
                                        <li className="breadcrumb-item active" aria-current="page">Layout Vertical Navbar
                                        </li>
                                    </ol>
                                </nav>
                            </div>
                        </div>
                    </div>
                    <section className="section">
                        <div className="card">
                            <div className="card-header">
                                <h4 className="card-title">Example Content</h4>
                            </div>
                            <div className="card-body">
                                Lorem ipsum dolor sit amet consectetur adipisicing elit. Consectetur quas omnis
                                laudantium tempore
                                exercitationem, expedita aspernatur sed officia asperiores unde tempora maxime odio
                                reprehenderit
                                distinctio incidunt! Vel aspernatur dicta consequatur!
                            </div>
                        </div>
                    </section>
                </div>
        </AdminLayout>
    );
}