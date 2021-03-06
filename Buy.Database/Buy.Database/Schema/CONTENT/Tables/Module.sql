CREATE TABLE [CONTENT].[Module]
(
	[ModuleId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[CategoryId] INT NOT NULL,
	[Name] NVARCHAR(255),
	[Price] DECIMAL(19,4) DEFAULT 0,
	[Description] NVARCHAR(1000),
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate] DATETIME2(7) NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedDate] DATETIME2(7) NULL

	CONSTRAINT FK_Module_User FOREIGN KEY ([CreatedBy]) REFERENCES IDP.[User] (UserId),
	CONSTRAINT FK_Module_Category FOREIGN KEY ([CategoryId]) REFERENCES CONTENT.Category ([CategoryId])
)
