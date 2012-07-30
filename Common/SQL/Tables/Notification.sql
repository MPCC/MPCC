SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
           WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'Notification' AND TABLE_TYPE = 'BASE TABLE')
BEGIN
	CREATE TABLE dbo.[Notification] (
		NotificationId int IDENTITY (1,1) NOT NULL,
		EnterpriseId int NOT NULL,
		BusinessUnitId int NOT NULL,
		ToMemberId int NOT NULL,
		FromMemberId int NOT NULL,
		FromUsername nvarchar(128) NOT NULL,
	    [Category] nvarchar(35) NOT NULL,
	    [Subject] nvarchar(60) NULL,
		[Message] nvarchar(255) NULL,		
		HasRead bit NOT NULL CONSTRAINT DF_Notification_HasRead DEFAULT((0)),
		IsActive bit NOT NULL CONSTRAINT DF_Notification_IsActive DEFAULT((1)),
		CreatedDate datetime NOT NULL CONSTRAINT DF_Notification_CreatedDate DEFAULT (getdate()),
		ModifiedDate datetime NOT NULL CONSTRAINT DF_Notification_ModifiedDate DEFAULT (getdate())
		
		CONSTRAINT PK_Notification PRIMARY KEY CLUSTERED (
			[NotificationId] ASC)
		)
END
GO