using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Db.Migrations
{
    /// <inheritdoc />
    public partial class changedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "purchaseMasterModels",
                newName: "NetAmount");

            migrationBuilder.RenameColumn(
                name: "NetAmount",
                table: "purchaseMasterDetailModels",
                newName: "Amount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NetAmount",
                table: "purchaseMasterModels",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "purchaseMasterDetailModels",
                newName: "NetAmount");
        }
    }
}
