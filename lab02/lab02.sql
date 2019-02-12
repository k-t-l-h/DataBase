USE master
GO

USE Lab01
GO

----запрос 1
SELECT GroupName, GenreID, Country
FROM dbo.GroupDB
WHERE GenreID < 20
ORDER BY GroupName
GO

----запрос 2
SELECT AlbumName, AlbumYear 
FROM dbo.AlbumDB
WHERE AlbumYear BETWEEN '1990' AND '2000'
ORDER BY AlbumYear
GO

----запрос 3
SELECT GenreID, GenreName
FROM dbo.GenreDB
WHERE GenreName LIKE '%rock%'
ORDER BY GenreID
GO

----запрос 4
SELECT TrackID, TrackName 
FROM TrackDB
WHERE AlbumID IN
	(
	SELECT AlbumID
	FROM AlbumDB
	WHERE AlbumYear < '1990'
	)
GO

--запрос 5
SELECT GenreID, GenreName
FROM dbo.GenreDB
WHERE EXISTS
(
	SELECT GenreDB.GenreID
	FROM GenreDB RIGHT JOIN GroupDB 
	ON GenreDB.GenreID = GroupDB.GenreID
	WHERE GroupID IS NULL
)
GO

--запрос 6
SELECT *
FROM AlbumDB JOIN  GroupDB 
ON  AlbumDB.GroupID = GroupDB.GroupID
WHERE AlbumYear > ALL
(
	SELECT AlbumYear 
	FROM AlbumDB JOIN  GroupDB 
	ON  AlbumDB.GroupID = GroupDB.GroupID
	WHERE GenreID = 3
)

--запрос 7
--SELECT COUNT(GroupDB.GroupID) AS 'Count'
--FROM AlbumDB JOIN  GroupDB 
--ON  AlbumDB.GroupID = GroupDB.GroupID

--запрос 8
SELECT DISTINCT GroupDB.GroupID, 
(
SELECT COUNT(AlbumID)
FROM AlbumDB
WHERE AlbumDB.GroupID = GroupDB.GroupID 
) AS AlbumNum
FROM AlbumDB JOIN  GroupDB 
ON  AlbumDB.GroupID = GroupDB.GroupID 
ORDER BY GroupID

--запрос 9
--SELECT DISTINCT GenreDB.GenreName,
--		CASE GroupDB.GroupName
--		WHEN ISNULL(GroupDB.GroupName, 0) THEN 'Yes'
--		ELSE 'No'
--		END as GroupsExists
--	FROM GenreDB LEFT JOIN GroupDB 
--	ON GenreDB.GenreID = GroupDB.GenreID
--ORDER BY GenreDB.GenreName

--запрос 10
SELECT AlbumName, 
CASE 
	WHEN AlbumYear < 1980 THEN '70th'
	WHEN AlbumYear < 1990 THEN '80th'
	ELSE '90th'
END AS Years
FROM dbo.AlbumDB
ORDER BY AlbumYear

--запрос 11
--SELECT DISTINCT GenreDB.GenreID, GenreName INTO #UsedGenres
--FROM GenreDB LEFT JOIN GroupDB 
--ON GenreDB.GenreID = GroupDB.GenreID
--WHERE GroupID IS NOT NULL
--ORDER BY GenreID

--DROP TABLE #UsedGenres

--запрос 12
SELECT *
FROM GenreDB JOIN 
(SELECT GenreID, GroupID, GroupName  FROM GroupDB ) AS GB
 ON GenreDB.GenreID = GB.GenreID
 JOIN
(SELECT GroupID, AlbumID, AlbumName FROM AlbumDB) AS Alb
ON  Alb.GroupID = GB.GroupID
ORDER BY GenreDB.GenreID

--запрос 13

