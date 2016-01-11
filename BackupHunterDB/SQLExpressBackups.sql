BACKUP DATABASE [Hunter]
TO DISK = N'C:\Hunter\BackUp\hunterDB.bak' WITH NOFORMAT, NOINIT,
NAME = N'Hunter-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO