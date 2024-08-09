using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class MigracionPrimera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    materialTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.materialTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    haulagePathId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    distance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    isExtraction = table.Column<bool>(type: "bit", nullable: false),
                    isEnabled = table.Column<bool>(type: "bit", nullable: false),
                    loadPointId = table.Column<int>(type: "int", nullable: false),
                    loadPointName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timeInHour = table.Column<TimeSpan>(type: "time", nullable: false),
                    unLoadPointId = table.Column<int>(type: "int", nullable: false),
                    unloadPointName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.haulagePathId);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    WorkShiftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enabled = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.WorkShiftId);
                });

            migrationBuilder.CreateTable(
                name: "TokenRegistries",
                columns: table => new
                {
                    TokenRegistryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    access_token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    token_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    refresh_token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenRegistries", x => x.TokenRegistryId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoEmployee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaternalLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaternalLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EconomicNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmptyWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FuelTankCapacity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicles_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Haulages",
                columns: table => new
                {
                    HaulageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PathId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    haulagePathId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dateofcarries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    materialTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Haulages", x => x.HaulageId);
                    table.ForeignKey(
                        name: "FK_Haulages_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Haulages_Materials_materialTypeId",
                        column: x => x.materialTypeId,
                        principalTable: "Materials",
                        principalColumn: "materialTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Haulages_Routes_haulagePathId",
                        column: x => x.haulagePathId,
                        principalTable: "Routes",
                        principalColumn: "haulagePathId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Haulages_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Historics",
                columns: table => new
                {
                    HistoricId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenRegistryId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    loadPointId = table.Column<int>(type: "int", nullable: false),
                    unLoadPointId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    materialTypeId = table.Column<int>(type: "int", nullable: false),
                    Dateofcarries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkShiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historics", x => x.HistoricId);
                    table.ForeignKey(
                        name: "FK_Historics_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historics_Materials_materialTypeId",
                        column: x => x.materialTypeId,
                        principalTable: "Materials",
                        principalColumn: "materialTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historics_Routes_loadPointId",
                        column: x => x.loadPointId,
                        principalTable: "Routes",
                        principalColumn: "haulagePathId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Historics_Routes_unLoadPointId",
                        column: x => x.unLoadPointId,
                        principalTable: "Routes",
                        principalColumn: "haulagePathId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Historics_Shifts_WorkShiftId",
                        column: x => x.WorkShiftId,
                        principalTable: "Shifts",
                        principalColumn: "WorkShiftId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historics_TokenRegistries_TokenRegistryId",
                        column: x => x.TokenRegistryId,
                        principalTable: "TokenRegistries",
                        principalColumn: "TokenRegistryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Historics_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammingRecords",
                columns: table => new
                {
                    ProgrammingRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HaulageId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Dateofcarries = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingRecords", x => x.ProgrammingRecordId);
                    table.ForeignKey(
                        name: "FK_ProgrammingRecords_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgrammingRecords_Haulages_HaulageId",
                        column: x => x.HaulageId,
                        principalTable: "Haulages",
                        principalColumn: "HaulageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Haulages_EmployeeId",
                table: "Haulages",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Haulages_haulagePathId",
                table: "Haulages",
                column: "haulagePathId");

            migrationBuilder.CreateIndex(
                name: "IX_Haulages_materialTypeId",
                table: "Haulages",
                column: "materialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Haulages_VehicleId",
                table: "Haulages",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Historics_EmployeeId",
                table: "Historics",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Historics_loadPointId",
                table: "Historics",
                column: "loadPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Historics_materialTypeId",
                table: "Historics",
                column: "materialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Historics_TokenRegistryId",
                table: "Historics",
                column: "TokenRegistryId");

            migrationBuilder.CreateIndex(
                name: "IX_Historics_unLoadPointId",
                table: "Historics",
                column: "unLoadPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Historics_VehicleId",
                table: "Historics",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Historics_WorkShiftId",
                table: "Historics",
                column: "WorkShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammingRecords_EmployeeId",
                table: "ProgrammingRecords",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammingRecords_HaulageId",
                table: "ProgrammingRecords",
                column: "HaulageId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CompanyId",
                table: "Vehicles",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historics");

            migrationBuilder.DropTable(
                name: "ProgrammingRecords");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "TokenRegistries");

            migrationBuilder.DropTable(
                name: "Haulages");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
