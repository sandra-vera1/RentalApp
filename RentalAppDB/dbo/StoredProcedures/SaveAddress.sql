CREATE PROCEDURE [dbo].[SaveAddress]
	@Neighborhood varchar(100),
	@StreetNumber int,
	@StreetName varchar(150),
	@City varchar(50),
	@ProvinceID int = 1, --Alberta in Province table
	@Country varchar(50),
	@SuiteNumber int,
	@PostalCode char(6)

AS BEGIN
	INSERT INTO [dbo].[Address]([Neighborhood], [StreetNumber], [StreetName], [City], [ProvinceID], [Country],[SuiteNumber],[PostalCode])
	VALUES(@Neighborhood, @StreetNumber, @StreetName, @City, @ProvinceID, @Country, @SuiteNumber, @PostalCode);
END
