﻿CREATE PROCEDURE [dbo].[DisplayUsers]
AS
BEGIN
	SELECT [UserID],[FullName],[Password],[PhoneNumber],[Email],[AccountType].[AccountTypeID],[AccountType].[AccountName]
	FROM [Users]
	INNER JOIN AccountType ON [Users].AccountTypeID = AccountType.AccountTypeID;
END
