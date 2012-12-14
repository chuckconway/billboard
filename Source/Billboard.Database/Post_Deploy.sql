

SET IDENTITY_INSERT [dbo].[Timezone] ON 

GO
INSERT [dbo].[Timezone] ([Id], [Name], [OffSetHour], [OffSetMinutes]) VALUES (1, N'Pacific Standard Time', -8, 0)
GO
INSERT [dbo].[Timezone] ([Id], [Name], [OffSetHour], [OffSetMinutes]) VALUES (2, N'Alaskan Standard Time', -9, 0)
GO
INSERT [dbo].[Timezone] ([Id], [Name], [OffSetHour], [OffSetMinutes]) VALUES (3, N'Mountain Standard Time', -7, 0)
GO
INSERT [dbo].[Timezone] ([Id], [Name], [OffSetHour], [OffSetMinutes]) VALUES (4, N'Central Standard Time', -6, 0)
GO
INSERT [dbo].[Timezone] ([Id], [Name], [OffSetHour], [OffSetMinutes]) VALUES (5, N'Eastern Standard Time', -5, 0)
GO
INSERT [dbo].[Timezone] ([Id], [Name], [OffSetHour], [OffSetMinutes]) VALUES (6, N'Hawaiian Standard Time', -10, 0)
GO
SET IDENTITY_INSERT [dbo].[Timezone] OFF