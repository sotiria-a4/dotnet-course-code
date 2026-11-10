USE DotNetCourseDatabase;
GO

SELECT [Users].[UserId],
[Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
[UserJobInfo].[JobTitle],
[UserJobInfo].[Department],
[UserSalary].[UserId],
[UserSalary].[Salary],
[Users].[Email],
[Users].[Gender],
[Users].[Active]


FROM TutorialAppSchema.Users AS Users
      INNER JOIN TutorialAppSchema.UserSalary AS UserSalary
            ON UserSalary.UserId = Users.UserId
      LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
            ON UserJobInfo.UserId = Users.UserId
            
WHERE Users.Active = 1
ORDER BY Users.UserId DESC

--DELETE FROM TutorialAppSchema.UserSalary WHERE UserId BETWEEN 250 AND 750 -- 501 ROWS
-- when we use between we also include the lower and upper bound of the value we are checking


--Θέλουμε να εμφανίσουμε τους μισθούς μόνο των χρηστών που υπάρχουν και στον πίνακα UserJobInfo
SELECT [UserSalary].[UserId],
      [UserSalary].[Salary] 
FROM TutorialAppSchema.UserSalary
      WHERE EXISTS (
            SELECT * FROM TutorialAppSchema.UserJobInfo AS UserJobInfo  --Υπάρχει στον πίνακα UserJobInfo έστω μία εγγραφή με το ίδιο UserId που εξετάζω αυτή τη στιγμή στον πίνακα UserSalary?
                  WHERE UserJobInfo.UserId = UserSalary.UserId)
            AND  UserId <> 7

SELECT [UserId],
[Salary] FROM TutorialAppSchema.UserSalary
-- UNION -- DISTINCT BETWEEN THE TWO QUERIES, Το UNION ενώνει τα αποτελέσματα δύο SELECT σε έναν ενιαίο πίνακα και αφαιρεί τις διπλότυπες γραμμές

UNION ALL -- WE DONT CARE IF IT IS DISTINCT OR NOT, WE GET ALL OF THE RECORDS FROM BOTH OF THESE QUERIES                              -- 

SELECT [UserId],
[Salary] FROM TutorialAppSchema.UserSalary
                  
SELECT [Users].[UserId],
[Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
[UserJobInfo].[JobTitle],
[UserJobInfo].[Department],
[UserSalary].[UserId],
[UserSalary].[Salary],
[Users].[Email],
[Users].[Gender],
[Users].[Active]

FROM TutorialAppSchema.Users AS Users
      INNER JOIN TutorialAppSchema.UserSalary AS UserSalary
            ON UserSalary.UserId = Users.UserId
      LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
            ON UserJobInfo.UserId = Users.UserId
            
WHERE Users.Active = 1
ORDER BY Users.UserId DESC

CREATE CLUSTERED INDEX cix_UserSalary_UserId ON TutorialAppSchema.UserSalary(UserId)
--CREATE NONCLUSTERED INDEX ix_UserSalary_Salary ON TutorialAppSchema.UserSalary(Salary)

CREATE NONCLUSTERED INDEX ix_UserJobInfo_JobTitle ON TutorialAppSchema.UserJobInfo(JobTitle) 
      INCLUDE (Department)

CREATE NONCLUSTERED INDEX fix_users_Active ON TutorialAppSchema.Users(Active) 
      INCLUDE ([Email],[FirstName],[LastName])
      WHERE Active=1
