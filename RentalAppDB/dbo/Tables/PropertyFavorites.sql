CREATE TABLE [dbo].[PropertyFavorites]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [PropertyID] INT NOT NULL, 
    [RenterID] INT NOT NULL, 
    CONSTRAINT [FK_PropertyFavorites_Properties] FOREIGN KEY ([PropertyID]) REFERENCES [Properties]([PropertyID]) ON DELETE CASCADE, 
    CONSTRAINT [FK_PropertyFavorites_Users] FOREIGN KEY ([RenterID]) REFERENCES [Users]([UserID]) ON DELETE CASCADE
)
