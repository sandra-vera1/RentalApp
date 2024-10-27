IF DB_ID('RentalAppDB') IS NOT NULL
	DROP DATABASE RentalAppDB
GO

CREATE DATABASE RentalAppDB
GO

USE RentalAppDB
GO


CREATE TABLE [dbo].[AccountType] (
    [AccountTypeID] INT IDENTITY(1,1) NOT NULL,
    [AccountName]   VARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([AccountTypeID] ASC)
);

CREATE TABLE [dbo].[Address] (
    [AddressID]    INT IDENTITY(1,1) NOT NULL,
    [Neighborhood] VARCHAR (100) NOT NULL,
    [StreetNumber] INT           NOT NULL,
    [StreetName]   VARCHAR (150) NOT NULL,
    [City]         VARCHAR (50)  NOT NULL,
    [Country]      VARCHAR (50)  NOT NULL,
    [SuiteNumber]  INT           NULL,
    [PostalCode]   CHAR (6)      NOT NULL,
    [ProvinceID]   INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([AddressID] ASC)
);

CREATE TABLE [dbo].[ParkingStalls] (
    [StallID]      INT IDENTITY(1,1) NOT NULL,
    [Availability] BIT   NOT NULL,
    [Price]        MONEY NULL,
    [PropertyID]   INT   NOT NULL,
    [TermID]       INT   NOT NULL,
    PRIMARY KEY CLUSTERED ([StallID] ASC)
);

CREATE TABLE [dbo].[Properties] (
    [PropertyID]   INT IDENTITY(1,1) NOT NULL,
    [SqFt]         VARCHAR (30)  NOT NULL,
    [Facilities]   VARCHAR (100) NOT NULL,
    [Type]         VARCHAR (30)  NOT NULL,
    [Price]        MONEY         NOT NULL,
    [OwnerID]      INT           NOT NULL,
    [AddressID]    INT           NOT NULL,
    [TermID]       INT           NOT NULL,
    [Availability] BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([PropertyID] ASC)
);

CREATE TABLE [dbo].[Provinces] (
    [ProvinceID]   INT IDENTITY(1,1) NOT NULL,
    [ProvinceName] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ProvinceID] ASC)
);

CREATE TABLE [dbo].[Term] (
    [TermID]   INT IDENTITY(1,1) NOT NULL,
    [TermName] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([TermID] ASC)
);

CREATE TABLE [dbo].[Users] (
    [UserID]        INT IDENTITY(1,1) NOT NULL,
    [UserName]      VARCHAR (30) NOT NULL,
    [Password]      VARCHAR (30) NOT NULL,
    [PhoneNumber]   VARCHAR (30) NOT NULL,
    [Email]         VARCHAR (50) NOT NULL,
    [AccountTypeID] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

ALTER TABLE [dbo].[Address] WITH NOCHECK
    ADD CONSTRAINT [FK_Address_ToProvinces] FOREIGN KEY ([ProvinceID]) REFERENCES [dbo].[Provinces] ([ProvinceID]);

ALTER TABLE [dbo].[ParkingStalls] WITH NOCHECK
    ADD CONSTRAINT [FK_ParkingStalls_ToTerm] FOREIGN KEY ([TermID]) REFERENCES [dbo].[Term] ([TermID]);

ALTER TABLE [dbo].[ParkingStalls] WITH NOCHECK
    ADD CONSTRAINT [FK_ParkingStalls_ToProperty] FOREIGN KEY ([PropertyID]) REFERENCES [dbo].[Properties] ([PropertyID]);

ALTER TABLE [dbo].[Properties] WITH NOCHECK
    ADD CONSTRAINT [FK_Properties_ToUsers] FOREIGN KEY ([OwnerID]) REFERENCES [dbo].[Users] ([UserID]);

ALTER TABLE [dbo].[Properties] WITH NOCHECK
    ADD CONSTRAINT [FK_Properties_ToTerm] FOREIGN KEY ([TermID]) REFERENCES [dbo].[Term] ([TermID]);

ALTER TABLE [dbo].[Properties] WITH NOCHECK
    ADD CONSTRAINT [FK_Properties_ToAddress] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Address] ([AddressID]);

ALTER TABLE [dbo].[Users] WITH NOCHECK
    ADD CONSTRAINT [FK_Users_ToAccountType] FOREIGN KEY ([AccountTypeID]) REFERENCES [dbo].[AccountType] ([AccountTypeID]);

ALTER TABLE [dbo].[Address] WITH CHECK CHECK CONSTRAINT [FK_Address_ToProvinces];

ALTER TABLE [dbo].[ParkingStalls] WITH CHECK CHECK CONSTRAINT [FK_ParkingStalls_ToTerm];

ALTER TABLE [dbo].[ParkingStalls] WITH CHECK CHECK CONSTRAINT [FK_ParkingStalls_ToProperty];

ALTER TABLE [dbo].[Properties] WITH CHECK CHECK CONSTRAINT [FK_Properties_ToUsers];

ALTER TABLE [dbo].[Properties] WITH CHECK CHECK CONSTRAINT [FK_Properties_ToTerm];

ALTER TABLE [dbo].[Properties] WITH CHECK CHECK CONSTRAINT [FK_Properties_ToAddress];

ALTER TABLE [dbo].[Users] WITH CHECK CHECK CONSTRAINT [FK_Users_ToAccountType];