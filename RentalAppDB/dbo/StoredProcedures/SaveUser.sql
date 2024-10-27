CREATE PROCEDURE [dbo].[SaveUser]
	@UserName VARCHAR(30),
	@Password VARCHAR(30),
	@PhoneNumber VARCHAR(30),
	@Email VARCHAR(50),
	@AccountTypeID INT
AS BEGIN
	INSERT INTO [dbo].[Users]([UserName], [Password], [PhoneNumber], [Email], [AccountTypeID])
	VALUES(@UserName, @Password, @PhoneNumber, @Email, @AccountTypeID);
END
