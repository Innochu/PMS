using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class workflowinstance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowInstances_Projects_ProjectId1",
                table: "WorkflowInstances");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowInstances_ProjectId1",
                table: "WorkflowInstances");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "WorkflowInstances");

            migrationBuilder.DropColumn(
                name: "TempProjectId",
                table: "Projects");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "WorkflowInstances",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstances_ProjectId",
                table: "WorkflowInstances",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowInstances_Projects_ProjectId",
                table: "WorkflowInstances",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowInstances_Projects_ProjectId",
                table: "WorkflowInstances");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowInstances_ProjectId",
                table: "WorkflowInstances");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "WorkflowInstances",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId1",
                table: "WorkflowInstances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TempProjectId",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstances_ProjectId1",
                table: "WorkflowInstances",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowInstances_Projects_ProjectId1",
                table: "WorkflowInstances",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "PublicId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
