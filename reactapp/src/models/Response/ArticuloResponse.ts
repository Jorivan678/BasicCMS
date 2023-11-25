import { CategoriaResponse } from './CategoriaResponse';
import { ImagenResponse } from './ImagenResponse';
import { UserResponse } from './UserResponse';

export interface ArticuloResponse
{
    id: number;
    titulo: string;
    contenido: string;
    fecha_Pub: Date;
    autorId: number;
    imagenId: number;
    autor: UserResponse;
    imagen: ImagenResponse;
    categorias: CategoriaResponse[];
}