CREATE OR REPLACE FUNCTION ObtenerCategoriasPaginadas
(
	IN pagina_ int,
	IN cantidad_ int = 10
)
RETURNS SETOF Categorias
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT C.* FROM Categorias AS C
	ORDER BY C.id DESC
	LIMIT cantidad_
	OFFSET (pagina_ - 1) * cantidad_;
END;
$$;