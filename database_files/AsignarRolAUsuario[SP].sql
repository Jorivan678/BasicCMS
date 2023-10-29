CREATE PROCEDURE AsignarRolAUsuario
(
	userId int,
	rolId int,
    rows_affected out int
)
LANGUAGE plpgsql
AS $$
DECLARE rolename varchar(30);
BEGIN
	IF (SELECT COUNT(*) FROM Usuarios_Roles AS UR WHERE UR.usuarioid = userId AND UR.roleid = rolId) = 0
	THEN
		INSERT INTO Usuarios_Roles(usuarioid, roleid)
		VALUES (userId, rolId);
	ELSE
		BEGIN
			SELECT Nombre INTO rolename FROM Roles WHERE Id = rolId;
			RAISE EXCEPTION 'The user alredy has the role %.', rolename;
		END;
	END IF;

	GET DIAGNOSTICS rows_affected = ROW_COUNT;
END;
$$;