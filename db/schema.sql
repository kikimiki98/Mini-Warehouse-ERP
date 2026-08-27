CREATE TABLE Suppliers(
    Id INT PRIMARY KEY,
    Name VARCHAR(100)
    );

CREATE TABLE Products(
    Id INT PRIMARY KEY,
    Name VARCHAR(100),
    StockQuantity INT,
    SupplierId INT,
    FOREIGN KEY (SupplierId)
    REFERENCES Suppliers(Id)
    );

CREATE TABLE StockMovements(
    Id INT PRIMARY KEY,
    ProductId INT,
    Quantity INT,
    MovementType VARCHAR(20),
    MovementDate DATETIME,
    FOREIGN KEY (ProductId)
    REFERENCES Products(Id)
    );