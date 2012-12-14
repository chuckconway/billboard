CREATE TABLE [dbo].[Alias] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (150) NOT NULL,
    [Number]  NVARCHAR (50)  NULL,
    [EventId] INT            NOT NULL,
    CONSTRAINT [PK_Alias] PRIMARY KEY CLUSTERED ([Id] ASC)
);

