using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net7.Migrations
{
    /// <inheritdoc />
    public partial class ColumnUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSkill_Skills_SkillId",
                table: "CharacterSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "Skill");

            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "CharacterSkill",
                newName: "SkillsId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterSkill_SkillId",
                table: "CharacterSkill",
                newName: "IX_CharacterSkill_SkillsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skill",
                table: "Skill",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSkill_Skill_SkillsId",
                table: "CharacterSkill",
                column: "SkillsId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSkill_Skill_SkillsId",
                table: "CharacterSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skill",
                table: "Skill");

            migrationBuilder.RenameTable(
                name: "Skill",
                newName: "Skills");

            migrationBuilder.RenameColumn(
                name: "SkillsId",
                table: "CharacterSkill",
                newName: "SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterSkill_SkillsId",
                table: "CharacterSkill",
                newName: "IX_CharacterSkill_SkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSkill_Skills_SkillId",
                table: "CharacterSkill",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
