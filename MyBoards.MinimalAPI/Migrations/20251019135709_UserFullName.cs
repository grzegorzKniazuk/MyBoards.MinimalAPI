﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoards.MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserFullName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false
            );

            migrationBuilder.Sql(@"
                UPDATE Users
                SET FullName = FirstName + ' ' + LastName
            ");
            
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
            
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
            
            migrationBuilder.Sql(@"
                UPDATE Users
                SET 
                    FirstName = SUBSTRING(FullName, 1, CHARINDEX(' ', FullName) - 1),
                    LastName = SUBSTRING(FullName, CHARINDEX(' ', FullName) + 1, LEN(FullName))
                WHERE CHARINDEX(' ', FullName) > 0
            ");
            
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");
        }
    }
}
