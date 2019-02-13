CREATE TABLE dbo.AlbumDB
(
	AlbumID int NOT NULL PRIMARY KEY,
	GroupID int NOT NULL,
	AlbumYear int NOT NULL,
	AlbumName nchar(50) NOT NULL
)
GO
alter table AlbumDB
add constraint chkYear CHECK(AlbumYear > 0)