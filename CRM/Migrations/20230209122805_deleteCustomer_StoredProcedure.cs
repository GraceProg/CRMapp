using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class deleteCustomer_StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
IF EXISTS(SELECT* FROM sys.objects WHERE type = 'P' AND name = 'DeleteCustomer')
DROP PROCEDURE DeleteCustomer
GO

CREATE PROCEDURE DeleteCustomer
@CustomerNumber int
AS
    DELETE FROM CUSTOMERS WHERE NUMBER = @CustomerNumber
GO
";
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
