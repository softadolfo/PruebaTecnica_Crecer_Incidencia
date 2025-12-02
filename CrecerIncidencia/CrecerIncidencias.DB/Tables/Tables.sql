
CREATE TABLE Incidencias
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Titulo NVARCHAR(200) NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    Categoria NVARCHAR(100) NOT NULL,
    Severidad NVARCHAR(50) NOT NULL,
    Estado NVARCHAR(50) NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    BitacoraInicial NVARCHAR(500) NULL
);

CREATE TABLE IncidenciaBitacora
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IncidenciaId INT NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Accion NVARCHAR(1000) NOT NULL,
    Usuario NVARCHAR(200) NULL,
    CONSTRAINT FK_IncidenciaBitacora_Incidencia FOREIGN KEY (IncidenciaId)
        REFERENCES Incidencias(Id) ON DELETE CASCADE
);
