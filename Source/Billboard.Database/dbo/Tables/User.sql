CREATE TABLE [dbo].[User] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Username]    NVARCHAR (100) NOT NULL,
    [Password]    NVARCHAR (250) NOT NULL,
    [DisplayName] NVARCHAR (100) NOT NULL,
    [Email]       NVARCHAR (250) NOT NULL,
    [CreateDate]  DATETIME2 (7)  CONSTRAINT [DF_User_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [TimezoneId]  INT            NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

