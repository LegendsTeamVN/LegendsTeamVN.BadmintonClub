using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegendsTeamVN.BadmintonClub.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourtPricings_Courts_CourtId",
                table: "CourtPricings");

            migrationBuilder.RenameColumn(
                name: "CourtId",
                table: "CourtPricings",
                newName: "VenueId");

            migrationBuilder.RenameIndex(
                name: "IX_CourtPricings_CourtId",
                table: "CourtPricings",
                newName: "IX_CourtPricings_VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourtPricings_Venues_VenueId",
                table: "CourtPricings",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourtPricings_Venues_VenueId",
                table: "CourtPricings");

            migrationBuilder.RenameColumn(
                name: "VenueId",
                table: "CourtPricings",
                newName: "CourtId");

            migrationBuilder.RenameIndex(
                name: "IX_CourtPricings_VenueId",
                table: "CourtPricings",
                newName: "IX_CourtPricings_CourtId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourtPricings_Courts_CourtId",
                table: "CourtPricings",
                column: "CourtId",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
