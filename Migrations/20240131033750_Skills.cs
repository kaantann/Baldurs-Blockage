using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetrpg.Migrations
{
    /// <inheritdoc />
    public partial class Skills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkillID",
                table: "Characters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Damage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_SkillID",
                table: "Characters",
                column: "SkillID");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Skills_SkillID",
                table: "Characters",
                column: "SkillID",
                principalTable: "Skills",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Skills_SkillID",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Characters_SkillID",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "SkillID",
                table: "Characters");
        }
    }
}
