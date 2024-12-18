﻿CREATE PROCEDURE [dbo].[QuotedInformation]
	@PropertyID int
AS
BEGIN
	SELECT p.[PropertyID], [SqFt], [Facilities], [Type], [Price], [OwnerID], p.[AddressID], [Availability], t.[TermID],
	t.[TermName], [FullName], [PhoneNumber], [Email], [Neighborhood], [StreetNumber], [StreetName], 
	[City], a.[ProvinceID], pr.[ProvinceName], [Country], [SuiteNumber], [PostalCode]
	FROM Properties p JOIN Term  t ON p.TermID = t.TermID
	JOIN Users u ON u.UserID = p.OwnerID
	JOIN [Address] a ON a.AddressID = p.AddressID
	JOIN Provinces pr ON pr.ProvinceID = a.ProvinceID
	WHERE p.PropertyID = @PropertyID
END