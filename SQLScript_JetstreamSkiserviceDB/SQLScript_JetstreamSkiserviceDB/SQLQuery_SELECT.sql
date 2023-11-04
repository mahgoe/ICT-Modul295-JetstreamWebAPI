/* 
	This Query is to select all Tables manually after build the database with code first in the entity framework 
	-- and select Tables
*/

USE JetstreamSkiserviceDB;
GO

/* 
	These Tables got from OnModelCreating standard values
*/

-- Should be 3 datas (1-Offen, 2-InArbeit, 3-abgeschlossen)
SELECT * FROM dbo.Status;
GO

-- Should be 3 datas (1-Tief, 2-Standard, 3-abgeschlossen)
SELECT * FROM dbo.Priority;
GO

-- Should be 6 datas (1-Kleiner Service, 2-Grosser Service, 3-Rennski Service, 4-Bindungen montieren und einstellen, 5-Fell zuschneiden, 6-Heisswachsen)
SELECT * FROM dbo.Services;
GO

/* 
	All Tables in JetstreamSkiserviceDB
*/
SELECT * FROM dbo.Registrations;
GO

SELECT * FROM dbo.Status;
GO

SELECT * FROM dbo.Priority;
GO

SELECT * FROM dbo.Services;
GO
