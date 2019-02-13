CREATE TABLE dbo.TrackDB
(
	TrackID int NOT NULL PRIMARY KEY,
	TrackName nchar(50) NOT NULL,
	AlbumID int NOT NULL
)