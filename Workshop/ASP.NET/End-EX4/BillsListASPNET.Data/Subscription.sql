CREATE TABLE [dbo].[Subscription]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GroupName] NVARCHAR(100) NOT NULL, 
    [WebHookUri] NVARCHAR(MAX) NOT NULL, 
    [Category] NVARCHAR(50) NULL
)
