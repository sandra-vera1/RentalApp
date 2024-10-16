CREATE TABLE [dbo].[Properties]
(
	[PropertyID] INT NOT NULL PRIMARY KEY, 
    [SqFt] VARCHAR(30) NOT NULL, 
    [Facilities] VARCHAR(100) NOT NULL, 
    [Type] VARCHAR(30) NOT NULL, 
    [Price] MONEY NOT NULL, 
    [OwnerID] INT NOT NULL,
    [AddressID] INT NOT NULL, 
    [TermID] INT NOT NULL, 
    [Availability] BIT NOT NULL, 
    CONSTRAINT [FK_Properties_ToUsers] FOREIGN KEY (OwnerID) REFERENCES Users(UserID), 
    CONSTRAINT [FK_Properties_ToTerm] FOREIGN KEY (TermID) REFERENCES Term(TermID), 
    CONSTRAINT [FK_Properties_ToAddress] FOREIGN KEY (AddressID) REFERENCES Address(AddressID)
)
