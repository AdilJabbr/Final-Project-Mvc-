using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class brand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Brands");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Brands",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Brands",
                newName: "Image");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
