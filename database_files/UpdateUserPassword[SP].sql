CREATE PROCEDURE UpdateUserPassword
(
    userid int,
    newpassword varchar(200),
    rows_affected out int
)
LANGUAGE plpgsql
AS $$
DECLARE salt varchar(150) default gen_salt('bf', 12);
DECLARE passwordhashed varchar(900) default crypt(newpassword, salt);
BEGIN
    UPDATE Usuarios
    SET passwordhash = passwordhashed, passwordsalt = salt
    WHERE id = userid;

    GET DIAGNOSTICS rows_affected = ROW_COUNT;
END;
$$;