SELECT *
FROM GroupDB JOIN AlbumDB ON GroupDB.GroupID = AlbumDB.GroupID
WHERE AlbumID = 
(
	SELECT MIN(AlbumDB.AlbumID)
	FROM AlbumDB
	WHERE AlbumYear = 
	(
		SELECT MIN(AlbumYear) 
		FROM AlbumDB JOIN GroupDB ON AlbumDB.GroupID = GroupDB.GroupID
		WHERE GenreID = 
		(
			SELECT MIN(GenreID)
			FROM GenreDB 
			WHERE GenreName LIKE '%rock%'

		)
	)
)


--запрос 14
--SELECT GroupName, MIN(AlbumYear) AS FirstAlbum, MAX(AlbumYear) AS LastAlbum
--FROM AlbumDB JOIN GroupDB ON AlbumDB.GroupID = GroupDB.GroupID
--GROUP BY GroupName

--запрос 15
SELECT GroupName, MIN(AlbumYear) AS FirstAlbum, MAX(AlbumYear) AS LastAlbum
FROM AlbumDB JOIN GroupDB ON AlbumDB.GroupID = GroupDB.GroupID
GROUP BY GroupName
HAVING MIN(AlbumYear) > 1985

--запрос 16
--INSERT AlbumDB(AlbumID,GroupID, AlbumYear,	AlbumName) 
--VALUES (1002, 17, 1991, 'Wow Insert Magick')

--запрос 17

CREATE TABLE IndieGroups ( 
GroupName nchar(50), 
Country nchar(50)
)

INSERT INTO IndieGroups (GroupName, Country)
SELECT GroupDB.GroupName, GroupDB.Country
FROM GroupDB
WHERE GroupDB.GenreID = 
(
SELECT GenreID FROM GenreDB WHERE GenreName LIKE '%indie%'
)


--SELECT * FROM IndieGroups

--запрос 18

--UPDATE GroupDB
--SET GenreID = GenreID + 1
--WHERE GenreID > 2

--запрос 19
--UPDATE AlbumDB
--SET AlbumYear = 
--(
--	SELECT AVG(AlbumYear) 
--	FROM AlbumDB
--	WHERE GroupID = 18
--)
--WHERE GroupID = 18

--запрос 20
DROP TABLE #TMP1

SELECT DISTINCT GenreDB.GenreID, GenreName, GroupID, GroupName INTO #TMP1
FROM GenreDB LEFT JOIN GroupDB 
ON GenreDB.GenreID = GroupDB.GenreID
ORDER BY GenreDB.GenreID


DELETE #TMP1
WHERE GroupID IS NULL

--SELECT * FROM #TMP1

--запрос 21
DELETE FROM #TMP1
WHERE GenreID NOT IN 
	(
	SELECT GenreDB.GenreID
	FROM GenreDB LEFT JOIN GroupDB 
	ON GenreDB.GenreID = GroupDB.GenreID
	WHERE GroupID IS NULL 
	)


--запрос 22
--WITH CTE(AlbumID, AlbumName, AlbumYear)
--AS
--(
--	SELECT AlbumID, AlbumName, AlbumYear 
--	FROM AlbumDB
--)
--SELECT * FROM CTE

--запрос 23

WITH Recursion(GroupName, GenreID, Country, GroupID)
AS 
(
SELECT GroupName, GenreID, Country, 1 AS GroupID
FROM GroupDB
UNION 
SELECT GroupName, GenreID, Country, GroupID + 1
FROM GroupDB
)
SELECT GroupName, GenreID, Country, GroupID
FROM GroupDB

--запрос 24
SELECT GroupDB.GroupName, MIN(AlbumDB.AlbumYear) OVER(partition by GroupDB.GroupID ) AS FirstYear,
	MAX(AlbumDB.AlbumYear) OVER(partition by GroupDB.GroupID ) AS LastYear
FROM GroupDB JOIN AlbumDB ON GroupDB.GroupID = AlbumDB.GroupID
ORDER BY GroupDB.GroupID



--запрос 25
SELECT * FROM
(
SELECT Row_number() OVER(PARTITION BY GenreName ORDER BY GenreName)  AS Num, GenreName FROM
GenreDB JOIN TrackDB ON GenreDB.GenreID = TrackDB.AlbumID) AS TMP3
WHERE Num = 1

