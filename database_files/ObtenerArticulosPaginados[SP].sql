-- Database: BasicCMS

-- Hora de crear procesos almacenados.
-- Las columnas 'split' solo sirven para indicarle a dapper donde debe empezar el mapeo
-- de otra entidad (tabla).

DO $$ 
BEGIN 
  IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'articulo_info') THEN
    CREATE TYPE Articulo_Info AS (
      id int,
      titulo varchar(50),
      contenido text,
      fecha_pub timestamp with time zone,
	  imagenid int,
      autorid int,
      split_author boolean,
      nombre varchar,
      apellidop varchar,
      apellidom varchar,
      nombreusuario varchar(50),
	  split_image boolean,
	  titulo_imagen varchar(50),
	  ruta varchar(150),
	  alto smallint,
	  ancho smallint,
	  fecha_subida timestamp with time zone,
	  usuarioid int
    );
  END IF;
END $$;

CREATE OR REPLACE FUNCTION ObtenerArticulosPaginados
(
	IN pagina_ int,
	IN cantidad_ int default 10,
	IN autorId_ int default 0,
	IN categorias_ varchar[] = null
)
RETURNS SETOF Articulo_Info
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT DISTINCT ar.*, false as split_author, 
	''::character varying as nombre, ''::character varying as apellidop, ''::character varying as apellidom, u.nombreusuario, 
	false as split_image, i.titulo_imagen, i.ruta, i.alto, i.ancho, i.fecha_subida, i.usuarioid
	FROM articulos AS ar
	LEFT JOIN usuarios AS u ON u.id = ar.autorid
	LEFT JOIN articulos_categorias AS ac ON ar.id = ac.articuloid
	LEFT JOIN categorias AS c ON c.id = ac.categoriaid
	LEFT JOIN imagenes AS i on i.id = ar.imagenid
	WHERE (autorId_ = 0 OR ar.autorid = autorId_)
	AND (categorias_ IS NULL OR UPPER(c.nombre) = ANY(categorias_))
	ORDER BY ar.id DESC
	LIMIT cantidad_
	OFFSET (pagina_ - 1) * cantidad_;
END;
$$;