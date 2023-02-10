using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class add_Calls_StoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CustomerCalls_CustomerNumber",
                table: "CustomerCalls",
                column: "CustomerNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCalls_Customers_CustomerNumber",
                table: "CustomerCalls",
                column: "CustomerNumber",
                principalTable: "Customers",
                principalColumn: "Number",
                onDelete: ReferentialAction.Cascade);

            var sql = @"
IF EXISTS(SELECT* FROM sys.objects WHERE type = 'P' AND name = 'GetCall')
DROP PROCEDURE GetCall
GO

CREATE PROCEDURE GetCall
@id uniqueidentifier
AS
    SELECT * FROM CustomerCalls WHERE Id = @id
GO




IF EXISTS(SELECT* FROM sys.objects WHERE type = 'P' AND name = 'SaveCall')
DROP PROCEDURE SaveCall
GO

CREATE PROCEDURE SaveCall
@Id uniqueidentifier
,@CustomerNumber int
,@TimeofCall DATETIME NULL
,@Notes VARCHAR(max) NULL
,@Subject VARCHAR(200) NULL

AS

declare @existingId uniqueIdentifier = null
select @existingId = id from CustomerCalls WHERE ID = @Id
IF @existingId IS NOT NULL 
BEGIN
UPDATE [dbo].CustomerCalls
   SET CustomerNumber = @CustomerNumber
      ,TimeofCall = @TimeofCall
      ,Notes = @Notes
      ,Subject = @Subject
 WHERE Id = @Id
END
ELSE 
BEGIN
	INSERT INTO [dbo].CustomerCalls
           (Id
           ,CustomerNumber
           ,TimeofCall
           ,Notes
           ,[Subject])
     VALUES
           (@Id
           ,@CustomerNumber
           ,@TimeofCall
           ,@Notes
           ,@Subject
		   )
END
SELECT @@ROWCOUNT 
GO


IF EXISTS(SELECT* FROM sys.objects WHERE type = 'P' AND name = 'DeleteCall')
DROP PROCEDURE DeleteCall
GO

CREATE PROCEDURE DeleteCall
@Id uniqueidentifier
AS
    DELETE FROM CustomerCalls WHERE Id = @Id
GO


DELETE FROM CustomerCalls

INSERT INTO [dbo].[CustomerCalls] ([Id] ,[CustomerNumber] ,[TimeofCall] ,[Notes] ,[Subject])  VALUES
(NewId(), 1, '2022-6-4 12:45', 'Called to complain about failing email server', 'failing email server')
,(NewId(), 2, '2022-11-4 12:01', 'Customer had challenges accessing the new website', 'Cannot access new website')
,(NewId(), 3, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 4, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 5, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 6, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 7, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 8, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 9, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 10, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 11, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 12, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 13, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 14, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 15, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 17, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 16, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 17, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 18, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 19, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 1, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 2, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 3, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 4, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 5, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 6, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 7, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 8, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 9, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 10, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 1, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 4, '2020-11-4 10:01', 'NOtes', 'Subject')
,(NewId(), 9, '2020-11-4 10:01', 'NOtes', 'Subject')
GO
";
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCalls_Customers_CustomerNumber",
                table: "CustomerCalls");

            migrationBuilder.DropIndex(
                name: "IX_CustomerCalls_CustomerNumber",
                table: "CustomerCalls");
        }
    }
}
