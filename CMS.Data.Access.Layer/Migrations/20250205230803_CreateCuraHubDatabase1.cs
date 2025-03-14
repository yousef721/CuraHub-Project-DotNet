using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Access.Layer.Migrations
{
    /// <inheritdoc />
    public partial class CreateCuraHubDatabase1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalPrescriptions_Doctors_DoctorId",
                schema: "Clinic",
                table: "MedicalPrescriptions");

            migrationBuilder.DropIndex(
                name: "IX_MedicalPrescriptions_DoctorId",
                schema: "Clinic",
                table: "MedicalPrescriptions");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                schema: "Clinic",
                table: "MedicalPrescriptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                schema: "Clinic",
                table: "MedicalPrescriptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalPrescriptions_DoctorId",
                schema: "Clinic",
                table: "MedicalPrescriptions",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalPrescriptions_Doctors_DoctorId",
                schema: "Clinic",
                table: "MedicalPrescriptions",
                column: "DoctorId",
                principalSchema: "Clinic",
                principalTable: "Doctors",
                principalColumn: "Id");
        }
    }
}
