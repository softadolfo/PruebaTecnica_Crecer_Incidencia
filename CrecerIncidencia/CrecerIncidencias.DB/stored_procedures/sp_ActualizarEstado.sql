
CREATE OR ALTER   PROCEDURE [dbo].[sp_ActualizarEstado]
    @Id INT,
    @NuevoEstado NVARCHAR(50),
    @Comentario NVARCHAR(1000) = NULL,
    @Usuario NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Incidencias
    SET Estado = @NuevoEstado
    WHERE Id = @Id;

    IF @@ROWCOUNT > 0
    BEGIN
        INSERT INTO IncidenciaBitacora (IncidenciaId, Fecha, Accion, Usuario)
        VALUES (@Id, GetDate(), ISNULL(@Comentario, 'Cambio de estado a ' + @NuevoEstado), @Usuario);
    END

    SELECT @@ROWCOUNT AS Affected;
END
