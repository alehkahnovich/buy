CREATE TABLE [CONTENT].[CategoryProperty]
(
    [CategoryId] INT NOT NULL,
    [PropertyId] INT NOT NULL
);
GO

ALTER TABLE [CONTENT].[CategoryProperty]
ADD CONSTRAINT PK_CategoryProperty PRIMARY KEY (CategoryId, PropertyId)
GO

ALTER TABLE [CONTENT].[CategoryProperty]
ADD CONSTRAINT FK_CategoryProperty_Category FOREIGN KEY ([CategoryId]) REFERENCES CONTENT.Category ([CategoryId]) ON DELETE CASCADE
GO

ALTER TABLE [CONTENT].[CategoryProperty]
ADD CONSTRAINT FK_CategoryProperty_Property FOREIGN KEY ([PropertyId]) REFERENCES CONTENT.Property ([PropertyId]) ON DELETE CASCADE
GO
