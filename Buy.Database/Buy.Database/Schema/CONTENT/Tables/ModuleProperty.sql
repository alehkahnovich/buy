CREATE TABLE [CONTENT].[ModuleProperty]
(
	[ModuleId] INT NOT NULL,
	[PropertyId] INT NOT NULL,
	[Value] NVARCHAR(MAX),
	CONSTRAINT FK_ModuleProperty_Property FOREIGN KEY ([PropertyId]) REFERENCES CONTENT.Property ([PropertyId]) ON DELETE CASCADE,
	CONSTRAINT FK_ModuleProperty_Module FOREIGN KEY ([ModuleId]) REFERENCES CONTENT.Module ([ModuleId]) ON DELETE CASCADE
);
GO

ALTER TABLE [CONTENT].[ModuleProperty]
ADD CONSTRAINT PK_ModuleProperty PRIMARY KEY (ModuleId, PropertyId)
GO
