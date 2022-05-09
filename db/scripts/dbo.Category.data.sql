SET IDENTITY_INSERT [dbo].[Category] ON

delete [dbo].[Category]

INSERT INTO [dbo].[Category] ([Id], [Name], [Description]) VALUES (1, 'Soda', NULL)
INSERT INTO [dbo].[Category] ([Id], [Name], [Description]) VALUES (2, 'Food', NULL)
INSERT INTO [dbo].[Category] ([Id], [Name], [Description]) VALUES (3, 'Cleaning', NULL)
INSERT INTO [dbo].[Category] ([Id], [Name], [Description]) VALUES (4, 'Bathroom', NULL)
SET IDENTITY_INSERT [dbo].[Category] OFF
