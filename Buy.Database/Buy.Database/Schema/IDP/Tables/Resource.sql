CREATE TABLE [IDP].[Resource]
(
	[ResourceId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(255) NULL, 
    [Type] NVARCHAR(50) NOT NULL,

	CONSTRAINT CH_Resource_Type CHECK (Type IS NULL OR Type IN (N'Api', N'Identity'))
);
GO

CREATE INDEX IX_Resource_Name ON [IDP].[Resource] (Name);
GO
