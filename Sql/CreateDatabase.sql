
CREATE DATABASE KoiCareSystem;

USE KoiCareSystem;


CREATE TABLE [User] (
    UserId NVARCHAR(450) NOT NULL PRIMARY KEY,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    Name NVARCHAR(255) NOT NULL
);


INSERT INTO [User] (UserId, Email, Password, Role, Name)
VALUES
    ('U001', 'member1@example.com', 'Abc@123', 'member', 'Member One'),
    ('U002', 'member2@example.com', 'Abc@123', 'member', 'Member Two'),
    ('U003', 'admin@example.com', 'Abc@123', 'admin', 'Admin User'),
    ('U004', 'manager@example.com', 'Abc@123', 'manager', 'Manager User');


CREATE TABLE Pond (
    PondId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserId NVARCHAR(450) NOT NULL,
    Name NVARCHAR(50) NOT NULL,
    Volume INT NOT NULL,
    Thumbnail VARCHAR(MAX) NULL,
    Depth REAL NOT NULL,
    PumpingCapacity INT NOT NULL,
    Drain INT NOT NULL,
    Skimmer INT NOT NULL,
    Note NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Pond_User FOREIGN KEY (UserId) REFERENCES [User](UserId)
);


INSERT INTO Pond (UserID, Name, Volume, Thumbnail, Depth, PumpingCapacity, Drain, Skimmer, Note)
VALUES 
('U001', 'Main Pond', 2000.0, 'https://th.bing.com/th/id/R.28e1217b364f51df5df816cae92b7bee?rik=4qTEdf%2bcdv39mg&riu=http%3a%2f%2f3.bp.blogspot.com%2f-E2t4QdpXEdc%2fTkPiqm1rOGI%2fAAAAAAAAAKM%2f-q6u_C9toJw%2fs1600%2fCopy%2bof%2bbutler%252526carterconstuction%2b305.jpg&ehk=AriWPv16%2fg3C15kmspYhYwoX4tSmlWaSx3qx9%2f6htmU%3d&risl=&pid=ImgRaw&r=0', 4, 700, 1, 1, 'This is the main pond for koi fish.'),
('U001', 'Small Pond', 500.0, 'https://www.pondplants.co.uk/wp-content/uploads/2018/02/koi-in-a-pool-merebrook-pondplants.jpg', 2, 300, 1, 0, 'A small pond for young koi.'),
('U002', 'Outdoor Pond', 1500.0, 'https://www.nualgiponds.com/wp-content/uploads/2014/04/nualgi-koi-pond.jpg', 3, 500, 1, 1, 'Outdoor pond with a skimmer and large volume.'),
('U002', 'Indoor Pond', 750.0, 'https://www.kodamakoifarm.com/wp-content/uploads/2018/11/koi-pond-winter-care.jpg', 2, 400, 0, 1, 'Indoor pond for special koi breeding.');


CREATE TABLE Koi (
    KoiId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    PondId INT NOT NULL,
    UserId NVARCHAR(450) NOT NULL,
    Age INT NOT NULL,
    Thumbnail NVARCHAR(MAX) NULL,
    Name NVARCHAR(255) NOT NULL,
    Note NVARCHAR(MAX) NULL,
    Origin NVARCHAR(255) NOT NULL,
    Length INT NOT NULL,
    Weight REAL NOT NULL,
    Color NVARCHAR(200) NOT NULL,
    CreateAt DATETIME2(7) NOT NULL,
    Sex NVARCHAR(MAX) NOT NULL,
    Variety NVARCHAR(MAX) NOT NULL,
    Physique NVARCHAR(MAX) NOT NULL,
    Status BIT NULL,
    CONSTRAINT FK_Koi_Pond FOREIGN KEY (PondId) REFERENCES Pond(PondId),
    CONSTRAINT FK_Koi_User FOREIGN KEY (UserId) REFERENCES [User](UserId)
);


