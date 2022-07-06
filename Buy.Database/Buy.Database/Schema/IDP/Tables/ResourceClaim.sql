CREATE TABLE [IDP].[ResourceClaim]
(
	[ResourceId] INT NOT NULL,
	[ClaimId] INT NOT NULL,

	CONSTRAINT PK_ResourceClaim PRIMARY KEY ([ResourceId], [ClaimId]),
	CONSTRAINT FK_ResourceClaim_Resource FOREIGN KEY ([ResourceId]) REFERENCES IDP.Resource (ResourceId),
	CONSTRAINT FK_ResourceClaim_Claim FOREIGN KEY (ClaimId) REFERENCES IDP.Claim (ClaimId)
)
