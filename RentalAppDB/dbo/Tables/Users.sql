CREATE TABLE [dbo].[Users]
(
	[UserID] INT NOT NULL PRIMARY KEY, 
    [UserName] VARCHAR(30) NOT NULL, 
    [Password] VARCHAR(30) NOT NULL,
    [PhoneNumber] VARCHAR(30) NOT NULL,
    [Email] VARCHAR(50) NOT NULL, 
    [AccountTypeID] INT NOT NULL, 
    CONSTRAINT [FK_Users_ToAccountType] FOREIGN KEY (AccountTypeID) REFERENCES AccountType(AccountTypeID)
)
