USE master;
GO

IF DB_ID(N'IssueTracker') IS NULL
BEGIN
    CREATE DATABASE IssueTracker;
END;
GO

USE IssueTracker;
GO

IF OBJECT_ID(N'dbo.Tickets', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Tickets
    (
        Id INT IDENTITY(1, 1) PRIMARY KEY,
        Title NVARCHAR(100) NOT NULL,
        Description NVARCHAR(1000) NOT NULL,
        Status NVARCHAR(20) NOT NULL DEFAULT N'Open',
        Priority NVARCHAR(10) NOT NULL,
        CONSTRAINT CK_Tickets_Title CHECK (LEN(LTRIM(RTRIM(Title))) >= 5),
        CONSTRAINT CK_Tickets_Description CHECK (LEN(LTRIM(RTRIM(Description))) >= 10),
        CONSTRAINT CK_Tickets_Status CHECK (Status IN (N'Open', N'InProgress', N'Closed')),
        CONSTRAINT CK_Tickets_Priority CHECK (Priority IN (N'Low', N'Normal', N'High'))
    );
END;
GO
