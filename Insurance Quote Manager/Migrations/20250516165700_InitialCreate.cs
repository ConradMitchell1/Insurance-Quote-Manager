using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurance_Quote_Manager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientName = table.Column<string>(type: "TEXT", nullable: false),
                    ClientAge = table.Column<int>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    IsSmoker = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasChronicIllness = table.Column<bool>(type: "INTEGER", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    PropertyType = table.Column<string>(type: "TEXT", nullable: true),
                    PropertyAge = table.Column<int>(type: "INTEGER", nullable: true),
                    ConstructionType = table.Column<string>(type: "TEXT", nullable: true),
                    PolicyType = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CoverDurationMonths = table.Column<int>(type: "INTEGER", nullable: false),
                    BasePremium = table.Column<decimal>(type: "TEXT", nullable: false),
                    RiskFactor = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPremium = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotes");
        }
    }
}
