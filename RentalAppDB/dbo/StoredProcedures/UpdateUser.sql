CREATE PROCEDURE [dbo].[UpdateUser]
	@UserID int,
	@FullName VARCHAR(30),
	@Password VARCHAR(30),
	@PhoneNumber VARCHAR(30),
	@AccountTypeID INT
AS
BEGIN
    UPDATE [Users]
    SET 
	FullName = @FullName,
	[Password] = @Password,
	PhoneNumber = @PhoneNumber,
	AccountTypeID = @AccountTypeID

    WHERE UserID = @UserID;
END
