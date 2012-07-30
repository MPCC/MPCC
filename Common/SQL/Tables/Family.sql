SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
           WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'Family' AND TABLE_TYPE = 'BASE TABLE')
BEGIN
	CREATE TABLE dbo.Family (
		FamilyId int IDENTITY (1,1) NOT NULL,				
		Name nvarchar(128) NULL,		
		[Image] nvarchar(255) NULL,
		EnterpriseId int NOT NULL,
		BusinessUnitId int NOT NULL,		
		CreatedBy int NOT NULL,
		IsActive bit NOT NULL CONSTRAINT DF_Family_IsActive DEFAULT((1)),
		CreatedDate datetime NOT NULL CONSTRAINT DF_Family_CreatedDate DEFAULT (getdate()),
		ModifiedDate datetime NOT NULL CONSTRAINT DF_Family_ModifiedDate DEFAULT (getdate())
		
		CONSTRAINT PK_Family PRIMARY KEY CLUSTERED (
			[FamilyId] ASC)
		)
END
GO