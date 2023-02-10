using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class populate_customer_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
DELETE FROM CUSTOMERS

SET IDENTITY_INSERT [dbo].[Customers] ON 
GO

INSERT [dbo].[Customers] ([Number], [Name], [SurName], [Address], [PostalCode], [Country], [DateOfBirth], [UserId]) VALUES 
(1, N'Titus', N'Yusinyu', N'Mile 4 Bamenda', N'1', N'Cameroon', CAST(N'1990-09-11T00:00:00.0000000' AS DateTime2), N'sdfsd'),
(2, N'Ella', N'Navti', N'Limbe', N'1', N'Cameroon', CAST(N'1990-09-11T00:00:00.0000000' AS DateTime2), N'sdfsd'),
(3, N'Peter', N'Faraday', N'Molyko Buea', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(4, N'Paul', N'Bush', N'Douala', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(5, N'Suhlivan', N'Nelson', N'Yaounde', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(6, N'Janet', N'Ghosh', N'Bafoussam', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(7, N'Joy', N'Dicks', N'Maroua', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(8, N'Emmanuel', N'Treewood', N'Melong', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(9, N'Joseph', N'Suller', N'Kribi', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(10, N'Nicolas', N'Esterfield', N'Bekoko', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(11, N'Doris', N'Kenland', N'Ebolowa', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(12, N'Nahum', N'Greenwood', N'Edea', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(13, N'Thomas', N'Faraday', N'Mevele', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(14, N'Karl', N'Tinssa', N'Idenau', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(15, N'Emerick', N'Toh', N'Ngoundere', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(16, N'Marinette', N'Tallin', N'Meinganga', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(17, N'Veronica', N'Stellar', N'Bertoua', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(18, N'Sneed', N'Reed', N'Makenene', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds'),
(19, N'Samuel', N'Blackwood', N'Foumban', N'237', N'Cameroon', CAST(N'1999-05-03T00:00:00.0000000' AS DateTime2), N'sfds')

GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO

";
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
