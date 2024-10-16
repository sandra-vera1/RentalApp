CREATE TABLE [dbo].[ParkingStalls]
(
	[StallID] INT NOT NULL PRIMARY KEY, 
    [Availability] BIT NOT NULL, 
    [Price] MONEY NULL,
    [PropertyID] INT NOT NULL, 
    [TermID] INT NOT NULL,
    CONSTRAINT [FK_ParkingStalls_ToTerm] FOREIGN KEY (TermID) REFERENCES Term(TermID), 
    CONSTRAINT [FK_ParkingStalls_ToProperty] FOREIGN KEY (PropertyID) REFERENCES Properties(PropertyID)
)
