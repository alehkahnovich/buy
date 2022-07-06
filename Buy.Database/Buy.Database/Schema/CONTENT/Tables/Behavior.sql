CREATE TABLE [CONTENT].[Behavior]
(
	[BehaviorId] INT NOT NULL PRIMARY KEY,
	[Type] NVARCHAR(255) NOT NULL,
	CONSTRAINT CH_Behavior_Type CHECK ([Type] IN (N'single', N'multiple'))
)
