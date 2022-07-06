﻿CREATE TABLE [IDP].[Client]
(
	[ClientId] UNIQUEIDENTIFIER NOT NULL, 
    [Name] NVARCHAR(255) NOT NULL,
	ReuseRefresh BIT NOT NULL,
	AllowOfflineAccess BIT NOT NULL,

	CONSTRAINT PK_Client PRIMARY KEY ([ClientId])
);
GO
