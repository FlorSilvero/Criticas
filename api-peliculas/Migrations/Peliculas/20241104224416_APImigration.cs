using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_peliculas.Migrations.Peliculas
{
    /// <inheritdoc />
    public partial class APImigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Criticas_ApplicationUser_UsuarioId",
                table: "Criticas");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_Criticas_PeliculaId",
                table: "Criticas");

            migrationBuilder.DropIndex(
                name: "IX_Criticas_UsuarioId_PeliculaId",
                table: "Criticas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Criticas");

            migrationBuilder.CreateIndex(
                name: "IX_Criticas_PeliculaId",
                table: "Criticas",
                column: "PeliculaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Criticas_PeliculaId",
                table: "Criticas");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Criticas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Criticas_PeliculaId",
                table: "Criticas",
                column: "PeliculaId");

            migrationBuilder.CreateIndex(
                name: "IX_Criticas_UsuarioId_PeliculaId",
                table: "Criticas",
                columns: new[] { "UsuarioId", "PeliculaId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Criticas_ApplicationUser_UsuarioId",
                table: "Criticas",
                column: "UsuarioId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
