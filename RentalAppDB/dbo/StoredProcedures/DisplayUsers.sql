CREATE PROCEDURE [dbo].[DisplayUsers]
AS
BEGIN
	SELECT [UserID],[UserName],[Password],[PhoneNumber],[Email],[AccountType].[AccountTypeID],[AccountType].[AccountName]
	FROM [Users]
	INNER JOIN AccountType ON [Users].AccountTypeID = AccountType.AccountTypeID;
END
