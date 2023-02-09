using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class addGetCustomerStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "CustomerCalls",
                type: "nvarchar(max)",
                nullable: true);

            var sql = @"IF EXISTS(SELECT* FROM sys.objects WHERE type = 'P' AND name = 'GetCustomer')
DROP PROCEDURE GetCustomer
GO

CREATE PROCEDURE GetCustomer
@CustomerNumber int
AS
    SELECT * FROM CUSTOMERS WHERE NUMBER = @CustomerNumber
GO




IF EXISTS(SELECT* FROM sys.objects WHERE type = 'P' AND name = 'SaveCustomer')
DROP PROCEDURE SaveCustomer
GO

CREATE PROCEDURE SaveCustomer
@Name VARCHAR(100)
,@SurName VARCHAR(100)
,@Number int
,@Address VARCHAR(100) NULL
,@PostalCode VARCHAR(100) NULL
,@Country VARCHAR(100) NULL
,@DateOfBirth DATETIME NULL
,@UserId VARCHAR(100) NULL

AS

IF @Number > 0 
BEGIN
UPDATE [dbo].[Customers]
   SET [Name] = @Name
      ,[SurName] = @SurName
      ,[Address] = @Address
      ,[PostalCode] = @PostalCode
      ,[Country] = @Country
      ,[DateOfBirth] = @DateOfBirth
      ,[UserId] = @UserId
 WHERE NUMBER = @Number
END
ELSE 
BEGIN
	INSERT INTO [dbo].[Customers]
           ([Name]
           ,[SurName]
           ,[Address]
           ,[PostalCode]
           ,[Country]
           ,[DateOfBirth]
           ,[UserId])
     VALUES
           (@Name
           ,@SurName
           ,@Address
           ,@PostalCode
           ,@Country
           ,@DateOfBirth
           ,@UserId
		   )
END
SELECT @@ROWCOUNT 
GO

";
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "CustomerCalls");
        }
    }
}
