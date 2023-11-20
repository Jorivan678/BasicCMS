import { CategoryIdRequest } from "./CategoryIdRequest";

export interface ArticleUpdRequest
{
    id: number;
    titulo: string;
    contenido: string;
    autorId: number;
    categorias: CategoryIdRequest[];
}