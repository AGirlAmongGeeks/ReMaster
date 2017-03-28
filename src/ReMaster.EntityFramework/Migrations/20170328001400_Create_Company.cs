using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReMaster.EntityFramework.Migrations
{
    public partial class Create_Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddessAnomaly = table.Column<string>(nullable: true),
                    BuildingNo = table.Column<string>(nullable: true),
                    CeidgId = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    NIP = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OwnerFirstName = table.Column<string>(nullable: true),
                    OwnerLastName = table.Column<string>(nullable: true),
                    PKDCodes = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PostalCity = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Regon = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Voivodeship = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
