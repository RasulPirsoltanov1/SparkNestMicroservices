using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SparkNest.Services.CommentAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class comment_photourl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Comments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Comments");
        }
    }
}
