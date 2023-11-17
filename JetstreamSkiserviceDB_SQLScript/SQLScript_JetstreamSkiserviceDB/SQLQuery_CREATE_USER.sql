/* 
	This Query creates a admin login for the JetstreamSkiservice database.
*/
USE master;
GO

CREATE LOGIN [Meier] WITH PASSWORD = 'password';
GO

USE [JetstreamSkiserviceDB];
GO

CREATE USER [Meier] FOR LOGIN [Meier];
GO

GRANT SELECT, INSERT, UPDATE, DELETE TO [Meier];
GO

