import { UserResponse } from "./UserResponse";

export interface ImagenResponse
{
    id: number;
    titulo_Imagen: string;
    ruta: string;
    alto: number;
    ancho: number;
    fecha_Subida: Date;
    usuarioId: number;
    usuario: UserResponse;
}