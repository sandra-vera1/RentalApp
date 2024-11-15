CREATE PROCEDURE [dbo].[DeleteAddress]
	@AddressID int
AS BEGIN
	IF EXISTS (SELECT AddressID FROM [Address] WHERE AddressID = @AddressID)
    BEGIN
        DELETE FROM [Address] WHERE AddressID = @AddressID;
    END
    ELSE
    BEGIN
        PRINT 'Address not found';
    END 
 END 