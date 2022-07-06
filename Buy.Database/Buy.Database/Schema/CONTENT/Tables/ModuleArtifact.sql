CREATE TABLE [CONTENT].[ModuleArtifact]
(
	[ModuleId] INT NOT NULL,
	[ArtifactId] INT NOT NULL,
	CONSTRAINT FK_ModuleArtifact_Module FOREIGN KEY ([ModuleId]) REFERENCES CONTENT.Module ([ModuleId]) ON DELETE CASCADE,
	CONSTRAINT FK_ModuleArtifact_Artifact FOREIGN KEY ([ArtifactId]) REFERENCES CONTENT.Artifact ([ArtifactId]) ON DELETE CASCADE
);
GO

ALTER TABLE [CONTENT].[ModuleArtifact]
ADD CONSTRAINT PK_ModuleArtifact PRIMARY KEY (ModuleId, ArtifactId)
GO