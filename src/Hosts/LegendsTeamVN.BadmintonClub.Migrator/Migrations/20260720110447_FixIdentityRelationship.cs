using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegendsTeamVN.BadmintonClub.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class FixIdentityRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRoleClaims_AppRoles_AppRoleId",
                schema: "Identity",
                table: "AppRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserClaims_AppUsers_AppUserId",
                schema: "Identity",
                table: "AppUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserLogins_AppUsers_AppUserId",
                schema: "Identity",
                table: "AppUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_AppRoles_AppRoleId",
                schema: "Identity",
                table: "AppUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_AppUsers_AppUserId",
                schema: "Identity",
                table: "AppUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserTokens_AppUsers_AppUserId",
                schema: "Identity",
                table: "AppUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserTokens",
                schema: "Identity",
                table: "AppUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AppUserTokens_AppUserId",
                schema: "Identity",
                table: "AppUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserRoles",
                schema: "Identity",
                table: "AppUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AppUserRoles_AppRoleId",
                schema: "Identity",
                table: "AppUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AppUserRoles_AppUserId",
                schema: "Identity",
                table: "AppUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AppUserRoles_RoleId",
                schema: "Identity",
                table: "AppUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserLogins",
                schema: "Identity",
                table: "AppUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AppUserLogins_AppUserId",
                schema: "Identity",
                table: "AppUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AppUserLogins_UserId",
                schema: "Identity",
                table: "AppUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AppUserClaims_AppUserId",
                schema: "Identity",
                table: "AppUserClaims");

            migrationBuilder.DropIndex(
                name: "IX_AppRoleClaims_AppRoleId",
                schema: "Identity",
                table: "AppRoleClaims");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                schema: "Identity",
                table: "AppUserTokens");

            migrationBuilder.DropColumn(
                name: "AppRoleId",
                schema: "Identity",
                table: "AppUserRoles");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                schema: "Identity",
                table: "AppUserRoles");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                schema: "Identity",
                table: "AppUserLogins");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                schema: "Identity",
                table: "AppUserClaims");

            migrationBuilder.DropColumn(
                name: "AppRoleId",
                schema: "Identity",
                table: "AppRoleClaims");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Identity",
                table: "AppUserTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "Identity",
                table: "AppUserTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                schema: "Identity",
                table: "AppUserLogins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "Identity",
                table: "AppUserLogins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserTokens",
                schema: "Identity",
                table: "AppUserTokens",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserRoles",
                schema: "Identity",
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserLogins",
                schema: "Identity",
                table: "AppUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_UserId",
                schema: "Identity",
                table: "AppUserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserTokens",
                schema: "Identity",
                table: "AppUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserRoles",
                schema: "Identity",
                table: "AppUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AppUserRoles_UserId",
                schema: "Identity",
                table: "AppUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserLogins",
                schema: "Identity",
                table: "AppUserLogins");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Identity",
                table: "AppUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "Identity",
                table: "AppUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                schema: "Identity",
                table: "AppUserTokens",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AppRoleId",
                schema: "Identity",
                table: "AppUserRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                schema: "Identity",
                table: "AppUserRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                schema: "Identity",
                table: "AppUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "Identity",
                table: "AppUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                schema: "Identity",
                table: "AppUserLogins",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                schema: "Identity",
                table: "AppUserClaims",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AppRoleId",
                schema: "Identity",
                table: "AppRoleClaims",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserTokens",
                schema: "Identity",
                table: "AppUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserRoles",
                schema: "Identity",
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserLogins",
                schema: "Identity",
                table: "AppUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserTokens_AppUserId",
                schema: "Identity",
                table: "AppUserTokens",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_AppRoleId",
                schema: "Identity",
                table: "AppUserRoles",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_AppUserId",
                schema: "Identity",
                table: "AppUserRoles",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_RoleId",
                schema: "Identity",
                table: "AppUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLogins_AppUserId",
                schema: "Identity",
                table: "AppUserLogins",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLogins_UserId",
                schema: "Identity",
                table: "AppUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserClaims_AppUserId",
                schema: "Identity",
                table: "AppUserClaims",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRoleClaims_AppRoleId",
                schema: "Identity",
                table: "AppRoleClaims",
                column: "AppRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRoleClaims_AppRoles_AppRoleId",
                schema: "Identity",
                table: "AppRoleClaims",
                column: "AppRoleId",
                principalSchema: "Identity",
                principalTable: "AppRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserClaims_AppUsers_AppUserId",
                schema: "Identity",
                table: "AppUserClaims",
                column: "AppUserId",
                principalSchema: "Identity",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserLogins_AppUsers_AppUserId",
                schema: "Identity",
                table: "AppUserLogins",
                column: "AppUserId",
                principalSchema: "Identity",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_AppRoles_AppRoleId",
                schema: "Identity",
                table: "AppUserRoles",
                column: "AppRoleId",
                principalSchema: "Identity",
                principalTable: "AppRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_AppUsers_AppUserId",
                schema: "Identity",
                table: "AppUserRoles",
                column: "AppUserId",
                principalSchema: "Identity",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserTokens_AppUsers_AppUserId",
                schema: "Identity",
                table: "AppUserTokens",
                column: "AppUserId",
                principalSchema: "Identity",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }
    }
}
