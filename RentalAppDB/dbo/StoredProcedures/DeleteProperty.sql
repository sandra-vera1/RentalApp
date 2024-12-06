

CREATE OR ALTER PROCEDURE [dbo].[DeleteProperty]
	@propertyId int
AS BEGIN
	IF EXISTS (SELECT PropertyID FROM Properties WHERE PropertyID = @propertyId)
    BEGIN
        DELETE FROM Properties WHERE PropertyID = @propertyId;
    END
    ELSE
    BEGIN
        PRINT 'Property not found';
    END 
 END 


