using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace THD.Infrastructure.Migrations
{
    public partial class addclienttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LAST_NAME = table.Column<string>(nullable: true),
                    FIRST_NAME = table.Column<string>(nullable: true),
                    ACCT_NBR = table.Column<int>(nullable: false),
                    ADDRESS_1 = table.Column<string>(nullable: true),
                    ZIP = table.Column<string>(nullable: true),
                    TELEPHONE = table.Column<string>(nullable: true),
                    DATE_OPEN = table.Column<DateTime>(nullable: false),
                    SS_NUMBER = table.Column<int>(nullable: false),
                    PICTURE = table.Column<string>(nullable: true),
                    BIRTH_DATE = table.Column<DateTime>(nullable: false),
                    OCCUPATION = table.Column<string>(nullable: true),
                    RISK_LEVEL = table.Column<string>(nullable: true),
                    CITY = table.Column<string>(nullable: true),
                    STATE = table.Column<string>(nullable: true),
                    OBJECTIVES = table.Column<string>(nullable: true),
                    INTERESTS = table.Column<string>(nullable: true),
                    IMAGE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
