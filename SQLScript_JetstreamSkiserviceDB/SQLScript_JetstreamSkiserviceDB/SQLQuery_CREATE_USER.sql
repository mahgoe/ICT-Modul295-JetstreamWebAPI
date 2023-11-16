/* 
	This Query creates a admin login for the JetstreamSkiservice database.
*/

-- Wechseln zu master-Datenbank, um den Login zu erstellen
USE master;
GO

-- Erstellen eines neuen Logins
CREATE LOGIN JetstreamSkiservice_admin WITH PASSWORD = 'password';
GO

-- Wechseln zu Ihrer spezifischen Datenbank
USE JetstreamSkiserviceDB;
GO

-- Erstellen eines Benutzers, der mit dem neuen Login verknüpft ist
CREATE USER JetstreamSkiservice_admin FOR LOGIN JetstreamSkiservice_admin;
GO

-- Erstellen der Rolle DB_employee
CREATE ROLE DB_employee;
GO

-- Gewähren von Berechtigungen
GRANT INSERT, SELECT, UPDATE, DELETE ON OBJECT::dbo.Employees TO DB_employee;
GRANT INSERT, SELECT, UPDATE, DELETE ON OBJECT::dbo.Priority TO DB_employee;
GRANT INSERT, SELECT, UPDATE, DELETE ON OBJECT::dbo.Registrations TO DB_employee;
GRANT INSERT, SELECT, UPDATE, DELETE ON OBJECT::dbo.Services TO DB_employee;
GRANT INSERT, SELECT, UPDATE, DELETE ON OBJECT::dbo.Status TO DB_employee;

-- Zuordnen des Benutzers zur Rolle
EXEC sp_addrolemember 'DB_employee', 'JetstreamSkiservice_admin';
