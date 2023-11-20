import { UserResponse } from './UserResponse';

export interface ComentarioResponse
{
    id: number;
    texto: string;
    fecha_Pub: Date;
    autorId: number;
    articuloId: number;
    autor: UserResponse;
}