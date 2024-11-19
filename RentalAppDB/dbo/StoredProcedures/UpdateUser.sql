CREATE PROCEDURE [dbo].[UpdateUser]
	@UserID int,
	@UserName VARCHAR(30),
	@Password VARCHAR(30),
	@PhoneNumber VARCHAR(30),
	@Email VARCHAR(50),
	@AccountTypeID INT
AS
BEGIN
    UPDATE [Users]
    SET 
	UserName = @UserName,
	[Password] = @Password,
	PhoneNumber = @PhoneNumber,
	Email = @Email,
	AccountTypeID = @AccountTypeID

    WHERE UserID = @UserID;
END
