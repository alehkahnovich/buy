﻿CREATE TABLE [UPLD].[Request]
(
	[RequestId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UploadKey] UNIQUEIDENTIFIER NOT NULL,
	[RawName] NVARCHAR(255) NOT NULL,
	[CreatedDate] DATETIME2(7) NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedDate] DATETIME2(7) NULL
)
