-- ========================================================
-- CARO GAME DATABASE (SQL SERVER)
-- Mo hinh Chuan mang LAN (Database tren Server/Host)
-- ========================================================

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'CaroGameDB')
BEGIN
    CREATE DATABASE CaroGameDB;
END
GO

USE CaroGameDB;
GO

-- 1. Bang nguoi dung (Users)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(50) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(256) NOT NULL,
        Elo INT NOT NULL DEFAULT 1000,
        Wins INT NOT NULL DEFAULT 0,
        Losses INT NOT NULL DEFAULT 0,
        Draws INT NOT NULL DEFAULT 0,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );

    CREATE NONCLUSTERED INDEX IX_Users_Elo ON Users (Elo DESC);
END
GO

-- 2. Bang lich su tran dau (MatchHistory)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MatchHistory')
BEGIN
    CREATE TABLE MatchHistory (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Player1Username NVARCHAR(50) NOT NULL,
        Player2Username NVARCHAR(50) NOT NULL,
        WinnerUsername NVARCHAR(50) NULL, -- NULL neu tran hoa
        Player1EloBefore INT NOT NULL,
        Player1EloAfter INT NOT NULL,
        Player2EloBefore INT NOT NULL,
        Player2EloAfter INT NOT NULL,
        TotalMoves INT NOT NULL DEFAULT 0,
        PlayedAt DATETIME NOT NULL DEFAULT GETDATE()
    );

    CREATE NONCLUSTERED INDEX IX_MatchHistory_P1 ON MatchHistory (Player1Username);
    CREATE NONCLUSTERED INDEX IX_MatchHistory_P2 ON MatchHistory (Player2Username);
    CREATE NONCLUSTERED INDEX IX_MatchHistory_PlayedAt ON MatchHistory (PlayedAt DESC);
END
GO

