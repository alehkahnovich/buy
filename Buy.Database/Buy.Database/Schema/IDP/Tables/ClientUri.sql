CREATE TABLE [IDP].[ClientUri]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
    [ClientId] UNIQUEIDENTIFIER NOT NULL,
    [Uri] NVARCHAR(MAX) NOT NULL,
    [Type] NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_ClientUri PRIMARY KEY ([Id]),
    CONSTRAINT FK_ClientUri_Client FOREIGN KEY ([ClientId]) REFERENCES IDP.Client (ClientId)
);
GO

CREATE INDEX IX_ClientUri_ClientId ON [IDP].[ClientSecret] ([ClientId]);
GO