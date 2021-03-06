SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
           WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'Member' AND TABLE_TYPE = 'BASE TABLE')
BEGIN
	CREATE TABLE dbo.Member (
		MemberId int IDENTITY (1,1) NOT NULL,
		EnterpriseId int NOT NULL,
		BusinessUnitId int NOT NULL,
		ProviderUserKey uniqueidentifier NOT NULL,
		Username nvarchar(255) NULL,
		FirstName nvarchar(128) NULL,
		MiddleName nvarchar(128) NULL,
		LastName nvarchar(128) NULL,		
		Street nvarchar(128) NULL,
		Apt nvarchar(128) NULL,
		City nvarchar(128) NULL,
		[State] nvarchar(4) NULL,
		Zip int NULL,
		DateOfBirth datetime NULL,
		[Image] nvarchar(255) NULL,
		Email nvarchar(255) NOT NULL,
		FamilyId int NULL,
		StartDate datetime NULL,
		LastVisitDate datetime NULL,
		EndDate datetime NULL,
		IsActive bit NOT NULL CONSTRAINT DF_Member_IsActive DEFAULT((1)),
		CreatedDate datetime NOT NULL CONSTRAINT DF_Member_CreatedDate DEFAULT (getdate()),
		ModifiedDate datetime NOT NULL CONSTRAINT DF_Member_ModifiedDate DEFAULT (getdate())
		
		CONSTRAINT PK_Member PRIMARY KEY CLUSTERED (
			[ProviderUserKey] ASC)
		)
END
GO