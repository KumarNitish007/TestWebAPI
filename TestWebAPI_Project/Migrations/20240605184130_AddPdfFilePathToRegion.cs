﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWebAPI_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddPdfFilePathToRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PdfFilePath",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfFilePath",
                table: "Regions");
        }
    }
}
