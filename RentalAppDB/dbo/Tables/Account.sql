CREATE TABLE [dbo].[Account]
(
	[AccountId] INT NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    CONSTRAINT [FK_Account_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
)
