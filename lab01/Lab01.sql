USE master
GO

IF exists (SELECT name FROM sys.databases WHERE name = N'Lab01')
	DROP DATABASE Lab01
GO

CREATE DATABASE Lab01
GO

USE Lab01
GO

CREATE TABLE dbo.GroupDB
(
	GroupID int NOT NULL PRIMARY KEY,
	GroupName nchar(30) NOT NULL,
	GenreID int NOT NULL,
	Country nchar(30) NOT NULL
)

CREATE TABLE dbo.GenreDB
(
	GenreID int NOT NULL PRIMARY KEY,
	GenreName nchar(30) NOT NULL
)

CREATE TABLE dbo.AlbumDB
(
	AlbumID int NOT NULL PRIMARY KEY,
	GroupID int NOT NULL,
	AlbumYear int NOT NULL,
	AlbumName nchar(50) NOT NULL
)

CREATE TABLE dbo.TrackDB
(
	TrackID int NOT NULL PRIMARY KEY,
	TrackName nchar(50) NOT NULL,
	AlbumID int NOT NULL
)

BULK INSERT lab01.dbo.GenreDB
FROM 'C:\Users\user\Desktop\Базы Данных\ЛБ1\genres.txt'
WITH (DATAFILETYPE = 'char', FIRSTROW = 1, FIELDTERMINATOR = ',', ROWTERMINATOR = ';')
GO

--SELECT * FROM GenreDB
--GO

BULK INSERT lab01.dbo.GroupDB
FROM 'C:\Users\user\Desktop\Базы Данных\ЛБ1\group.txt'
WITH (DATAFILETYPE = 'char', FIRSTROW = 1, FIELDTERMINATOR = ',', ROWTERMINATOR = ';')
GO

--SELECT * FROM GroupDB
--GO

BULK INSERT lab01.dbo.TrackDB
FROM 'C:\Users\user\Desktop\Базы Данных\ЛБ1\tracks.txt'
WITH (DATAFILETYPE = 'char', FIRSTROW = 1, FIELDTERMINATOR = ',', ROWTERMINATOR = ';')
GO

--SELECT * FROM TackDB
--GO

BULK INSERT lab01.dbo.AlbumDB
FROM 'C:\Users\user\Desktop\Базы Данных\ЛБ1\album.txt'
WITH (DATAFILETYPE = 'char', FIRSTROW = 1, FIELDTERMINATOR = ',', ROWTERMINATOR = ';')
GO

--SELECT * FROM AlbumDB
--GO

CREATE TABLE History 
(
    Id INT IDENTITY PRIMARY KEY,
    GroupID INT NOT NULL,
    Operation NVARCHAR(200) NOT NULL,
    CreateAt DATETIME NOT NULL DEFAULT GETDATE(),
);


alter table AlbumDB
add constraint chkYear CHECK(AlbumYear > 0)

alter table TrackDB
add foreign key (AlbumId) references AlbumDB (AlbumID)

alter table GroupDB
add foreign key (GenreID) references GenreDB (GenreID)

alter table AlbumDB
add foreign key (GroupID) references GroupDB (GroupID)

alter table GenreDB
add foreign key (GenreID) references GroupDB (GenreID)

INSERT INTO AlbumDB(AlbumID, GroupID, AlbumYear, AlbumName) VALUES (1234, 23, -1, 'ERROR')