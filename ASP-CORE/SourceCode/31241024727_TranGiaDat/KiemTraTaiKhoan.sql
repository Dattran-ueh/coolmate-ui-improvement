USE [DatPaper31241024727];
GO

SELECT DB_NAME() AS TenCSDL;

SELECT TOP (10)
    Id,
    UserName,
    Email,
    EmailConfirmed
FROM dbo.AspNetUsers;