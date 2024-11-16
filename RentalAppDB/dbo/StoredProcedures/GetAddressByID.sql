CREATE PROCEDURE [dbo].[GetAddressByID]
	@AddressID INT
AS
BEGIN
	SELECT * From Address
	WHERE AddressID = @AddressID
END
