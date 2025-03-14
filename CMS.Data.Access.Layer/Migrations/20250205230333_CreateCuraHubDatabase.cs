using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Data.Access.Layer.Migrations
{
    /// <inheritdoc />
    public partial class CreateCuraHubDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Clinic");

            migrationBuilder.EnsureSchema(
                name: "MedicalAnalysisLab");

            migrationBuilder.EnsureSchema(
                name: "Pharmacy");

            migrationBuilder.EnsureSchema(
                name: "QuestionAndAnswer");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAnalysisLabCustomers",
                schema: "MedicalAnalysisLab",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloodType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    PersonalNationalIDNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PersonalNationalIDCard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DateOfbirth = table.Column<DateOnly>(type: "date", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAnalysisLabCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAnalysisLabs",
                schema: "MedicalAnalysisLab",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAnalysisLabs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicineManufactories",
                schema: "Pharmacy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Info = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineManufactories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                schema: "Clinic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastVisitDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PersonalNationalIDNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PersonalNationalIDCard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BloodType = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: true),
                    MedicalAnalysis = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacists",
                schema: "Pharmacy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDCard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false),
                    MedicalDegree = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PharmacyCategories",
                schema: "Pharmacy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacyCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PharmacyCustomers",
                schema: "Pharmacy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacyCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PharmacyDeliveryRepresentatives",
                schema: "Pharmacy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDCard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacyDeliveryRepresentatives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                schema: "Clinic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAnalysisLabBranches",
                schema: "MedicalAnalysisLab",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    AnalysisDuration = table.Column<TimeOnly>(type: "time", nullable: false),
                    MedicalAnalysisLabId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAnalysisLabBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalAnalysisLabBranches_MedicalAnalysisLabs_MedicalAnalysisLabId",
                        column: x => x.MedicalAnalysisLabId,
                        principalSchema: "MedicalAnalysisLab",
                        principalTable: "MedicalAnalysisLabs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAnalysisTests",
                schema: "MedicalAnalysisLab",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Duration = table.Column<TimeOnly>(type: "time", nullable: false),
                    MedicalAnalysisLabId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAnalysisTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalAnalysisTests_MedicalAnalysisLabs_MedicalAnalysisLabId",
                        column: x => x.MedicalAnalysisLabId,
                        principalSchema: "MedicalAnalysisLab",
                        principalTable: "MedicalAnalysisLabs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientHistories",
                schema: "Clinic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientHistories_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Clinic",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                schema: "Pharmacy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<double>(type: "float", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PharmacyCategoryId = table.Column<int>(type: "int", nullable: false),
                    MedicineManufactoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicines_MedicineManufactories_MedicineManufactoryId",
                        column: x => x.MedicineManufactoryId,
                        principalSchema: "Pharmacy",
                        principalTable: "MedicineManufactories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medicines_PharmacyCategories_PharmacyCategoryId",
                        column: x => x.PharmacyCategoryId,
                        principalSchema: "Pharmacy",
                        principalTable: "PharmacyCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PharmacyOrders",
                schema: "Pharmacy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quentity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    ShipmentStatus = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderCount = table.Column<int>(type: "int", nullable: false),
                    PharmacyCustomerId = table.Column<int>(type: "int", nullable: false),
                    PharmacyDeliveryRepresentativeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacyOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PharmacyOrders_PharmacyCustomers_PharmacyCustomerId",
                        column: x => x.PharmacyCustomerId,
                        principalSchema: "Pharmacy",
                        principalTable: "PharmacyCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PharmacyOrders_PharmacyDeliveryRepresentatives_PharmacyDeliveryRepresentativeId",
                        column: x => x.PharmacyDeliveryRepresentativeId,
                        principalSchema: "Pharmacy",
                        principalTable: "PharmacyDeliveryRepresentatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                schema: "Clinic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultationDuration = table.Column<double>(type: "float", nullable: false),
                    ConsultationFees = table.Column<double>(type: "float", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    userRatingCount = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MedicalLicense = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MedicalRegistration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MedicalIdentificationCard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDCard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false),
                    MedicalDegree = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalSchema: "Clinic",
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestDoctors",
                schema: "Clinic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultationDuration = table.Column<double>(type: "float", nullable: false),
                    ConsultationFees = table.Column<double>(type: "float", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    MedicalLicense = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MedicalRegistration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MedicalIdentificationCard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDCard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false),
                    MedicalDegree = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestDoctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestDoctors_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalSchema: "Clinic",
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAnalysisLabAppointments",
                schema: "MedicalAnalysisLab",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Appointment = table.Column<TimeOnly>(type: "time", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    MedicalAnalysisLabBranchId = table.Column<int>(type: "int", nullable: false),
                    MedicalAnalysisLabCustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAnalysisLabAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalAnalysisLabAppointments_MedicalAnalysisLabBranches_MedicalAnalysisLabBranchId",
                        column: x => x.MedicalAnalysisLabBranchId,
                        principalSchema: "MedicalAnalysisLab",
                        principalTable: "MedicalAnalysisLabBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalAnalysisLabAppointments_MedicalAnalysisLabCustomers_MedicalAnalysisLabCustomerId",
                        column: x => x.MedicalAnalysisLabCustomerId,
                        principalSchema: "MedicalAnalysisLab",
                        principalTable: "MedicalAnalysisLabCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAnalysisLabReceptionists",
                schema: "MedicalAnalysisLab",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalAnalysisLabBranchId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDCard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAnalysisLabReceptionists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalAnalysisLabReceptionists_MedicalAnalysisLabBranches_MedicalAnalysisLabBranchId",
                        column: x => x.MedicalAnalysisLabBranchId,
                        principalSchema: "MedicalAnalysisLab",
                        principalTable: "MedicalAnalysisLabBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAnalysisSpecialists",
                schema: "MedicalAnalysisLab",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    MedicalAnalysisLabBranchId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDCard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false),
                    MedicalDegree = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAnalysisSpecialists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalAnalysisSpecialists_MedicalAnalysisLabBranches_MedicalAnalysisLabBranchId",
                        column: x => x.MedicalAnalysisLabBranchId,
                        principalSchema: "MedicalAnalysisLab",
                        principalTable: "MedicalAnalysisLabBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestMedicalAnalysisLabReceptionists",
                schema: "MedicalAnalysisLab",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalAnalysisLabBranchId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDCard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpectedSalary = table.Column<double>(type: "float", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestMedicalAnalysisLabReceptionists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestMedicalAnalysisLabReceptionists_MedicalAnalysisLabBranches_MedicalAnalysisLabBranchId",
                        column: x => x.MedicalAnalysisLabBranchId,
                        principalSchema: "MedicalAnalysisLab",
                        principalTable: "MedicalAnalysisLabBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestMedicalAnalysisSpecialists",
                schema: "MedicalAnalysisLab",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpectedSalary = table.Column<double>(type: "float", nullable: false),
                    MedicalAnalysisLabBranchId = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDCard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false),
                    MedicalDegree = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestMedicalAnalysisSpecialists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestMedicalAnalysisSpecialists_MedicalAnalysisLabBranches_MedicalAnalysisLabBranchId",
                        column: x => x.MedicalAnalysisLabBranchId,
                        principalSchema: "MedicalAnalysisLab",
                        principalTable: "MedicalAnalysisLabBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAnalysisTestCustomers",
                schema: "MedicalAnalysisLab",
                columns: table => new
                {
                    MedicalAnalysisTestId = table.Column<int>(type: "int", nullable: false),
                    MedicalAnalysisLabCustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAnalysisTestCustomers", x => new { x.MedicalAnalysisLabCustomerId, x.MedicalAnalysisTestId });
                    table.ForeignKey(
                        name: "FK_MedicalAnalysisTestCustomers_MedicalAnalysisLabCustomers_MedicalAnalysisLabCustomerId",
                        column: x => x.MedicalAnalysisLabCustomerId,
                        principalSchema: "MedicalAnalysisLab",
                        principalTable: "MedicalAnalysisLabCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalAnalysisTestCustomers_MedicalAnalysisTests_MedicalAnalysisTestId",
                        column: x => x.MedicalAnalysisTestId,
                        principalSchema: "MedicalAnalysisLab",
                        principalTable: "MedicalAnalysisTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAnalysisTestResults",
                schema: "MedicalAnalysisLab",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Report = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MediacalAnalysisTestId = table.Column<int>(type: "int", nullable: false),
                    MedicalAnalysisLabCustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAnalysisTestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalAnalysisTestResults_MedicalAnalysisLabCustomers_MedicalAnalysisLabCustomerId",
                        column: x => x.MedicalAnalysisLabCustomerId,
                        principalSchema: "MedicalAnalysisLab",
                        principalTable: "MedicalAnalysisLabCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalAnalysisTestResults_MedicalAnalysisTests_MediacalAnalysisTestId",
                        column: x => x.MediacalAnalysisTestId,
                        principalSchema: "MedicalAnalysisLab",
                        principalTable: "MedicalAnalysisTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicineOrders",
                schema: "Pharmacy",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    PharmacyOrderId = table.Column<int>(type: "int", nullable: false),
                    MedicineCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineOrders", x => new { x.MedicineId, x.PharmacyOrderId });
                    table.ForeignKey(
                        name: "FK_MedicineOrders_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalSchema: "Pharmacy",
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineOrders_PharmacyOrders_PharmacyOrderId",
                        column: x => x.PharmacyOrderId,
                        principalSchema: "Pharmacy",
                        principalTable: "PharmacyOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: " ClinicReceptionists",
                schema: "Clinic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDCard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ ClinicReceptionists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ ClinicReceptionists_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Clinic",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                schema: "Clinic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Certification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Qualifications_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Clinic",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAndAnswers",
                schema: "QuestionAndAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAndAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAndAnswers_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Clinic",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAndAnswers_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalSchema: "Clinic",
                        principalTable: "Specializations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestClinicReceptionists",
                schema: "Clinic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonalNationalIDCard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndWork = table.Column<TimeOnly>(type: "time", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpectedSalary = table.Column<double>(type: "float", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestClinicReceptionists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestClinicReceptionists_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Clinic",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                schema: "Clinic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Appointment = table.Column<TimeOnly>(type: "time", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Clinic",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientAppointments",
                schema: "Clinic",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    paid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAppointments", x => new { x.PatientId, x.ScheduleId });
                    table.ForeignKey(
                        name: "FK_PatientAppointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Clinic",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientAppointments_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "Clinic",
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalPrescriptions",
                schema: "Clinic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    numOfTaken = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalPrescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalPrescriptions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Clinic",
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalPrescriptions_PatientAppointments_PatientId_ScheduleId",
                        columns: x => new { x.PatientId, x.ScheduleId },
                        principalSchema: "Clinic",
                        principalTable: "PatientAppointments",
                        principalColumns: new[] { "PatientId", "ScheduleId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientAppointmentCards",
                schema: "Clinic",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAppointmentCards", x => new { x.ApplicationUserId, x.PatientId, x.ScheduleId });
                    table.ForeignKey(
                        name: "FK_PatientAppointmentCards_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientAppointmentCards_PatientAppointments_PatientId_ScheduleId",
                        columns: x => new { x.PatientId, x.ScheduleId },
                        principalSchema: "Clinic",
                        principalTable: "PatientAppointments",
                        principalColumns: new[] { "PatientId", "ScheduleId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ ClinicReceptionists_DoctorId",
                schema: "Clinic",
                table: " ClinicReceptionists",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecializationId",
                schema: "Clinic",
                table: "Doctors",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAnalysisLabAppointments_MedicalAnalysisLabBranchId",
                schema: "MedicalAnalysisLab",
                table: "MedicalAnalysisLabAppointments",
                column: "MedicalAnalysisLabBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAnalysisLabAppointments_MedicalAnalysisLabCustomerId",
                schema: "MedicalAnalysisLab",
                table: "MedicalAnalysisLabAppointments",
                column: "MedicalAnalysisLabCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAnalysisLabBranches_MedicalAnalysisLabId",
                schema: "MedicalAnalysisLab",
                table: "MedicalAnalysisLabBranches",
                column: "MedicalAnalysisLabId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAnalysisLabReceptionists_MedicalAnalysisLabBranchId",
                schema: "MedicalAnalysisLab",
                table: "MedicalAnalysisLabReceptionists",
                column: "MedicalAnalysisLabBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAnalysisSpecialists_MedicalAnalysisLabBranchId",
                schema: "MedicalAnalysisLab",
                table: "MedicalAnalysisSpecialists",
                column: "MedicalAnalysisLabBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAnalysisTestCustomers_MedicalAnalysisTestId",
                schema: "MedicalAnalysisLab",
                table: "MedicalAnalysisTestCustomers",
                column: "MedicalAnalysisTestId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAnalysisTestResults_MediacalAnalysisTestId",
                schema: "MedicalAnalysisLab",
                table: "MedicalAnalysisTestResults",
                column: "MediacalAnalysisTestId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAnalysisTestResults_MedicalAnalysisLabCustomerId",
                schema: "MedicalAnalysisLab",
                table: "MedicalAnalysisTestResults",
                column: "MedicalAnalysisLabCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAnalysisTests_MedicalAnalysisLabId",
                schema: "MedicalAnalysisLab",
                table: "MedicalAnalysisTests",
                column: "MedicalAnalysisLabId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalPrescriptions_DoctorId",
                schema: "Clinic",
                table: "MedicalPrescriptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalPrescriptions_PatientId_ScheduleId",
                schema: "Clinic",
                table: "MedicalPrescriptions",
                columns: new[] { "PatientId", "ScheduleId" });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineOrders_PharmacyOrderId",
                schema: "Pharmacy",
                table: "MedicineOrders",
                column: "PharmacyOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_MedicineManufactoryId",
                schema: "Pharmacy",
                table: "Medicines",
                column: "MedicineManufactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_PharmacyCategoryId",
                schema: "Pharmacy",
                table: "Medicines",
                column: "PharmacyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointmentCards_PatientId_ScheduleId",
                schema: "Clinic",
                table: "PatientAppointmentCards",
                columns: new[] { "PatientId", "ScheduleId" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointments_ScheduleId",
                schema: "Clinic",
                table: "PatientAppointments",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientHistories_PatientId",
                schema: "Clinic",
                table: "PatientHistories",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyOrders_PharmacyCustomerId",
                schema: "Pharmacy",
                table: "PharmacyOrders",
                column: "PharmacyCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyOrders_PharmacyDeliveryRepresentativeId",
                schema: "Pharmacy",
                table: "PharmacyOrders",
                column: "PharmacyDeliveryRepresentativeId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_DoctorId",
                schema: "Clinic",
                table: "Qualifications",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAndAnswers_DoctorId",
                schema: "QuestionAndAnswer",
                table: "QuestionAndAnswers",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAndAnswers_SpecializationId",
                schema: "QuestionAndAnswer",
                table: "QuestionAndAnswers",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestClinicReceptionists_DoctorId",
                schema: "Clinic",
                table: "RequestClinicReceptionists",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDoctors_SpecializationId",
                schema: "Clinic",
                table: "RequestDoctors",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestMedicalAnalysisLabReceptionists_MedicalAnalysisLabBranchId",
                schema: "MedicalAnalysisLab",
                table: "RequestMedicalAnalysisLabReceptionists",
                column: "MedicalAnalysisLabBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestMedicalAnalysisSpecialists_MedicalAnalysisLabBranchId",
                schema: "MedicalAnalysisLab",
                table: "RequestMedicalAnalysisSpecialists",
                column: "MedicalAnalysisLabBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DoctorId",
                schema: "Clinic",
                table: "Schedules",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: " ClinicReceptionists",
                schema: "Clinic");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MedicalAnalysisLabAppointments",
                schema: "MedicalAnalysisLab");

            migrationBuilder.DropTable(
                name: "MedicalAnalysisLabReceptionists",
                schema: "MedicalAnalysisLab");

            migrationBuilder.DropTable(
                name: "MedicalAnalysisSpecialists",
                schema: "MedicalAnalysisLab");

            migrationBuilder.DropTable(
                name: "MedicalAnalysisTestCustomers",
                schema: "MedicalAnalysisLab");

            migrationBuilder.DropTable(
                name: "MedicalAnalysisTestResults",
                schema: "MedicalAnalysisLab");

            migrationBuilder.DropTable(
                name: "MedicalPrescriptions",
                schema: "Clinic");

            migrationBuilder.DropTable(
                name: "MedicineOrders",
                schema: "Pharmacy");

            migrationBuilder.DropTable(
                name: "PatientAppointmentCards",
                schema: "Clinic");

            migrationBuilder.DropTable(
                name: "PatientHistories",
                schema: "Clinic");

            migrationBuilder.DropTable(
                name: "Pharmacists",
                schema: "Pharmacy");

            migrationBuilder.DropTable(
                name: "Qualifications",
                schema: "Clinic");

            migrationBuilder.DropTable(
                name: "QuestionAndAnswers",
                schema: "QuestionAndAnswer");

            migrationBuilder.DropTable(
                name: "RequestClinicReceptionists",
                schema: "Clinic");

            migrationBuilder.DropTable(
                name: "RequestDoctors",
                schema: "Clinic");

            migrationBuilder.DropTable(
                name: "RequestMedicalAnalysisLabReceptionists",
                schema: "MedicalAnalysisLab");

            migrationBuilder.DropTable(
                name: "RequestMedicalAnalysisSpecialists",
                schema: "MedicalAnalysisLab");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MedicalAnalysisLabCustomers",
                schema: "MedicalAnalysisLab");

            migrationBuilder.DropTable(
                name: "MedicalAnalysisTests",
                schema: "MedicalAnalysisLab");

            migrationBuilder.DropTable(
                name: "Medicines",
                schema: "Pharmacy");

            migrationBuilder.DropTable(
                name: "PharmacyOrders",
                schema: "Pharmacy");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PatientAppointments",
                schema: "Clinic");

            migrationBuilder.DropTable(
                name: "MedicalAnalysisLabBranches",
                schema: "MedicalAnalysisLab");

            migrationBuilder.DropTable(
                name: "MedicineManufactories",
                schema: "Pharmacy");

            migrationBuilder.DropTable(
                name: "PharmacyCategories",
                schema: "Pharmacy");

            migrationBuilder.DropTable(
                name: "PharmacyCustomers",
                schema: "Pharmacy");

            migrationBuilder.DropTable(
                name: "PharmacyDeliveryRepresentatives",
                schema: "Pharmacy");

            migrationBuilder.DropTable(
                name: "Patients",
                schema: "Clinic");

            migrationBuilder.DropTable(
                name: "Schedules",
                schema: "Clinic");

            migrationBuilder.DropTable(
                name: "MedicalAnalysisLabs",
                schema: "MedicalAnalysisLab");

            migrationBuilder.DropTable(
                name: "Doctors",
                schema: "Clinic");

            migrationBuilder.DropTable(
                name: "Specializations",
                schema: "Clinic");
        }
    }
}
