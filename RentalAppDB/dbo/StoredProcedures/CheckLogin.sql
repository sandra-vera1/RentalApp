CREATE PROCEDURE [dbo].[CheckLogin]
	@UserEmail VARCHAR(50),
	@Password VARCHAR(30)
AS
BEGIN
	SELECT U.UserID, U.FullName, U.PhoneNumber, U.Email, U.AccountTypeId, AT.AccountName
	FROM [Users] AS U
	INNER JOIN [AccountType] AS AT ON U.AccountTypeID = AT.AccountTypeID
	WHERE U.Email = @UserEmail AND U.[Password] = @Password;
END