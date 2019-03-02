USE Lab01
GO

--Режимы XML
--1
SELECT AlbumID, rtrim(ltrim(AlbumName)) as AlbumName FROM AlbumDB
FOR XML AUTO

--2
SELECT * FROM GroupDB
FOR XML RAW, ROOT('GroupName')

--3
SELECT 1 as Tag,
NULL as Parent, 
GroupName as [Group!1!Name],
NULL as [Album!2!Name] FROM GroupDB JOIN AlbumDB ON AlbumDB.GroupID = GroupDB.GroupID
UNION
SELECT 2 as Tag, 
	1 as Parent,
GroupName as [Group!1!Name], 
AlbumName  as [Album!2!Name] 
FROM GroupDB JOIN AlbumDB ON AlbumDB.GroupID = GroupDB.GroupID
ORDER BY [Group!1!Name], [Album!2!Name] 
FOR XML EXPLICIT, ROOT('GroupName')
GO

