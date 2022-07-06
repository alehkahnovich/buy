CREATE TABLE [CONTENT].[Control] (
	[ControlId] INT NOT NULL PRIMARY KEY,
	[Type] NVARCHAR(255) NOT NULL,
	CONSTRAINT CH_Control_Type CHECK ([Type] IN (N'text', N'number', N'year', N'date'))
)
