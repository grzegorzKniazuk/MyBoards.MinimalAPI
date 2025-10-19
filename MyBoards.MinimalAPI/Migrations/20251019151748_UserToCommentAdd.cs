using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoards.MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserToCommentAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "WorkItemComments");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "WorkItemComments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemComments_AuthorId",
                table: "WorkItemComments",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItemComments_Users_AuthorId",
                table: "WorkItemComments",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItemComments_Users_AuthorId",
                table: "WorkItemComments");

            migrationBuilder.DropIndex(
                name: "IX_WorkItemComments_AuthorId",
                table: "WorkItemComments");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "WorkItemComments");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "WorkItemComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
