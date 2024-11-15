CREATE PROCEDURE [dbo].[CreateProperty]
	@SqFt VARCHAR(30),
	@Facilities VARCHAR(100),
	@Type VARCHAR(30),
	@Price MONEY,
	@OwnerID INT, 
	@AddressID INT,
	@TermID INT,
	@Availability BIT

AS BEGIN
	INSERT INTO [dbo].[Properties]([SqFt], [Facilities], [Type], [Price], [OwnerID], [AddressID], [TermID], [Availability])
	VALUES (@SqFt, @Facilities, @Type, @Price, @OwnerID, @AddressID, @TermID, @Availability);
END
