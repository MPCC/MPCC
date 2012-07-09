SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
           WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'Token' AND TABLE_TYPE = 'BASE TABLE')
BEGIN
	CREATE TABLE dbo.Token (
		TokenId int IDENTITY (1,1) NOT NULL,
		Token nvarchar(255) NOT NULL,
		Salt nvarchar(255) NOT NULL,
		EnterpriseId int NOT NULL,
		BusinessUnitId int NOT NULL,
		MemberId int NOT NULL,
		ProviderUserKey uniqueidentifier NOT NULL,	
		ApplicationId uniqueidentifier NULL,	
		ExpirationDate datetime NOT NULL,
		IsActive bit NOT NULL CONSTRAINT DF_Token_IsActive DEFAULT((1)),
		IpAddress nvarchar(15) NULL,
		UserAgent nvarchar(255) NULL,
		Key1 nvarchar(255) NULL,
		Key2 nvarchar(255) NULL,
		CreatedDate datetime NOT NULL CONSTRAINT DF_Token_CreatedDate DEFAULT (getdate()),
		ModifiedDate datetime NOT NULL CONSTRAINT DF_Token_ModifiedDate DEFAULT (getdate())
		
		CONSTRAINT PK_Token PRIMARY KEY CLUSTERED (
			[TokenId] ASC)
		)
END
GO

IF NOT EXISTS(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.Token') AND [name] = 'IX_Token_Token_Salt')
BEGIN
	CREATE UNIQUE NONCLUSTERED INDEX IX_Token_Token_Salt ON
		dbo.Token(Token, Salt) INCLUDE (ExpirationDate, IsActive)
	WITH(ONLINE=ON)	
END
GO
