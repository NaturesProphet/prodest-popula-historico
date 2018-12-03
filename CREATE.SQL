CREATE TABLE tempdb.dbo.veiculo (
	dataregistro datetime2(7) DEFAULT (getdate()) NOT NULL,
	atualizadoem datetime2(7) DEFAULT (getdate()) NOT NULL,
	uuid UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID(),
	dados TEXT NOT NULL,
	CONSTRAINT PK_uuid PRIMARY KEY (uuid)
) go;
