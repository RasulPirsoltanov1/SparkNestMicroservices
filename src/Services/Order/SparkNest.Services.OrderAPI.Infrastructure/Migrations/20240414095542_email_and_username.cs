using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SparkNest.Services.OrderAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class email_and_username : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "ordering",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "ordering",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "ordering",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "ordering",
                table: "Orders");
        }
    }
}
