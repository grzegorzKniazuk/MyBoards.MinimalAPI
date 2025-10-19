using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoards.MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class TagSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.InsertData(
                table: "WorkItemTags",
                column: "Value",
                value: "Web"
            );
            
            migrationBuilder.InsertData(
                table: "WorkItemTags",
                column: "Value",
                value: "UI"
            );
            
            migrationBuilder.InsertData(
                table: "WorkItemTags",
                column: "Value",
                value: "Desktop"
            );
            
            migrationBuilder.InsertData(
                table: "WorkItemTags",
                column: "Value",
                value: "API"
            );
            
            migrationBuilder.InsertData(
                table: "WorkItemTags",
                column: "Value",
                value: "Service"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkItemTags",
                keyColumn: "Value",
                keyValue: "Web"
            );
            
            migrationBuilder.DeleteData(
                table: "WorkItemTags",
                keyColumn: "Value",
                keyValue: "UI"
            );
            
            migrationBuilder.DeleteData(
                table: "WorkItemTags",
                keyColumn: "Value",
                keyValue: "Desktop"
            );
            
            migrationBuilder.DeleteData(
                table: "WorkItemTags",
                keyColumn: "Value",
                keyValue: "API"
            );
            
            migrationBuilder.DeleteData(
                table: "WorkItemTags",
                keyColumn: "Value",
                keyValue: "Service"
            );
        }
    }
}
