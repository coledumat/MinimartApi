﻿SET IDENTITY_INSERT [dbo].[Customer] ON
INSERT INTO [dbo].[Customer] ([Id], [Name], [LastName], [Email], [Adress], [Telephone], [CreationDate], [LastLoginDate]) VALUES (1, N'John                                                                                                                                                                                                    ', N'Smith                                                                                                                                                                                                   ', N'john.smith@gmail.com                              ', N'345 Albert St Unit No G-06, Ottawa, ON K1R 7X7, Canadá                                                                                                                                                  ', CAST(6134221317 AS Decimal(10, 0)), N'2022-05-09 00:00:00', N'2022-05-12 00:00:00')
INSERT INTO [dbo].[Customer] ([Id], [Name], [LastName], [Email], [Adress], [Telephone], [CreationDate], [LastLoginDate]) VALUES (4, N'Justin                                                                                                                                                                                                  ', N'Trudeau                                                                                                                                                                                                 ', N'Justin .Trudeau@gmail.com                         ', N'NULL	24 Sussex Dr, Ottawa, ON K1M 1M4, Canadá                                                                                                                                                           ', CAST(6123443242 AS Decimal(10, 0)), N'2022-05-11 00:00:00', N'2022-05-12 00:00:00')
INSERT INTO [dbo].[Customer] ([Id], [Name], [LastName], [Email], [Adress], [Telephone], [CreationDate], [LastLoginDate]) VALUES (8, N' Michael                                                                                                                                                                                                ', N'Buble                                                                                                                                                                                                   ', N'mb.@gmail.com                                     ', N'81 Metcalfe St, Ottawa, ON K1P 6K7, Canadá                                                                                                                                                              ', CAST(6136787788 AS Decimal(10, 0)), N'2022-01-01 00:00:00', NULL)
SET IDENTITY_INSERT [dbo].[Customer] OFF
