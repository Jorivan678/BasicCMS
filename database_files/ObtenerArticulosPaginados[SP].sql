-- Database: BasicCMS

-- Hora de crear procesos almacenados.
-- La columna 'split' solo sirve para indicarle a dapper donde debe empezar el mapeo
-- de otra entidad (tabla).

CREATE OR REPLACE FUNCTION ObtenerArticulosPaginados
(
	IN pagina_ int,
	IN cantidad_ int default 10,
	IN autorId_ int default 0,
	IN categorias_ varchar[] = null
)
RETURNS TABLE (
	id int,
	titulo varchar(50),
	contenido text,
	fecha_pub timestamp with time zone,
	autorid int,
	split boolean,
	nombre varchar(30),
	apellidop varchar(30),
	apellidom varchar(30),
	nombreusuario varchar(50)
)
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT DISTINCT ar.*, false as split, ''::character varying as nombre, ''::character varying as apellidop, ''::character varying as apellidom, u.nombreusuario 
	FROM articulos AS ar
	LEFT JOIN usuarios AS u ON u.id = ar.autorid
	LEFT JOIN articulos_categorias AS ac ON ar.id = ac.articuloid
	LEFT JOIN categorias AS c ON c.id = ac.categoriaid
	WHERE (autorId_ = 0 OR ar.autorid = autorId_)
	AND (categorias_ IS NULL OR UPPER(c.nombre) = ANY(categorias_))
	ORDER BY ar.id DESC
	LIMIT cantidad_
	OFFSET (pagina_ - 1) * cantidad_;
END;
$$;