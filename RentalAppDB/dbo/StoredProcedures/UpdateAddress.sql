CREATE PROCEDURE [dbo].[UpdateAddress]
	@AddressID int,
	@Neighborhood varchar(100),
	@StreetNumber int,
	@StreetName varchar(150),
	@City varchar(50),
	@ProvinceID int, 
	@Country varchar(50),
	@SuiteNumber int,
	@PostalCode char(6)
AS
BEGIN
    UPDATE [Address]
    SET 
        Neighborhood = @Neighborhood,
		StreetNumber = @StreetNumber,
		StreetName = @StreetName,
		City = @City,
		ProvinceID = @ProvinceID, 
		Country = @Country,
		SuiteNumber = @SuiteNumber,
		PostalCode = @PostalCode
    WHERE AddressID = @AddressID;
END
