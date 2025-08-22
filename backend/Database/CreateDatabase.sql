-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'CocktailDB')
BEGIN
    CREATE DATABASE CocktailDB;
END
GO

USE CocktailDB;
GO

-- Create Tables
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Cocktails')
BEGIN
    CREATE TABLE Cocktails (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(255) NOT NULL,
        Category NVARCHAR(100),
        Alcoholic NVARCHAR(50),
        Glass NVARCHAR(100),
        Instructions NVARCHAR(MAX),
        ImageUrl NVARCHAR(500),
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Ingredients')
BEGIN
    CREATE TABLE Ingredients (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(255) NOT NULL,
        Measure NVARCHAR(100),
        CocktailId INT NOT NULL,
        FOREIGN KEY (CocktailId) REFERENCES Cocktails(Id) ON DELETE CASCADE
    );
END
GO

-- Create Stored Procedures

-- Get All Cocktails
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetAllCocktails')
    DROP PROCEDURE sp_GetAllCocktails;
GO

CREATE PROCEDURE sp_GetAllCocktails
AS
BEGIN
    SELECT 
        c.Id,
        c.Name,
        c.Category,
        c.Alcoholic,
        c.Glass,
        c.Instructions,
        c.ImageUrl,
        c.CreatedAt,
        c.UpdatedAt
    FROM Cocktails c
    ORDER BY c.Name;
END
GO

-- Get Cocktail By ID
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetCocktailById')
    DROP PROCEDURE sp_GetCocktailById;
GO

CREATE PROCEDURE sp_GetCocktailById
    @Id INT
AS
BEGIN
    -- Get cocktail details
    SELECT 
        c.Id,
        c.Name,
        c.Category,
        c.Alcoholic,
        c.Glass,
        c.Instructions,
        c.ImageUrl,
        c.CreatedAt,
        c.UpdatedAt
    FROM Cocktails c
    WHERE c.Id = @Id;
    
    -- Get ingredients
    SELECT 
        i.Id,
        i.Name,
        i.Measure,
        i.CocktailId
    FROM Ingredients i
    WHERE i.CocktailId = @Id
    ORDER BY i.Id;
END
GO

-- Create Cocktail
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_CreateCocktail')
    DROP PROCEDURE sp_CreateCocktail;
GO

CREATE PROCEDURE sp_CreateCocktail
    @Name NVARCHAR(255),
    @Category NVARCHAR(100),
    @Alcoholic NVARCHAR(50),
    @Glass NVARCHAR(100),
    @Instructions NVARCHAR(MAX),
    @ImageUrl NVARCHAR(500)
AS
BEGIN
    INSERT INTO Cocktails (Name, Category, Alcoholic, Glass, Instructions, ImageUrl, CreatedAt)
    VALUES (@Name, @Category, @Alcoholic, @Glass, @Instructions, @ImageUrl, GETUTCDATE());
    
    SELECT SCOPE_IDENTITY();
END
GO

-- Update Cocktail
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_UpdateCocktail')
    DROP PROCEDURE sp_UpdateCocktail;
GO

CREATE PROCEDURE sp_UpdateCocktail
    @Id INT,
    @Name NVARCHAR(255),
    @Category NVARCHAR(100),
    @Alcoholic NVARCHAR(50),
    @Glass NVARCHAR(100),
    @Instructions NVARCHAR(MAX),
    @ImageUrl NVARCHAR(500)
AS
BEGIN
    UPDATE Cocktails 
    SET 
        Name = @Name,
        Category = @Category,
        Alcoholic = @Alcoholic,
        Glass = @Glass,
        Instructions = @Instructions,
        ImageUrl = @ImageUrl,
        UpdatedAt = GETUTCDATE()
    WHERE Id = @Id;
END
GO

-- Delete Cocktail
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_DeleteCocktail')
    DROP PROCEDURE sp_DeleteCocktail;
GO

CREATE PROCEDURE sp_DeleteCocktail
    @Id INT
AS
BEGIN
    DELETE FROM Cocktails WHERE Id = @Id;
END
GO

-- Search Cocktails
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_SearchCocktails')
    DROP PROCEDURE sp_SearchCocktails;
GO

CREATE PROCEDURE sp_SearchCocktails
    @SearchTerm NVARCHAR(255)
AS
BEGIN
    SELECT 
        c.Id,
        c.Name,
        c.Category,
        c.Alcoholic,
        c.Glass,
        c.Instructions,
        c.ImageUrl,
        c.CreatedAt,
        c.UpdatedAt
    FROM Cocktails c
    WHERE 
        c.Name LIKE '%' + @SearchTerm + '%' OR
        c.Category LIKE '%' + @SearchTerm + '%' OR
        c.Instructions LIKE '%' + @SearchTerm + '%'
    ORDER BY c.Name;
END
GO

-- Insert Sample Data
IF NOT EXISTS (SELECT * FROM Cocktails WHERE Name = 'Mojito')
BEGIN
    INSERT INTO Cocktails (Name, Category, Alcoholic, Glass, Instructions, ImageUrl, CreatedAt)
    VALUES ('Mojito', 'Cocktail', 'Alcoholic', 'Highball glass', 'Muddle mint leaves with sugar and lime juice. Add rum and fill with ice. Top with soda water and garnish with mint sprig.', 'https://www.thecocktaildb.com/images/media/drink/metwgh1606770327.jpg', GETUTCDATE());
    
    DECLARE @MojitoId INT = SCOPE_IDENTITY();
    
    INSERT INTO Ingredients (Name, Measure, CocktailId) VALUES
    ('White rum', '2 oz', @MojitoId),
    ('Lime juice', '1 oz', @MojitoId),
    ('Sugar', '2 tsp', @MojitoId),
    ('Mint leaves', '6-8', @MojitoId),
    ('Soda water', 'Top up', @MojitoId);
END
GO

IF NOT EXISTS (SELECT * FROM Cocktails WHERE Name = 'Old Fashioned')
BEGIN
    INSERT INTO Cocktails (Name, Category, Alcoholic, Glass, Instructions, ImageUrl, CreatedAt)
    VALUES ('Old Fashioned', 'Cocktail', 'Alcoholic', 'Old-fashioned glass', 'Place sugar cube in old-fashioned glass and saturate with bitters. Add a dash of plain water. Muddle until dissolved. Fill the glass with ice cubes and add whiskey. Garnish with orange slice and a cocktail cherry.', 'https://www.thecocktaildb.com/images/media/drink/vrwquq1478252802.jpg', GETUTCDATE());
    
    DECLARE @OldFashionedId INT = SCOPE_IDENTITY();
    
    INSERT INTO Ingredients (Name, Measure, CocktailId) VALUES
    ('Bourbon', '4.5 cL', @OldFashionedId),
    ('Angostura bitters', '2 dashes', @OldFashionedId),
    ('Sugar cube', '1', @OldFashionedId),
    ('Water', 'dash', @OldFashionedId);
END
GO

PRINT 'Database and stored procedures created successfully!'; 