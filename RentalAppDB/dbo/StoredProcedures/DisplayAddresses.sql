CREATE PROCEDURE [dbo].[DisplayAddresses]
AS
BEGIN
	SELECT [AddressID],[Neighborhood], [StreetNumber], [StreetName], [City], [Address].[ProvinceID],[ProvinceName], [Country],[SuiteNumber],[PostalCode], [UserID]
	FROM [Address]
	INNER JOIN Provinces ON [Address].ProvinceID = Provinces.ProvinceID;
END
