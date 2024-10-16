CREATE TABLE [dbo].[Address]
(
	[AddressID] INT NOT NULL PRIMARY KEY, 
    [Neighborhood] VARCHAR(100) NOT NULL, 
    [StreetNumber] INT NOT NULL, 
    [StreetName] VARCHAR(150) NOT NULL, 
    [City] VARCHAR(50) NOT NULL, 
    [Country] VARCHAR(50) NOT NULL, 
    [SuiteNumber] INT NULL, 
    [PostalCode] CHAR(6) NOT NULL,
    [ProvinceID] INT NOT NULL, 
    CONSTRAINT [FK_Address_ToProvinces] FOREIGN KEY (ProvinceID) REFERENCES Provinces(ProvinceID)
)
