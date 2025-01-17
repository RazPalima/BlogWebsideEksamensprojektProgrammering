using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCVideoGuide.Migrations
{
    /// <inheritdoc />
    public partial class VideoDbCommentFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CommentId",
                table: "Blogs",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Products_CommentId",
                table: "Blogs",
                column: "CommentId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Products_CommentId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_CommentId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Blogs");
        }
    }
}
