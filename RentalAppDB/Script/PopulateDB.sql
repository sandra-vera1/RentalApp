INSERT INTO Term VALUES 
('Year'),
('Month'),
('Week'),
('Day');

INSERT INTO Provinces VALUES 
('AB'),
('BC'),
('MB'),
('NB'),
('NL'),
('NS'),
('NT'),
('NU'),
('ON'),
('PE'),
('QC'),
('SK'),
('YT');

INSERT INTO AccountType VALUES 
('Owner'), 
('Renter');



INSERT INTO Address VALUES
('Riverbend', 102, 'Elm St', 'Edmonton', 'Canada', NULL, 'T5H1R1', 1),
('Sunnybrook', 88, 'Pine Ave', 'Red Deer', 'Canada', 12, 'R3T2N4', 2),
('Crescent Heights', 256, 'Maple Rd', 'Calgary', 'Canada', NULL, 'S4P3A9', 1),
('West End', 54, 'Willow Lane', 'Toronto', 'Canada', 9, 'M5W1E6', 8),
('Glamorgan', 4, 'Oak way', 'Calgary', 'Canada', 5, 'ABC123', 1),
('Old Town', 709, 'Birch Blvd', 'Victoria', 'Canada', NULL, 'V8W3Y6', 2);


INSERT INTO Users VALUES 
('Tiny', 'abc123', '123-456-7879', 'tiny@gmail.com', 1),
('Al Dente', 'pasta4life', '234-567-8910', 'aldente@email.com', 2),
('Ray Sunshine', 'sunnysmile123', '345-678-9012', 'ray.s@gmail.com', 1),
('Pepper Mint', 'candycane88', '456-789-0123', 'pepper.mint@email.com', 2),
('Basil Herb', 'greenleaf!23', '567-890-1234', 'b.herb@email.com', 1);

INSERT INTO Properties VALUES 
(500, 'Climate control, 24/7 Access', 'storage', 400, 1, 1, 1, 1),
(750, 'Climate control, Easy loading area', 'storage', 600, 1, 2, 3, 1),
(200, 'Security camera, 24/7 Access', 'parking lot', 50, 2, 3, 2, 1),
(150, 'Near elevator, Secure access', 'parking lot', 45, 2, 4, 1, 1),
(600, 'Large vehicle access, 24/7 Access', 'parking lot', 65, 1, 5, 4, 1),
(550, 'Temperature control, Private access', 'storage', 450, 1, 3, 3, 0),
(800, 'Drive-up access, CCTV', 'storage', 700, 1, 2, 2, 1),
(100, 'Underground, secure gate', 'parking lot', 40, 2, 1, 1, 1),
(120, 'Covered stall, Convenient location', 'parking lot', 30, 1, 4, 3, 1),
(140, 'Close to entrance, Security patrol', 'parking lot', 35, 2, 5, 4, 1);


INSERT INTO ParkingStalls VALUES
(1, 300, 3, 2),
(1, 400, 3, 2),
(1, 100, 3, 3),
(1, 25, 3, 4),
(1, 300, 4, 2),
(1, 350, 4, 2),
(0, 10, 4, 4),
(1, 75, 4, 3),
(1, 15, 4, 4),
(1, 60, 5, 3);

