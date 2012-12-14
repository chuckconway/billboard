CREATE TABLE [dbo].[Message] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [From]          NVARCHAR (50)   NOT NULL,
    [To]            NVARCHAR (50)   NOT NULL,
    [Body]          NVARCHAR (4000) NOT NULL,
    [Received]      DATETIME        NULL,
    [CreateTime]    DATETIME        CONSTRAINT [DF_Message_CreateTime] DEFAULT (getutcdate()) NOT NULL,
    [AccountSID]    NVARCHAR (50)   NULL,
    [ToCity]        NVARCHAR (50)   NULL,
    [SmsSid]        NVARCHAR (50)   NULL,
    [FromCountry]   NVARCHAR (50)   NULL,
    [ToCountry]     NVARCHAR (50)   NULL,
    [SmsMessageSid] NVARCHAR (50)   NULL,
    [ApiVersion]    NVARCHAR (50)   NULL,
    [FromState]     NVARCHAR (50)   NULL,
    [ToZip]         NVARCHAR (10)   NULL,
    [ToState]       NVARCHAR (50)   NULL
);

