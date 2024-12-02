CREATE PROCEDURE [dbo].[DeleteFavoriteP]
	@FavoriteID int
AS BEGIN
	IF EXISTS (SELECT Id FROM [PropertyFavorites] WHERE Id = @FavoriteID)
    BEGIN
        DELETE FROM [PropertyFavorites] WHERE Id = @FavoriteID;
    END
    ELSE
    BEGIN
        PRINT 'Favorite property not found';
    END 
 END 