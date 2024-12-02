CREATE OR ALTER PROCEDURE [dbo].[UpdateProperty]
	@PropertyId INT,
	@SqFt VARCHAR(30),
	@Facilities VARCHAR(100),
	@Type VARCHAR(30),
	@Price MONEY,
	-- @OwnerID INT, 
	@AddressID INT,
	@TermID INT,
	@Availability BIT

AS BEGIN
	UPDATE [dbo].[Properties] 
	SET [SqFt] = @SqFt, 
	[Facilities] = @Facilities, 
	[Type] = @Type, 
	[Price] = @Price, 
	-- [OwnerID] = @OwnerID, shouldn't need this.
	[AddressID] = @AddressID, 
	[TermID] = @TermID, 
	[Availability] = @Availability

	WHERE PropertyID = @PropertyId
END


