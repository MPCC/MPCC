declare @salt nvarchar(128)
declare @password varbinary(256)
declare @input varbinary(512)
declare @hash varchar(64)

-- Change these values (@salt should be Base64 encoded)
set @salt = N'eyhKDP858wdrYHbBmFoQ6DXzFE1FB+RDP4ULrpoZXt6f'
set @password = convert(varbinary(256),N'iulian')

set @input = hashbytes('sha1',cast('' as  xml).value('xs:base64Binary(sql:variable(''@salt''))','varbinary(256)') + @password)
set @hash = cast('' as xml).value('xs:base64Binary(xs:hexBinary(sql:variable(''@input'')))','varchar(64)')

-- @hash now contains a suitable password hash
-- Now create the user using the value of @salt as the salt, and the value of @hash as the password (with the @PasswordFormat set to 1)

DECLARE @return_value int, 
  @UserId uniqueidentifier 

EXEC @return_value = [dbo].[aspnet_Membership_CreateUser] 
  @ApplicationName = N'MPCC Connect', 
  @UserName = N'Iulian', 
  @Password = @hash, 
  @PasswordSalt = @salt, 
  @Email = N'iulian_mihai@yahoo.com', 
  @PasswordQuestion = N'Whats your favorite color', 
  @PasswordAnswer = N'white', 
  @IsApproved = 1, 
  @CurrentTimeUtc = '2012-08-08', 
  @CreateDate = '2012-08-08', 
  @UniqueEmail = 1, 
  @PasswordFormat = 1, 
  @UserId = @UserId OUTPUT 

SELECT @UserId as N'@UserId' 

SELECT 'Return Value' = @return_value 