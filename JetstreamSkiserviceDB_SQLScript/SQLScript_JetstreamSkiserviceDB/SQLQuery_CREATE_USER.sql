/* 
	This Query creates a admin login for the JetstreamSkiservice database.
*/

-- Change master to create user
USE master;
GO

-- Give the login a role
ALTER SERVER ROLE sysadmin ADD MEMBER JetstreamSkiservice_admin;
GO

-- Create new login with password
CREATE LOGIN JetstreamSkiservice_admin WITH PASSWORD = 'password';
GO

-- Change to the specific database
USE JetstreamSkiserviceDB;
GO

-- Create a new user with the login
CREATE USER JetstreamSkiservice_admin FOR LOGIN JetstreamSkiservice_admin;
GO

-- Add user to the role
ALTER ROLE db_owner ADD MEMBER JetstreamSkiservice_admin;
GO

-- Give permissions to the role
GRANT INSERT, SELECT, UPDATE, DELETE ON OBJECT::dbo.Employees TO DB_employee;
GRANT INSERT, SELECT, UPDATE, DELETE ON OBJECT::dbo.Priority TO DB_employee;
GRANT INSERT, SELECT, UPDATE, DELETE ON OBJECT::dbo.Registrations TO DB_employee;
GRANT INSERT, SELECT, UPDATE, DELETE ON OBJECT::dbo.Services TO DB_employee;
GRANT INSERT, SELECT, UPDATE, DELETE ON OBJECT::dbo.Status TO DB_employee;

-- Assigning the user to the role
EXEC sp_addrolemember 'DB_employee', 'JetstreamSkiservice_admin';
