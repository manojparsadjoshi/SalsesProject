using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Db.Migrations
{
    /// <inheritdoc />
    public partial class addingPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "purchaseMasterDetailModels",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "purchaseMasterDetailModels");
        }
    }
}
