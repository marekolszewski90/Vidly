SET IDENTITY_INSERT [dbo].[Customers] ON
INSERT INTO [dbo].[Customers] ([Id], [Name], [IsSubscribedToNewsletter], [MembershipTypeId], [Birthdate]) VALUES (1, N'Jack Nicholson', 0, 1, NULL)
INSERT INTO [dbo].[Customers] ([Id], [Name], [IsSubscribedToNewsletter], [MembershipTypeId], [Birthdate]) VALUES (2, N'Leonardo DiCaprio', 1, 2, N'1974-11-11 00:00:00')
INSERT INTO [dbo].[Customers] ([Id], [Name], [IsSubscribedToNewsletter], [MembershipTypeId], [Birthdate]) VALUES (3, N'Kevin Spacey', 0, 3, N'1959-07-26 00:00:00')
INSERT INTO [dbo].[Customers] ([Id], [Name], [IsSubscribedToNewsletter], [MembershipTypeId], [Birthdate]) VALUES (4, N'Al Pacino', 1, 4, N'1940-04-25 00:00:00')
INSERT INTO [dbo].[Customers] ([Id], [Name], [IsSubscribedToNewsletter], [MembershipTypeId], [Birthdate]) VALUES (5, N'Tom Hanks', 1, 1, NULL)
SET IDENTITY_INSERT [dbo].[Customers] OFF
