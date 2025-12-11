using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class pinformupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PinForms_Projects_ProjectId",
                table: "PinForms");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Portfolios_PortfolioId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Section1Data",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "Section2Data",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "SpecialistInputs",
                table: "PinForms");

            migrationBuilder.AddColumn<decimal>(
                name: "ApprovedCostUsd",
                table: "Projects",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "CompletionDate",
                table: "Projects",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationArea",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectLocationArea",
                table: "Projects",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Roadmap",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Projects",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainUnitNo",
                table: "Projects",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessImpactAnalysis",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PinForms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentMitigations",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateRegistered",
                table: "PinForms",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DrbApprovalDate",
                table: "PinForms",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DrbMeetingRef",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExecutiveSummary",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactsAndAssumptions",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinancialBenefitsAnalysis",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InterfaceDetails",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBreakIn",
                table: "PinForms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MtoNo",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpportunityCostNonImplementation",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProblemStatements",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectObjective",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProposalNo",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProposedProjectNo",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShutdownRemarks",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PinForms",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValueAtRisk",
                table: "PinForms",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectPinAttachments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PinId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliverableName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsAttached = table.Column<bool>(type: "boolean", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    IsBrownfieldHealthCheck = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPinAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPinAttachments_PinForms_PinId",
                        column: x => x.PinId,
                        principalTable: "PinForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPinCo2Screenings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PinId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScenarioDescription = table.Column<string>(type: "text", nullable: true),
                    ExpectedCo2ChangeTonnePerYear = table.Column<decimal>(type: "numeric", nullable: true),
                    PsuName = table.Column<string>(type: "text", nullable: true),
                    PsuSignature = table.Column<byte[]>(type: "bytea", nullable: true),
                    PsuDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ExpectedCostUsdPerYear = table.Column<decimal>(type: "numeric", nullable: true),
                    CteName = table.Column<string>(type: "text", nullable: true),
                    CteSignature = table.Column<byte[]>(type: "bytea", nullable: true),
                    CteDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Co2AbatementCostUsdPerTonne = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPinCo2Screenings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPinCo2Screenings_PinForms_PinId",
                        column: x => x.PinId,
                        principalTable: "PinForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPinInterfaceSignOffs",
                columns: table => new
                {
                    PinId = table.Column<Guid>(type: "uuid", nullable: false),
                    TfiName = table.Column<string>(type: "text", nullable: true),
                    TfiRef = table.Column<string>(type: "text", nullable: true),
                    TfiSignature = table.Column<byte[]>(type: "bytea", nullable: true),
                    TfiDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPinInterfaceSignOffs", x => x.PinId);
                    table.ForeignKey(
                        name: "FK_ProjectPinInterfaceSignOffs_PinForms_PinId",
                        column: x => x.PinId,
                        principalTable: "PinForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPinMtoScores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PinId = table.Column<Guid>(type: "uuid", nullable: false),
                    MtoScore = table.Column<string>(type: "text", nullable: true),
                    AssessorName = table.Column<string>(type: "text", nullable: true),
                    AssessorRef = table.Column<string>(type: "text", nullable: true),
                    AssessorSignature = table.Column<byte[]>(type: "bytea", nullable: true),
                    AssessmentDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPinMtoScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPinMtoScores_PinForms_PinId",
                        column: x => x.PinId,
                        principalTable: "PinForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPinRamAssessments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PinId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScenarioDescription = table.Column<string>(type: "text", nullable: true),
                    SeverityPeople = table.Column<byte>(type: "smallint", nullable: true),
                    SeverityAsset = table.Column<byte>(type: "smallint", nullable: true),
                    SeverityEnvironment = table.Column<byte>(type: "smallint", nullable: true),
                    SeverityReputation = table.Column<byte>(type: "smallint", nullable: true),
                    SeverityCommunity = table.Column<byte>(type: "smallint", nullable: true),
                    SeveritySecurity = table.Column<byte>(type: "smallint", nullable: true),
                    LikelihoodPeople = table.Column<string>(type: "text", nullable: true),
                    LikelihoodAsset = table.Column<string>(type: "text", nullable: true),
                    LikelihoodEnvironment = table.Column<string>(type: "text", nullable: true),
                    LikelihoodReputation = table.Column<string>(type: "text", nullable: true),
                    LikelihoodCommunity = table.Column<string>(type: "text", nullable: true),
                    LikelihoodSecurity = table.Column<string>(type: "text", nullable: true),
                    OverallRiskScore = table.Column<string>(type: "text", nullable: true),
                    HeadSafetyName = table.Column<string>(type: "text", nullable: true),
                    HeadSafetyRef = table.Column<string>(type: "text", nullable: true),
                    HeadSafetySignature = table.Column<byte[]>(type: "bytea", nullable: true),
                    HeadSafetyDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPinRamAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPinRamAssessments_PinForms_PinId",
                        column: x => x.PinId,
                        principalTable: "PinForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPinStakeholders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PinId = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    RefInd = table.Column<string>(type: "text", nullable: true),
                    Signature = table.Column<byte[]>(type: "bytea", nullable: true),
                    SignedDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPinStakeholders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPinStakeholders_PinForms_PinId",
                        column: x => x.PinId,
                        principalTable: "PinForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPinValueDrivers",
                columns: table => new
                {
                    PinId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValueDriverCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ValueDriverName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPinValueDrivers", x => new { x.PinId, x.ValueDriverCode });
                    table.ForeignKey(
                        name: "FK_ProjectPinValueDrivers_PinForms_PinId",
                        column: x => x.PinId,
                        principalTable: "PinForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPinAttachments_PinId",
                table: "ProjectPinAttachments",
                column: "PinId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPinCo2Screenings_PinId",
                table: "ProjectPinCo2Screenings",
                column: "PinId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPinInterfaceSignOffs_PinId",
                table: "ProjectPinInterfaceSignOffs",
                column: "PinId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPinMtoScores_PinId",
                table: "ProjectPinMtoScores",
                column: "PinId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPinRamAssessments_PinId",
                table: "ProjectPinRamAssessments",
                column: "PinId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPinStakeholders_PinId",
                table: "ProjectPinStakeholders",
                column: "PinId");

            migrationBuilder.AddForeignKey(
                name: "FK_PinForms_Projects_ProjectId",
                table: "PinForms",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Portfolios_PortfolioId",
                table: "Projects",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PinForms_Projects_ProjectId",
                table: "PinForms");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Portfolios_PortfolioId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectPinAttachments");

            migrationBuilder.DropTable(
                name: "ProjectPinCo2Screenings");

            migrationBuilder.DropTable(
                name: "ProjectPinInterfaceSignOffs");

            migrationBuilder.DropTable(
                name: "ProjectPinMtoScores");

            migrationBuilder.DropTable(
                name: "ProjectPinRamAssessments");

            migrationBuilder.DropTable(
                name: "ProjectPinStakeholders");

            migrationBuilder.DropTable(
                name: "ProjectPinValueDrivers");

            migrationBuilder.DropColumn(
                name: "ApprovedCostUsd",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "LocationArea",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectLocationArea",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Roadmap",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TrainUnitNo",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BusinessImpactAnalysis",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "CurrentMitigations",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "DateRegistered",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "DrbApprovalDate",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "DrbMeetingRef",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "ExecutiveSummary",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "FactsAndAssumptions",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "FinancialBenefitsAnalysis",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "InterfaceDetails",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "IsBreakIn",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "MtoNo",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "OpportunityCostNonImplementation",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "ProblemStatements",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "ProjectObjective",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "ProposalNo",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "ProposedProjectNo",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "ShutdownRemarks",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PinForms");

            migrationBuilder.DropColumn(
                name: "ValueAtRisk",
                table: "PinForms");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<JsonDocument>(
                name: "Section1Data",
                table: "PinForms",
                type: "jsonb",
                nullable: false);

            migrationBuilder.AddColumn<JsonDocument>(
                name: "Section2Data",
                table: "PinForms",
                type: "jsonb",
                nullable: false);

            migrationBuilder.AddColumn<JsonDocument>(
                name: "SpecialistInputs",
                table: "PinForms",
                type: "jsonb",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_PinForms_Projects_ProjectId",
                table: "PinForms",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Portfolios_PortfolioId",
                table: "Projects",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
