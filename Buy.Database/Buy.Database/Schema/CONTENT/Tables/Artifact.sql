CREATE TABLE [CONTENT].[Artifact]
(
	[ArtifactId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[RequestId] INT,
	[UploadKey] UNIQUEIDENTIFIER NOT NULL,
	[Type] NVARCHAR(50) NOT NULL,
	[CreatedDate] DATETIME2(7) NOT NULL DEFAULT GETUTCDATE(),
	[SortOrder] INT NOT NULL DEFAULT 0,
	CONSTRAINT FK_Artifact_Request FOREIGN KEY ([RequestId]) REFERENCES UPLD.Request ([RequestId]),
	CONSTRAINT CH_Artifact_Type CHECK ([Type] IN (N'thumbnail', N'preview'))
);
GO

CREATE INDEX IX_Artifact_RequestId ON [CONTENT].[Artifact] ([RequestId]);
GO

CREATE INDEX IX_Artifact_Type ON [CONTENT].[Artifact] ([Type]);
GO

