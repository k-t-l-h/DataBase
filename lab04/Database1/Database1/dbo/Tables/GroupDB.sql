CREATE TABLE dbo.GroupDB
(
	GroupID int NOT NULL PRIMARY KEY,
	GroupName nchar(30) NOT NULL,
	GenreID int NOT NULL,
	Country nchar(30) NOT NULL
)