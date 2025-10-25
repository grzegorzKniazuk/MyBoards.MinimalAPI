using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoards.MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class TopAuthorsViewAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql(@"
            CREATE VIEW TopAuthorsView AS
            SELECT TOP 5 u.FullName, COUNT(*) as [WorkItemsCreated]
            FROM Users u
            JOIN WorkItems wi ON wi.AuthorId = u.Id
            GROUP BY u.Id, u.FullName
            ORDER BY [WorkItemsCreated] DESC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql(@"DROP VIEW IF EXISTS TopAuthorsView");
        }
    }
}
