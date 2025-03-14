using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Access.Layer.Migrations
{
    /// <inheritdoc />
    public partial class addPharmacySection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PharmacyOrders_PharmacyDeliveryRepresentatives_PharmacyDeliveryRepresentativeId",
                schema: "Pharmacy",
                table: "PharmacyOrders");

            migrationBuilder.DropColumn(
                name: "OrderCount",
                schema: "Pharmacy",
                table: "PharmacyOrders");

            migrationBuilder.DropColumn(
                name: "Logo",
                schema: "Pharmacy",
                table: "MedicineManufactories");

            migrationBuilder.AlterColumn<int>(
                name: "PharmacyDeliveryRepresentativeId",
                schema: "Pharmacy",
                table: "PharmacyOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PharmacyCustomerId",
                schema: "Pharmacy",
                table: "PharmacyOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                schema: "Pharmacy",
                table: "PharmacyOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BloodType",
                schema: "Pharmacy",
                table: "PharmacyDeliveryRepresentatives",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                schema: "Pharmacy",
                table: "PharmacyCustomers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Img",
                schema: "Pharmacy",
                table: "PharmacyCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "Pharmacy",
                table: "Medicines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "Pharmacy",
                table: "MedicineManufactories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                schema: "Pharmacy",
                table: "MedicineManufactories",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Pharmacy",
                table: "MedicineManufactories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyCustomers_ApplicationUserId",
                schema: "Pharmacy",
                table: "PharmacyCustomers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PharmacyCustomers_AspNetUsers_ApplicationUserId",
                schema: "Pharmacy",
                table: "PharmacyCustomers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PharmacyOrders_PharmacyDeliveryRepresentatives_PharmacyDeliveryRepresentativeId",
                schema: "Pharmacy",
                table: "PharmacyOrders",
                column: "PharmacyDeliveryRepresentativeId",
                principalSchema: "Pharmacy",
                principalTable: "PharmacyDeliveryRepresentatives",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PharmacyCustomers_AspNetUsers_ApplicationUserId",
                schema: "Pharmacy",
                table: "PharmacyCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_PharmacyOrders_PharmacyDeliveryRepresentatives_PharmacyDeliveryRepresentativeId",
                schema: "Pharmacy",
                table: "PharmacyOrders");

            migrationBuilder.DropIndex(
                name: "IX_PharmacyCustomers_ApplicationUserId",
                schema: "Pharmacy",
                table: "PharmacyCustomers");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                schema: "Pharmacy",
                table: "PharmacyOrders");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                schema: "Pharmacy",
                table: "PharmacyCustomers");

            migrationBuilder.DropColumn(
                name: "Img",
                schema: "Pharmacy",
                table: "PharmacyCategories");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Pharmacy",
                table: "Medicines");

            migrationBuilder.AlterColumn<int>(
                name: "PharmacyDeliveryRepresentativeId",
                schema: "Pharmacy",
                table: "PharmacyOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PharmacyCustomerId",
                schema: "Pharmacy",
                table: "PharmacyOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderCount",
                schema: "Pharmacy",
                table: "PharmacyOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "BloodType",
                schema: "Pharmacy",
                table: "PharmacyDeliveryRepresentatives",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "Pharmacy",
                table: "MedicineManufactories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                schema: "Pharmacy",
                table: "MedicineManufactories",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Pharmacy",
                table: "MedicineManufactories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                schema: "Pharmacy",
                table: "MedicineManufactories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PharmacyOrders_PharmacyDeliveryRepresentatives_PharmacyDeliveryRepresentativeId",
                schema: "Pharmacy",
                table: "PharmacyOrders",
                column: "PharmacyDeliveryRepresentativeId",
                principalSchema: "Pharmacy",
                principalTable: "PharmacyDeliveryRepresentatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
