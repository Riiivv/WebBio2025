using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBio2025.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class tirsdag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HallId",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Halls",
                columns: table => new
                {
                    HallId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallNumber = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halls", x => x.HallId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_HallId",
                table: "Persons",
                column: "HallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Halls_HallId",
                table: "Persons",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "HallId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Halls_HallId",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "Halls");

            migrationBuilder.DropIndex(
                name: "IX_Persons_HallId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "HallId",
                table: "Persons");
        }
    }
}
