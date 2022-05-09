SET IDENTITY_INSERT [dbo].[Product] ON

delete [dbo].[Product]

INSERT INTO [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (1, 'old Ice Tea', NULL, CAST(10.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (2, 'Coffee flavoured milk', NULL, CAST(10.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (3, 'Nuke-Cola', NULL, CAST(10.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (4, 'Sprute', NULL, CAST(11.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (5, 'Slurm', NULL, CAST(10.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (6, 'Diet Slurm', NULL, CAST(10.00 AS Decimal(10, 2)))


INSERT INTO [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (7, 'Salsa Cookies', NULL, CAST(10.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (8, 'Windmill Cookies', NULL, CAST(10.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (9, 'Garlic-o-bread 2000', NULL, CAST(10.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (10,'LACTEL bread', NULL, CAST(10.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (11,'Ravioloches x12', NULL, CAST(10.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (12,'Ravioloches x48', NULL, CAST(10.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (13,'Milanga ganga', NULL, CAST(10.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (14,'Milanga ganga napo', NULL, CAST(10.00 AS Decimal(10, 2)))

SET IDENTITY_INSERT [dbo].[Product] OFF
