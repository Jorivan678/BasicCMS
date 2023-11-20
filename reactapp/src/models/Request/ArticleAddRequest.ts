import { CategoryIdRequest } from "./CategoryIdRequest";

export interface ArticleAddRequest
{
    titulo: string;
    contenido: string;
    autorId: number;
    categorias: CategoryIdRequest[];
}