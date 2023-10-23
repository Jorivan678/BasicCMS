CREATE OR REPLACE FUNCTION ObtenerCategoriasPorArticuloId
(
    artid int
)
RETURNS SETOF Categorias
LANGUAGE plpgsql
AS $$
BEGIN
    IF artid <= 0 THEN
        RAISE EXCEPTION 'El id del artículo no puede ser 0 o menor.';
    ELSE
        RETURN QUERY
        SELECT C.*
        FROM articulos_categorias AS AC
        INNER JOIN Categorias AS C ON AC.categoriaid = C.id
        WHERE AC.articuloid = artid;
    END IF;
END;
$$;