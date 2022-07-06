CREATE TABLE [CONTENT].[Property]
(
	[PropertyId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Type] NVARCHAR(255) NOT NULL,
	[ControlId] INT,
	[BehaviorId] INT,
	[ParentId] INT,
	[Name] NVARCHAR(255),
	[IsFacet] BIT NOT NULL DEFAULT 0,
	[CreatedDate] DATETIME2(7) NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedDate] DATETIME2(7) NULL,
    [SortOrder] INT NOT NULL DEFAULT 0,
	CONSTRAINT FK_Property_Property FOREIGN KEY ([ParentId]) REFERENCES CONTENT.Property ([PropertyId]),
	CONSTRAINT FK_Property_Control FOREIGN KEY ([ControlId]) REFERENCES CONTENT.Control ([ControlId]),
	CONSTRAINT FK_Property_Behavior FOREIGN KEY ([BehaviorId]) REFERENCES CONTENT.Behavior ([BehaviorId]),
	CONSTRAINT CH_Property_Type CHECK ([Type] IN (N'string', N'number', N'date', N'numberrange'))
);
GO

CREATE INDEX IX_Property_ParentId ON [CONTENT].[Property] ([ParentId]);
GO
