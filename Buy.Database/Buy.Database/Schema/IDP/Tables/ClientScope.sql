CREATE TABLE [IDP].[ClientScope]
(
	[ClientId] UNIQUEIDENTIFIER NOT NULL,
	[ResourceId] INT NOT NULL

	CONSTRAINT PK_ClientScope PRIMARY KEY ([ClientId], [ResourceId]),
	CONSTRAINT FK_ClientScope_Client FOREIGN KEY ([ClientId]) REFERENCES IDP.Client (ClientId),
	CONSTRAINT FK_ClientScope_Resource FOREIGN KEY ([ResourceId]) REFERENCES IDP.Resource (ResourceId)
);
GO
