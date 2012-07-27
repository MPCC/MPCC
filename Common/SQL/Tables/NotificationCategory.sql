SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
           WHERE TABLE_SCHEMA = N'dbo' AND TABLE_NAME = N'NotificationCategory' AND TABLE_TYPE = 'BASE TABLE')
BEGIN
	CREATE TABLE dbo.NotificationCategory (
		NotificationCategoryId int IDENTITY (1,1) NOT NULL,
		[Category] nvarchar(35) NOT NULL,		
		CreatedDate datetime NOT NULL CONSTRAINT DF_NotificationCategory_CreatedDate DEFAULT (getdate()),
		ModifiedDate datetime NOT NULL CONSTRAINT DF_NotificationCategory_ModifiedDate DEFAULT (getdate())
		
		CONSTRAINT PK_NotificationCategory PRIMARY KEY CLUSTERED (
			[Category] ASC)
		)
END
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
GO

CREATE TABLE #Temp (Category nvarchar(35))
INSERT INTO #Temp(Category)
SELECT 'Message' UNION ALL
SELECT 'System' UNION ALL
SELECT 'Request' UNION ALL
SELECT 'Alert'

INSERT INTO dbo.NotificationCategory (Category)
SELECT Category FROM #Temp
WHERE Category NOT IN (SELECT Category FROM dbo.NotificationCategory)

GO