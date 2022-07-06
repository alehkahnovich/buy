CREATE VIEW [IDP].[vwClient] AS 
SELECT 
	c.ClientId, 
	c.Name, 
	c.ReuseRefresh, 
	c.AllowOfflineAccess,
	r.Name as ResourceName,
	r.Description as ResourceDescription
FROM IDP.Client as c
INNER JOIN IDP.ClientScope as cs on cs.ClientId = c.ClientId
INNER JOIN IDP.Resource as r on r.ResourceId = cs.ResourceId
