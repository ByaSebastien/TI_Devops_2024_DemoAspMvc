﻿CREATE TABLE [dbo].[User_]
(
	[Id] INT PRIMARY KEY IDENTITY,
	[Username] VARCHAR(50) NOT NULL UNIQUE,
	[Email] VARCHAR(150) NOT NULL UNIQUE,
	[Password] VARCHAR(255) NOT NULL,
	CONSTRAINT CK_USER__USERNAME__EMAIL CHECK 
		(TRIM(Username) != '' AND TRIM(Email) != '')
)
