CREATE OR REPLACE FUNCTION ObtenerRolesPorUsuarioId
(
	userid int
)
RETURNS SETOF Roles
LANGUAGE plpgsql
AS $$
BEGIN
	IF userid <= 0 THEN
        RAISE EXCEPTION 'User id cannot be less than or equal to 0.';
    ELSE
        RETURN QUERY
        SELECT R.*
        FROM usuarios_roles AS UR
        INNER JOIN Roles AS R ON UR.roleid = R.id
        WHERE UR.usuarioid = userid;
    END IF;
END;
$$;