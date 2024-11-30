﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CafeEmployeeManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cafes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Logo = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cafes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<int>(type: "integer", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    CafeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Cafes_CafeId",
                        column: x => x.CafeId,
                        principalTable: "Cafes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cafes",
                columns: new[] { "Id", "CreatedDate", "Description", "Location", "Logo", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("3e58d4dd-3d2a-422f-a4c0-e13758fdc7ef"), new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Amazing coffe cafe", "45 Merlion Lane, #12-34, Singapore 098765", "/img/mocha_muse.jpg", "Mocha Muse", new DateTime(2010, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cb93d19a-4019-4770-b4f5-5787bd3e7da7"), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Coffe heaven", "123 Orchid Avenue, #05-67, Singapore 567890", "/img/brew_heaven.jpg", "Brew Heaven", new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CafeId", "CreatedDate", "Email", "Gender", "Name", "PhoneNumber", "UpdatedDate" },
                values: new object[,]
                {
                    { "UIAbcDEfg", new Guid("cb93d19a-4019-4770-b4f5-5787bd3e7da7"), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "john.doe@example.com", 1, "John Doe", 98881111, new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "UIArfDEfg", new Guid("cb93d19a-4019-4770-b4f5-5787bd3e7da7"), new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), "jane.doe@example.com", 2, "Jane Doe", 98881221, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "UIBrfrEfg", new Guid("3e58d4dd-3d2a-422f-a4c0-e13758fdc7ef"), new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "steve.smith@example.com", 1, "Steve Smith", 88855221, new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CafeId",
                table: "Employees",
                column: "CafeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Cafes");
        }
    }
}
