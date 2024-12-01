CREATE PROCEDURE [dbo].[SaveAddress]
	@UserId INT,
	@Neighborhood varchar(100),
	@StreetNumber int,
	@StreetName varchar(150),
	@City varchar(50),
	@ProvinceID int = 1, 
	@Country varchar(50),
	@SuiteNumber int,
	@PostalCode char(6)

AS BEGIN
	INSERT INTO [dbo].[Address]([UserID], [Neighborhood], [StreetNumber], [StreetName], [City], [ProvinceID], [Country],[SuiteNumber],[PostalCode])
	VALUES(@UserId, @Neighborhood, @StreetNumber, @StreetName, @City, @ProvinceID, @Country, @SuiteNumber, @PostalCode);
END
