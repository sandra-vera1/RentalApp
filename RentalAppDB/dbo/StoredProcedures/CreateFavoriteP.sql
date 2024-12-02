CREATE PROCEDURE [dbo].[CreateFavoriteP]
	@PropertyID int,
	@RenterID int
AS BEGIN
 IF NOT EXISTS (SELECT Id FROM [PropertyFavorites] WHERE PropertyID = @PropertyID AND RenterID = @RenterID)
    BEGIN
	INSERT INTO [dbo].[PropertyFavorites]([PropertyID], [RenterID])
	VALUES(@PropertyID, @RenterID);
	END
END
