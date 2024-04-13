using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedCloudinaryImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CloudinaryImageId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CloudinaryImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudinaryImages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_CloudinaryImageId",
                table: "Images",
                column: "CloudinaryImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_CloudinaryImages_CloudinaryImageId",
                table: "Images",
                column: "CloudinaryImageId",
                principalTable: "CloudinaryImages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_CloudinaryImages_CloudinaryImageId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "CloudinaryImages");

            migrationBuilder.DropIndex(
                name: "IX_Images_CloudinaryImageId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CloudinaryImageId",
                table: "Images");
        }
    }
}