INSERT INTO Koi (UserID, PondID, Thumbnail, Age, Name, Note, Origin, Length, Weight, Color, CreateAt, Sex, Variety, Physique, Status)
VALUES
('U001', 1, 'https://th.bing.com/th/id/OIP.Gfyrt1yYp4TtEK6HU6tMxwHaIL?pid=ImgDet&w=474&h=523&rs=1', 1, 'Hotaro', 'A beautiful koi with bright colors.', 'Japan', 45, 3100, 'Orange and White', GETDATE(), 'Male','Kohaku','Corpulent', 1),  
('U001', 3, 'https://th.bing.com/th/id/OIP.4EUdtGjKRHc08wAsM6gjJQHaLL?pid=ImgDet&w=474&h=715&rs=1', 1, 'Kawasaki', 'Young koi, growing fast.', 'USA', 30, 2000, 'Black and Yellow', GETDATE(), 'Female','Sanke','Slim', 1),  
('U001', 2, 'https://th.bing.com/th/id/OIP.P7sMlIeBzK-X3INSPj7czgHaGQ?pid=ImgDet&w=474&h=400&rs=1', 2, 'Neru', 'A stunning koi with unique patterns.', 'China', 35, 3000, 'Red and White', GETDATE(), 'Male','Hikari','Normal', 1),  
('U001', 4, 'https://th.bing.com/th/id/OIP.wqdDkTl9KO4ahpdbVumYwQHaHa?w=1000&h=1000&rs=1&pid=ImgDetMain', 2, 'Yama', 'Very active koi, loves to swim.', 'Thailand', 52, 4600, 'Blue and Orange', GETDATE(), 'Female','Kohaku','Corpulent', 1),  
('U002', 1, 'https://aquariumfishindia.com/wp-content/uploads/2021/12/200563137.jpg', 3, 'Eitoku', 'Friendly koi, often interacts with people.', 'Japan', 38, 2600, 'White with Black Spots', GETDATE(), 'Female','Asagi','Slim', 1),  
('U002', 3, 'https://i.pinimg.com/736x/3c/71/52/3c7152339593b502fee1dec5d14a8f2d.jpg', 3, 'Anteiku', 'An older koi with lots of personality.', 'USA', 65, 5300, 'Yellow and Black', GETDATE(), 'Male','Sanke','Corpulent', 1),  
('U002', 2, 'https://th.bing.com/th/id/OIP.KOCOJ84hiegMDqHYOucJaQHaMW?w=1228&h=2048&rs=1&pid=ImgDetMain', 4, 'Garam', 'Small but very colorful.', 'China', 40, 3000, 'Orange and Black', GETDATE(), 'Female','Shusui','Slim', 1),  
('U002', 4, 'https://aquariumfishindia.com/wp-content/uploads/2021/12/200711250.jpg', 4, 'Honda', 'A rare breed of koi.', 'Thailand', 48, 3700, 'Calico', GETDATE(), 'Male','Hikari','Normal', 1);  


CREATE TABLE Category (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CreatedAt DATETIME2(7) NOT NULL DEFAULT GETDATE(),
);


INSERT INTO Category (Name, Description)
VALUES
    ('Koi Treatment', 'Products and solutions to treat koi-specific health issues, such as anti-bacterial and anti-parasite treatments.'),
    ('Water Treatment', 'Products to maintain and improve water quality, including water conditioners and pH balancers.');


CREATE TABLE ExternalProduct (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Price DECIMAL(18, 2) NOT NULL,
    Thumbnail NVARCHAR(MAX) NULL,
    ExternalUrl NVARCHAR(2083) NOT NULL,
    CategoryId INT NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2(7) NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT FK_ExternalProduct_Category FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);


CREATE TABLE Measurements (
    MeasureId INT IDENTITY(1,1) PRIMARY KEY,
    PondId INT NOT NULL,
    UserId NVARCHAR(450) NOT NULL,
    Nitrite FLOAT NOT NULL,
    Oxygen FLOAT NOT NULL,
    Nitrate FLOAT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    Temperature FLOAT NOT NULL,
    Phosphate FLOAT NOT NULL,
    pH FLOAT NOT NULL,
    Ammonium FLOAT NOT NULL,
    Hardness FLOAT NOT NULL,
    CarbonDioxide FLOAT NOT NULL,
    CarbonHardness FLOAT NOT NULL,
    Salt FLOAT NOT NULL,
    TotalChlorines FLOAT NOT NULL,
    OutdoorTemp FLOAT NOT NULL,
    AmountFed FLOAT NOT NULL,

    CONSTRAINT FK_Measurements_PondId FOREIGN KEY (PondId) REFERENCES Pond(PondId),
    CONSTRAINT FK_Measurements_UserId FOREIGN KEY (UserId) REFERENCES [User](UserId)
);