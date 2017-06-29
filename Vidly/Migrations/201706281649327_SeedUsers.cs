namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(
@"  INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'57e86130-6a54-4065-9b77-ae8c907b82a4', N'admin@vidly.com', 0, N'AB7Y784LOpc58FCQalwy8TMBMouQiLgTE6UbdR2mxQsyc6xAnffKhtEdlWY9xaHHIw==', N'4433bab1-1f0e-4496-93c3-5fce924f2657', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6a7f9180-68d1-49da-bbae-cdbedd73a939', N'guest@vidly.com', 0, N'AHr4K+/7ggX8BmQi5uWWJAGl8z9xxSutAk9mEEbibQbpgOeMyZxpb4MgI/2yUXb1EA==', N'278bdbaa-0a17-4e61-9ff6-9dfe3b2d06e4', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
    
    INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'849a0e00-5f5b-4598-b1f8-b99e8910c2a8', N'CanManageMovies')
    
    INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'57e86130-6a54-4065-9b77-ae8c907b82a4', N'849a0e00-5f5b-4598-b1f8-b99e8910c2a8')


"
);
        }

        public override void Down()
        {
        }
    }
}
