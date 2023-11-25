-- Database: BasicCMS

-- Hora de crear procesos almacenados.

CREATE OR REPLACE FUNCTION ObtenerCantidadArticulos
(
	IN autorId_ int default 0,
	IN categorias_ varchar[] = null
)
RETURNS TABLE (
	conteo bigint
)
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT DISTINCT COUNT(*) AS conteo
	FROM articulos AS ar
	LEFT JOIN usuarios AS u ON u.id = ar.autorid
	LEFT JOIN articulos_categorias AS ac ON ar.id = ac.articuloid
	LEFT JOIN categorias AS c ON c.id = ac.categoriaid
	WHERE (autorId_ = 0 OR ar.autorid = autorId_)
	AND (categorias_ IS NULL OR UPPER(c.nombre) = ANY(categorias_));
END;
$$;