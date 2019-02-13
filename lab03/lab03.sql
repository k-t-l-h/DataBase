USE master
GO

USE Lab01
GO


----Скалярная функция

DROP FUNCTION  GetAlbum
GO

CREATE FUNCTION GetAlbum(@groupid int)
RETURNS INT AS
BEGIN
	RETURN
	(
	SELECT COUNT(*) As AlbumNum 
	FROM AlbumDB
	WHERE AlbumDB.GroupID = @groupid 
	GROUP BY GroupID
	)
END
GO 

SELECT dbo.GetAlbum(GroupDB.GroupID) As [Num of Albums] FROM GroupDB
 
SELECT * FROM AlbumDB WHERE AlbumDB.GroupID = 83
GO

----Подставляемая табличная функция

DROP FUNCTION getLazyGroups
GO

CREATE FUNCTION getLazyGroups()
RETURNS TABLE
AS RETURN
(
	SELECT GroupName, GroupDB.GroupID, Country
    FROM GroupDB LEFT JOIN AlbumDB ON GroupDB.GroupID = AlbumDB.GroupID
	WHERE 
	(
	SELECT COUNT(*) As AlbumNum 
 	FROM AlbumDB
    WHERE AlbumDB.GroupID = GroupDB.GroupID 
	GROUP BY GroupID
	) IS NULL
)


SELECT * FROM dbo.getLazyGroups() ORDER BY GroupID


----Многооператорная табличная функция

DROP  FUNCTION FullAlbumInfo
GO

CREATE FUNCTION FullAlbumInfo(@AlbumID int)
RETURNS @info TABLE
(AlbumID int, AlbumYear int, TrackID int, TrackName nchar(50))
AS
BEGIN
	INSERT @info
	SELECT AlbumDB.AlbumID, AlbumDB.AlbumYear, TrackDB.TrackID, TrackDB.TrackName
	FROM AlbumDB JOIN TrackDB ON AlbumDB.AlbumID = TrackDB.AlbumID
	WHERE AlbumDB.AlbumID = @AlbumID;

	RETURN
END

SELECT * FROM FullAlbumInfo(6)


--рекурсивная функция

DROP FUNCTION recurs
GO

CREATE FUNCTION recurs()
RETURNS TABLE
RETURN
(
WITH REC(Id, Genre)
AS
	(
		SELECT GenreName, 1 as GenreID
		FROM dbo.GenreDB 
		UNION
		SELECT GenreName, GenreID + 1
		FROM dbo.GenreDB 
	)
SELECT Id FROM Rec
WHERE Id is NOT NULL
Group by Id
)

SELECT * from dbo.recurs() ORDER BY Id






----триггер AFTER
 

DROP TRIGGER aft 
GO 
CREATE TRIGGER aft 
ON dbo.GroupDB
AFTER INSERT
AS BEGIN
PRINT 'Trigger is working'
END
GO

INSERT INTO GroupDB(GroupID, GroupName, GenreID, Country) VALUES (1010, 'Wow', 35, 'Russia')
GO

----триггер INSTEAD OF
DROP TRIGGER inst_of
GO

CREATE TRIGGER inst_of
ON dbo.GenreDB
INSTEAD OF INSERT
AS BEGIN
	INSERT INTO GenreDB
	SELECT GenreID, GenreName + ' Some text'
	FROM inserted
END

INSERT INTO GenreDB(GenreID, GenreName) VALUES (100, 'Valz')

----простая хранимая процедура

DROP  Procedure ResetGenreNum

CREATE Procedure ResetGenreNum(@p INT)
	AS UPDATE GenreDB
	SET GenreID = GenreID + @p

EXECUTE ResetGenreNum 1;
SELECT * FROM GenreDB

EXECUTE ResetGenreNum -1;
SELECT * FROM GenreDB

----рекурсивная хранимая процедура
DROP PROCEDURE recurs_2
GO

CREATE PROCEDURE recurs_2
AS
BEGIN
	WITH REC(Id, Genre)
	AS
		(
			SELECT GenreName, 1 as GenreID
			FROM dbo.GenreDB 
			UNION
			SELECT GenreName, GenreID + 1
			FROM dbo.GenreDB 
		)
	SELECT Id FROM Rec
	WHERE Id is NOT NULL
	Group by Id
END

EXECUTE recurs_2


----процедура с курсором
DROP Procedure GenreGroup
GO

CREATE Procedure GenreGroup
AS
BEGIN
	DECLARE @id int
	DECLARE curs CURSOR FOR
		SELECT GenreDB.GenreID FROM GenreDB

	OPEN curs

	FETCH NEXT FROM curs INTO @id
	WHILE @@FETCH_STATUS = 0
		BEGIN
			SELECT * FROM AlbumDB WHERE AlbumDB.GroupID = @id
			FETCH NEXT FROM curs INTO @id
		END

	CLOSE curs
	DEALLOCATE curs
END
GO

EXECUTE GenreGroup;



----процедура с метаданным
DROP PROCEDURE sys_proc
GO

CREATE PROCEDURE sys_proc
AS
BEGIN
	SELECT * FROM sys.procedures
END
GO

EXECUTE sys_proc

--quest 1

DROP TRIGGER infotable ON DATABASE
GO

CREATE TRIGGER infotable
ON DATABASE 
FOR CREATE_TABLE
AS
	PRINT 'Table created'
	SELECT * FROM sys.tables 
	WHERE tables.name =	 EVENTDATA().value('(/EVENT_INSTANCE/ObjectName)[1]','nvarchar(max)');

GO


DROP TABLE test

CREATE TABLE test(id int)
