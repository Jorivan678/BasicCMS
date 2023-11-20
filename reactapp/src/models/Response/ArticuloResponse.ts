import { CategoriaResponse } from './CategoriaResponse';
import { UserResponse } from './UserResponse';

export interface ArticuloResponse
{
    id: number;
    titulo: string;
    contendio: string;
    fecha_Pub: Date;
    autorId: number;
    autor: UserResponse;
    categorias: CategoriaResponse[];
}