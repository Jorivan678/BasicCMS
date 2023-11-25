import React, { useEffect, useState } from "react";
import { AdminLayout } from "../../../components";
import { CreateArticleService } from "../../../services";
import { useSelector } from "react-redux";
import { RootState } from "../../../redux";
import {
    ArticuloResponse, CountResponse,
    ResponseObject, UserResponse
} from "../../../models";
import { toast } from "react-toastify";

const defaultUser: UserResponse = { id: 0, nombre: '', apellidoP: '', apellidoM: '', nombreUsuario: '' };

const articleInitialState: ArticuloResponse = {
    autorId: 0, imagenId: 0, contenido: '', fecha_Pub: new Date(Date.now()), id: 0, titulo: '',
    autor: defaultUser,
    imagen: {
        fecha_Subida: new Date(Date.now()), alto: 0, ancho: 0, id: 0, ruta: '', titulo_Imagen: '', usuario: defaultUser, usuarioId: 0
    },
    categorias: []
};

export function AdminHome(): React.JSX.Element
{
    const [articlesCount, setArticlesCount] = useState<number>(0);
    const [recentArticle, setRecentArticle] = useState<ArticuloResponse>(articleInitialState);
    const [articleService] = useState(CreateArticleService());
    const user = useSelector<RootState, UserResponse>((state) => state.user);

    useEffect(() => {
        const showToast = (res: ResponseObject) => {
            toast<string>(res.message, {
                position: "top-right",
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
                theme: "dark",
                type: 'error'
            });
        };

        const handleResponse = (res: CountResponse | ResponseObject) => {
            if ('count' in res) {
                setArticlesCount(res.count);
            } else {
                showToast(res);
            }
        };

        const handleArtResponse = (res: ArticuloResponse[] | ResponseObject) => {
            if ('statusCode' in res)
                showToast(res);
            else if (res.length > 0)
                setRecentArticle(res[0]);
            else
                setRecentArticle(articleInitialState);
        };

        const getAllData = async () => {
            let result = await articleService.GetCountAsync([], user.id);
            let artResult = await articleService.GetAsync(1, 1, [], user.id);
            handleResponse(result);
            handleArtResponse(artResult);
        };

        getAllData();
    }, []);

    return (
        <AdminLayout scripts={[]} stylesDir={[]}>
            <div className="page-heading">
                <div className="page-title">
                    <div className="row">
                        <div className="col-12 col-md-6 order-md-1 order-last">
                            <h3>Home</h3>
                            <p className="text-subtitle text-muted">Hey there! It&apos;s good to see you again.</p>
                        </div>
                        <div className="col-12 col-md-6 order-md-2 order-first">
                            <nav aria-label="breadcrumb" className="breadcrumb-header float-start float-lg-end">
                                <ol className="breadcrumb">
                                    <li className="breadcrumb-item active" aria-current="page">Home</li>
                                </ol>
                            </nav>
                        </div>
                    </div>
                </div>
                <section className="section">
                    <div className="row">
                        <div className="col-xl-4 col-md-6 col-sm-12">
                            <div className="card">
                                <div className="card-content">
                                    <div className="card-body">
                                        <h4 className="card-title">Number of articles</h4>
                                        <p className="card-text">
                                            This shows the number of articles you have published: {articlesCount}.
                                        </p>
                                    </div>
                                    <img className="img-fluid w-100" src="/assets/admin/images/some/article-writing.jpg"
                                        alt="article image" />
                                </div>
                            </div>

                        </div>
                        <div className="col-xl-4 col-md-6 col-sm-12">
                            <div className="card">
                                <div className="card-content">
                                    <div className="card-body">
                                        <h4 className="card-title">Manage images</h4>
                                        <p className="card-text">
                                            Manage all images that you can use as a cover for your articles.
                                        </p>
                                    </div>
                                    <img className="img-fluid w-100" src="/assets/admin/images/some/image-cover.jpg"
                                        alt="Card image cap" />
                                </div>
                            </div>

                        </div>
                        <div className="col-xl-4 col-md-6 col-sm-12">
                            <div className="card">
                                <div className="card-content">
                                    <div className="card-body">
                                        <h4 className="card-title">Receive comments</h4>
                                        <p className="card-text">
                                            Receive comments and feedback from users that visit your blog :D
                                        </p>
                                    </div>
                                    <img className="img-fluid w-100" src="/assets/admin/images/some/social-comments.png"
                                        alt="Card image cap" />
                                </div>
                            </div>

                        </div>
                        {recentArticle.id != 0 ?
                            <div className="col-md-6 col-sm-12">
                                <div className="card">
                                    <div className="card-content">
                                        <img className="card-img-top img-fluid" src={recentArticle.imagen.ruta}
                                            alt="Card image cap" style={{ height: '20rem' }} />
                                        <div className="card-body">
                                            <h4 className="card-title">Your most recent article</h4>
                                            <p className="card-text">
                                                {recentArticle.titulo}
                                            </p>
                                            <p className="card-text">
                                                Publication Date: {recentArticle.fecha_Pub.toLocaleString()}
                                            </p>
                                            {/*<button className="btn btn-primary block">See now</button>*/}
                                        </div>
                                    </div>
                                </div>
                            </div> : null
                        }
                        {recentArticle.id != 0 ?
                            <div className="col-md-6 col-sm-12">
                                <div className="card">
                                    <div className="card-content">
                                        <div className="card-body">
                                            <h4 className="card-title">Public an article now :)</h4>
                                            <p className="card-text">
                                                You can publish an article wherever you want.
                                            </p>
                                            <p className="card-text">
                                                Cupcake fruitcake macaroon donut pastry gummies tiramisu chocolate bar
                                                muffin. My favorite lul.
                                            </p>
                                            <small className="text-muted">A random timestamp: {new Date(Date.now()).toLocaleString()}</small>
                                        </div>
                                        <img className="card-img-bottom img-fluid" src="/assets/admin/images/samples/water.jpg"
                                            alt="Card image cap" style={{ height: '20rem', objectFit: 'cover' }} />
                                    </div>
                                </div>
                            </div> : null
                        }
                    </div>
                </section>
            </div>
        </AdminLayout>
    );
}