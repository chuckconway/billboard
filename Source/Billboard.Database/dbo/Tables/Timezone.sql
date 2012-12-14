CREATE TABLE [dbo].[Timezone] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (100) NOT NULL,
    [OffSetHour]    SMALLINT       NOT NULL,
    [OffSetMinutes] SMALLINT       NOT NULL,
    CONSTRAINT [PK_Timezone] PRIMARY KEY CLUSTERED ([Id] ASC)
);

