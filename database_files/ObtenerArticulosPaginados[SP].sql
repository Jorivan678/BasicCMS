-- Database: BasicCMS

-- Hora de crear procesos almacenados.
-- La columna 'split' solo sirve para indicarle a dapper donde debe empezar el mapeo
-- de otra entidad (tabla).

CREATE OR REPLACE FUNCTION ObtenerArticulosPaginados
(
	IN pagina_ int,
	IN cantidad_ int = 10,
	IN autorId_ int = 0
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
	IF autorId_ = 0 THEN
		RETURN QUERY
		SELECT ar.*, false as split, u.nombre, u.apellidop, u.apellidom, u.nombreusuario FROM articulos AS ar
		INNER JOIN usuarios AS u ON u.id = ar.autorid 
		ORDER BY ar.id DESC
		LIMIT cantidad_
		OFFSET (pagina_ - 1) * cantidad_;
		
	ELSE
		RETURN QUERY
		SELECT ar.*, false as split, u.nombre, u.apellidop, u.apellidom, u.nombreusuario FROM articulos AS ar
		INNER JOIN usuarios AS u ON u.id = ar.autorid
		WHERE ar.autorid = autorid_
		ORDER BY ar.id DESC
		LIMIT cantidad_
		OFFSET (pagina_ - 1) * cantidad_;
	END IF;
END;
$$;