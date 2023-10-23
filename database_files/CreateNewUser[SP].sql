CREATE PROCEDURE CreateNewUser
(
	nombre varchar(30),
	apellidop varchar(30),
	apellidom varchar(30),
	nombre_usuario varchar(50),
	passwordnormal varchar(200),
	userid out int
)
LANGUAGE plpgsql
AS $$
DECLARE salt varchar(150) default gen_salt('bf', 12);
DECLARE passwordhashed varchar(900) default crypt(passwordnormal, salt);
BEGIN
	IF (SELECT COUNT(*) FROM Usuarios AS U WHERE U.nombreusuario = nombre_usuario) = 0
	THEN
		INSERT INTO Usuarios(nombre, apellidop, apellidom, nombreusuario, passwordhash, passwordsalt)
		VALUES (nombre, apellidop, apellidom, nombre_usuario, passwordhashed, salt)
		RETURNING ID INTO userid;
	ELSE
		RAISE EXCEPTION 'The user with name % already exists.', nombre_usuario;
	END IF;
END;
$$;