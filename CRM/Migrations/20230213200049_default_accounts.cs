using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class default_accounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
DELETE FROM ASPNETUSERS
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5e7bb61e-0f69-4b2f-b90c-35f6219288e4', N'employee@crm.com', N'employee@crm.com', N'employee@crm.com', N'employee@crm.com', 1, N'AQAAAAEAACcQAAAAECD86+YkAhh+7ynfqppnr+R39iUo7j5aIUF85//G+Y/a1DNaxuy2YUYSX/qM7e6sMQ==', N'JW3HSBLEQFTRJ4VD5RTKCKZX47TVHPYM', N'e4af438d-b700-409b-bea3-cbaa70aa2ddb', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'e61a1d95-c29e-430a-a079-7d9fbdcc72eb', N'manager@crm.com', N'manager@crm.com', N'manager@crm.com', N'manager@crm.com', 1, N'AQAAAAEAACcQAAAAENg1tNmALLDx5mgdZOBUlKw4Ep2MNi9ObbB92WAf8cMa7CU1hPfuvoAEfq5HmZiAOQ==', N'6HIRO56BG47TZ7L7HSXA64POGRGBVBHW', N'87bba292-33b4-407c-911d-fffb6c457ae4', NULL, 0, 0, NULL, 1, 0)


DELETE FROM [AspNetUserRoles]
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5e7bb61e-0f69-4b2f-b90c-35f6219288e4', N'606D5523-F271-4A31-8525-ACBABB992530')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e61a1d95-c29e-430a-a079-7d9fbdcc72eb', N'CA846514-AB41-4008-A5CE-09AC407FB112')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e61a1d95-c29e-430a-a079-7d9fbdcc72eb', N'606D5523-F271-4A31-8525-ACBABB992530')
GO";
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
