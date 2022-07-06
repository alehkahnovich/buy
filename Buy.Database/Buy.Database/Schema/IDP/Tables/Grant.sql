﻿CREATE TABLE [IDP].[Grant]
(
	[Key] NVARCHAR(255) NOT NULL PRIMARY KEY,
	[Type] NVARCHAR(255) NOT NULL,
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[ClientId] UNIQUEIDENTIFIER NOT NULL,
	[CreationTime] DATETIME2(7) NOT NULL,
	[Expiration] DATETIME2(7),
	[Data] NVARCHAR(MAX),

	CONSTRAINT FK_Grant_Client FOREIGN KEY ([ClientId]) REFERENCES IDP.Client (ClientId),
	CONSTRAINT FK_Grant_User FOREIGN KEY ([UserId]) REFERENCES IDP.[User] (UserId)
)
