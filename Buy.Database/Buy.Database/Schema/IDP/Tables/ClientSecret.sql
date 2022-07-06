CREATE TABLE [IDP].[ClientSecret]
(
	[SecretId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ClientId] UNIQUEIDENTIFIER NOT NULL,
	[Value] NVARCHAR(255) NOT NULL,
    [Expiration] DATETIME2(7),

	CONSTRAINT FK_ClientSecret_Client FOREIGN KEY ([ClientId]) REFERENCES IDP.Client (ClientId)
);
GO

CREATE INDEX IX_ClientSecret_ClientId ON [IDP].[ClientSecret] ([ClientId]);
GO
