using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ordination_api.Migrations
{
    public partial class AddDoserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dosis_Ordinationer_DagligSkævOrdinationId",
                table: "Dosis");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordinationer_Dosis_AftenDosisDosisId",
                table: "Ordinationer");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordinationer_Dosis_MiddagDosisDosisId",
                table: "Ordinationer");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordinationer_Dosis_MorgenDosisDosisId",
                table: "Ordinationer");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordinationer_Dosis_NatDosisDosisId",
                table: "Ordinationer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dosis",
                table: "Dosis");

            migrationBuilder.RenameTable(
                name: "Dosis",
                newName: "Doser");

            migrationBuilder.RenameColumn(
                name: "DagligSkævOrdinationId",
                table: "Doser",
                newName: "DagligSkævId");

            migrationBuilder.RenameIndex(
                name: "IX_Dosis_DagligSkævOrdinationId",
                table: "Doser",
                newName: "IX_Doser_DagligSkævId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doser",
                table: "Doser",
                column: "DosisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doser_Ordinationer_DagligSkævId",
                table: "Doser",
                column: "DagligSkævId",
                principalTable: "Ordinationer",
                principalColumn: "OrdinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordinationer_Doser_AftenDosisDosisId",
                table: "Ordinationer",
                column: "AftenDosisDosisId",
                principalTable: "Doser",
                principalColumn: "DosisId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordinationer_Doser_MiddagDosisDosisId",
                table: "Ordinationer",
                column: "MiddagDosisDosisId",
                principalTable: "Doser",
                principalColumn: "DosisId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordinationer_Doser_MorgenDosisDosisId",
                table: "Ordinationer",
                column: "MorgenDosisDosisId",
                principalTable: "Doser",
                principalColumn: "DosisId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordinationer_Doser_NatDosisDosisId",
                table: "Ordinationer",
                column: "NatDosisDosisId",
                principalTable: "Doser",
                principalColumn: "DosisId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doser_Ordinationer_DagligSkævId",
                table: "Doser");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordinationer_Doser_AftenDosisDosisId",
                table: "Ordinationer");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordinationer_Doser_MiddagDosisDosisId",
                table: "Ordinationer");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordinationer_Doser_MorgenDosisDosisId",
                table: "Ordinationer");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordinationer_Doser_NatDosisDosisId",
                table: "Ordinationer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doser",
                table: "Doser");

            migrationBuilder.RenameTable(
                name: "Doser",
                newName: "Dosis");

            migrationBuilder.RenameColumn(
                name: "DagligSkævId",
                table: "Dosis",
                newName: "DagligSkævOrdinationId");

            migrationBuilder.RenameIndex(
                name: "IX_Doser_DagligSkævId",
                table: "Dosis",
                newName: "IX_Dosis_DagligSkævOrdinationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dosis",
                table: "Dosis",
                column: "DosisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dosis_Ordinationer_DagligSkævOrdinationId",
                table: "Dosis",
                column: "DagligSkævOrdinationId",
                principalTable: "Ordinationer",
                principalColumn: "OrdinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordinationer_Dosis_AftenDosisDosisId",
                table: "Ordinationer",
                column: "AftenDosisDosisId",
                principalTable: "Dosis",
                principalColumn: "DosisId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordinationer_Dosis_MiddagDosisDosisId",
                table: "Ordinationer",
                column: "MiddagDosisDosisId",
                principalTable: "Dosis",
                principalColumn: "DosisId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordinationer_Dosis_MorgenDosisDosisId",
                table: "Ordinationer",
                column: "MorgenDosisDosisId",
                principalTable: "Dosis",
                principalColumn: "DosisId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordinationer_Dosis_NatDosisDosisId",
                table: "Ordinationer",
                column: "NatDosisDosisId",
                principalTable: "Dosis",
                principalColumn: "DosisId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
