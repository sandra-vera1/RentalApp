﻿CREATE PROCEDURE [dbo].[SaveUser]
	@FullName VARCHAR(30),
	@Password VARCHAR(30),
	@PhoneNumber VARCHAR(30),
	@Email VARCHAR(50),
	@AccountTypeID INT
AS BEGIN
	INSERT INTO [dbo].[Users]([FullName], [Password], [PhoneNumber], [Email], [AccountTypeID])
	VALUES(@FullName, @Password, @PhoneNumber, @Email, @AccountTypeID);
END
