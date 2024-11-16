CREATE OR ALTER PROCEDURE [dbo].[DisplayProperties]
AS
BEGIN
	SELECT [SqFt], [Facilities], [Type], [Price], [OwnerID], p.[AddressID], [Availability], 
	t.[TermName], [UserName], [PhoneNumber], [Email], [Neighborhood], [StreetNumber], [StreetName], 
	[City], a.[ProvinceID], pr.[ProvinceName], [Country], [SuiteNumber], [PostalCode]
	FROM Properties p JOIN Term  t ON p.TermID = t.TermID
	JOIN Users u ON u.UserID = p.OwnerID
	JOIN Address a ON a.AddressID = p.AddressID
	JOIN Provinces pr ON pr.ProvinceID = a.AddressID
END

