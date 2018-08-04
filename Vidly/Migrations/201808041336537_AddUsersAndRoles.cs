namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersAndRoles : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e2a92fb0-5477-49ca-ae52-ad93c164a20f', N'guest@vidly.com', 0, N'AGASxYF3i+R2GTzqPL58PbEIuID/euUBuc0fZE6WRnQM8b1yDEBASaRzc9YC2DwEFg==', N'f094556b-4e7d-42df-9e47-5e705be0a15d', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')");
            Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'391b1d7b-08a1-4baa-8e68-82b2ba1b9880', N'admin@vidly.com', 0, N'AKt0WRJwIoDjpULuK4CXcgC2DSu47HNENknGXbBwDS1k0V/hLv/3drxyKvi3h0Z+Rg==', N'a60c6977-dafb-4f19-a39d-9015dc7aacd7', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')");
            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4bb83f6d-ae19-411c-9a24-031fbc1d17f2', N'CanManageMovies')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'391b1d7b-08a1-4baa-8e68-82b2ba1b9880', N'4bb83f6d-ae19-411c-9a24-031fbc1d17f2')");
        }
        
        public override void Down()
        {
        }
    }
}
