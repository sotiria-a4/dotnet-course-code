CREATE DATABASE DotNetCourseDatabase;


GO
USE DotNetCourseDatabase;

-- GO
-- CREATE SCHEMA TutorialAppSchema;
-- GO
-- CREATE TABLE TutorialAppSchema.Computer (
--     ComputerId  INT             IDENTITY (1, 1) PRIMARY KEY,
--     Motherboard NVARCHAR (50)  ,
--     CPUCores    INT            ,
--     HasWifi     BIT            ,
--     HasLTE      BIT            ,
--     ReleaseDate DATE           ,
--     Price       DECIMAL (18, 4),
--     VideoCard   NVARCHAR (50)   
-- );
SELECT [ComputerId],
       [Motherboard],
       [CPUCores],
       [HasWifi],
       [HasLTE],
       [ReleaseDate],
       [Price],
       [VideoCard]
FROM   TutorialAppSchema.Computer;

SELECT * FROM TutorialAppSchema.Computer;


--WHERE  1 = 0;
SET IDENTITY_INSERT TutorialAppSchema.Computer ON;

INSERT  INTO TutorialAppSchema.Computer (
    [Motherboard],
    [CPUCores],
    [HasWifi],
    [HasLTE],
    [ReleaseDate],
    [Price],
    [VideoCard]
)
VALUES 
 ('Sample-Motherboard', 4, 1, 0, '2022-01-01', 1000, 'Sample-VideoCard');

DELETE TutorialAppSchema.Computer
WHERE  ComputerId = 1;

