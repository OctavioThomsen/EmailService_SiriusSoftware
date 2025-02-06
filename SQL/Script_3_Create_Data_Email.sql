IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'EMAIL') AND type = N'U')
BEGIN
    DELETE FROM EMAIL;

    INSERT INTO EMAIL (Sender, Recipient, Body, SendStatus)
    VALUES ('oti_thomsen98@gmail.com', 'oti_thomsen99@gmail.com', 'Test mail nro 1, ya enviado', 'ENVIADO'),
           ('oti_thomsen98@gmail.com', 'oti_thomsen99@gmail.com', 'Test mail nro 2, pendiente de enviar', 'PENDIENTE');
END
