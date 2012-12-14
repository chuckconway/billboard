CREATE TABLE [dbo].[Event] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (200)  NOT NULL,
    [StartTime]       DATETIME        NOT NULL,
    [EndTime]         DATETIME        NOT NULL,
    [Number]          NVARCHAR (50)   NULL,
    [UserId]          INT             NOT NULL,
    [Price]           MONEY           NULL,
    [TimezoneId]      INT             NULL,
    [Message]         NVARCHAR (2000) NULL,
    [FormattedNumber] NVARCHAR (50)   NULL,
    [NumberSid]       NVARCHAR (50)   NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([Id] ASC)
);

