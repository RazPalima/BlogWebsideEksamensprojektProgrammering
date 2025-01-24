using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCVideoGuide.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDbNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommentText",
                table: "Comments",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "BlogText",
                table: "Blogs",
                newName: "Text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Comments",
                newName: "CommentText");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Blogs",
                newName: "BlogText");
        }
    }
}
