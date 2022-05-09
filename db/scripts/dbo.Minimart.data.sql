SET IDENTITY_INSERT [dbo].[MiniMart] ON

delete [dbo].[MiniMart]

INSERT INTO [dbo].[MiniMart] ([Id], [Name], [Adress], [Logo]) VALUES (1, N'COCO Downtown', N'340 Albert St Unit No G-06, Ottawa, ON K1R 7X7, Canadá', NULL)
INSERT INTO [dbo].[MiniMart] ([Id], [Name], [Adress], [Logo]) VALUES (2, N'COCO Mall', N'250 City Centre Ave, Ottawa, ON K1R 6K7, Canadá', NULL)
INSERT INTO [dbo].[MiniMart] ([Id], [Name], [Adress], [Logo]) VALUES (3, N'COCO Bay', N'2277 Riverside Dr., Ottawa, ON K1H 7X6, Canadá', NULL)
SET IDENTITY_INSERT [dbo].[MiniMart] OFF
