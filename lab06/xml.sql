
SELECT 1 as Tag, 
	NULL as Parent, 
	GroupDB.GroupID as [Group!1!Id!Id], 
	LTRIM(RTRIM(GroupName)) as [Group!1!Name!element],
	LTRIM(RTRIM(Country)) as [Group!1!Country!element]
FROM GroupDB
ORDER BY [Group!1!Id!Id] 
FOR XML EXPLICIT, ROOT('Groups') 
GO