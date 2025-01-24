using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCVideoGuide.Migrations
{
    /// <inheritdoc />
    public partial class BlogCategoryJoiningTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Comments_CommentId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_CommentId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogId",
                table: "Comments",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Blogs_BlogId",
                table: "Comments",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Blogs_BlogId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_BlogId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CommentId",
                table: "Blogs",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Comments_CommentId",
                table: "Blogs",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }
    }
}
