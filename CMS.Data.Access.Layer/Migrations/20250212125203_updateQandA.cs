using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Access.Layer.Migrations
{
    /// <inheritdoc />
    public partial class updateQandA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAndAnswers_Doctors_DoctorId",
                schema: "QuestionAndAnswer",
                table: "QuestionAndAnswers");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                schema: "QuestionAndAnswer",
                table: "QuestionAndAnswers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAndAnswers_Doctors_DoctorId",
                schema: "QuestionAndAnswer",
                table: "QuestionAndAnswers",
                column: "DoctorId",
                principalSchema: "Clinic",
                principalTable: "Doctors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAndAnswers_Doctors_DoctorId",
                schema: "QuestionAndAnswer",
                table: "QuestionAndAnswers");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                schema: "QuestionAndAnswer",
                table: "QuestionAndAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAndAnswers_Doctors_DoctorId",
                schema: "QuestionAndAnswer",
                table: "QuestionAndAnswers",
                column: "DoctorId",
                principalSchema: "Clinic",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
