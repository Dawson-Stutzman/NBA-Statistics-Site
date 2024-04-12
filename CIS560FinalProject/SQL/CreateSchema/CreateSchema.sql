IF NOT EXISTS
   (
      SELECT *
      FROM sys.schemas s
      WHERE s.[name] = N'NBA'
   )
BEGIN
   EXEC(N'CREATE SCHEMA [NBA] AUTHORIZATION [dbo]');
END;