
USE master
GO

IF exists (SELECT name FROM sys.databases WHERE name = N'Lab01')
	DROP DATABASE Lab01
GO

CREATE DATABASE Lab01
GO

USE Lab01
GO

BULK INSERT lab01.dbo.GenreDB
FROM 'C:\Users\user\Desktop\ءàçû ؤàييûُ\ثء1\genres.txt'
WITH (DATAFILETYPE = 'char', FIRSTROW = 1, FIELDTERMINATOR = ',', ROWTERMINATOR = ';')
GO

--SELECT * FROM GenreDB
--GO

BULK INSERT lab01.dbo.GroupDB
FROM 'C:\Users\user\Desktop\ءàçû ؤàييûُ\ثء1\group.txt'
WITH (DATAFILETYPE = 'char', FIRSTROW = 1, FIELDTERMINATOR = ',', ROWTERMINATOR = ';')
GO

--SELECT * FROM GroupDB
--GO

BULK INSERT lab01.dbo.TrackDB
FROM 'C:\Users\user\Desktop\ءàçû ؤàييûُ\ثء1\tracks.txt'
WITH (DATAFILETYPE = 'char', FIRSTROW = 1, FIELDTERMINATOR = ',', ROWTERMINATOR = ';')
GO

--SELECT * FROM TackDB
--GO

BULK INSERT lab01.dbo.AlbumDB
FROM 'C:\Users\user\Desktop\ءàçû ؤàييûُ\ثء1\album.txt'
WITH (DATAFILETYPE = 'char', FIRSTROW = 1, FIELDTERMINATOR = ',', ROWTERMINATOR = ';')
GO

INSERT INTO AlbumDB(AlbumID, GroupID, AlbumYear, AlbumName) VALUES (1234, 23, -1, 'ERROR')
GO
