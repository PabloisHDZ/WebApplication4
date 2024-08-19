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
                    unLoadPointName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    ShiftId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enabled = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    OperationTime = table.Column<TimeSpan>(type: "time", nullable: false)
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
                name: "VehicleType",
                columns: table => new
                {
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleType", x => x.VehicleTypeId);
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
                        principalColumn: "CompanyId");
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
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicles_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId");
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleType_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleType",
                        principalColumn: "VehicleTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Haulages",
                columns: table => new
                {
                    HaulageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    PathId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dateofcarries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    materialTypeId = table.Column<int>(type: "int", nullable: true),
                    LoadPointId = table.Column<int>(type: "int", nullable: true),
                    UnloadPointId = table.Column<int>(type: "int", nullable: true),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    LoadPointName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnloadPointName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LawType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kilometers = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaterialType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    haulagePathId = table.Column<int>(type: "int", nullable: false),
                    MaterialTypeematerialTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Haulages", x => x.HaulageID);
                    table.ForeignKey(
                        name: "FK_Haulages_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Haulages_Materials_MaterialTypeematerialTypeId",
                        column: x => x.MaterialTypeematerialTypeId,
                        principalTable: "Materials",
                        principalColumn: "materialTypeId");
                    table.ForeignKey(
                        name: "FK_Haulages_Routes_haulagePathId",
                        column: x => x.haulagePathId,
                        principalTable: "Routes",
                        principalColumn: "haulagePathId");
                    table.ForeignKey(
                        name: "FK_Haulages_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId");
                });

            migrationBuilder.CreateTable(
                name: "Historics",
                columns: table => new
                {
                    HistoricId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenRegistryId = table.Column<int>(type: "int", nullable: false),
                    vehicle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    loadPointName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unLoadPointName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    materialTypeId = table.Column<int>(type: "int", nullable: false),
                    Dateofcarries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkShiftId = table.Column<int>(type: "int", nullable: false),
                    VehicleNavigationVehicleId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    haulagePathId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historics", x => x.HistoricId);
                    table.ForeignKey(
                        name: "FK_Historics_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Historics_Materials_materialTypeId",
                        column: x => x.materialTypeId,
                        principalTable: "Materials",
                        principalColumn: "materialTypeId");
                    table.ForeignKey(
                        name: "FK_Historics_Routes_haulagePathId",
                        column: x => x.haulagePathId,
                        principalTable: "Routes",
                        principalColumn: "haulagePathId");
                    table.ForeignKey(
                        name: "FK_Historics_Shifts_WorkShiftId",
                        column: x => x.WorkShiftId,
                        principalTable: "Shifts",
                        principalColumn: "WorkShiftId");
                    table.ForeignKey(
                        name: "FK_Historics_TokenRegistries_TokenRegistryId",
                        column: x => x.TokenRegistryId,
                        principalTable: "TokenRegistries",
                        principalColumn: "TokenRegistryId");
                    table.ForeignKey(
                        name: "FK_Historics_Vehicles_VehicleNavigationVehicleId",
                        column: x => x.VehicleNavigationVehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId");
                });

            migrationBuilder.CreateTable(
                name: "ProgrammingRecords",
                columns: table => new
                {
                    ProgrammingRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HaulageID = table.Column<int>(type: "int", nullable: false),
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
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_ProgrammingRecords_Haulages_HaulageID",
                        column: x => x.HaulageID,
                        principalTable: "Haulages",
                        principalColumn: "HaulageID");
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
                name: "IX_Haulages_MaterialTypeematerialTypeId",
                table: "Haulages",
                column: "MaterialTypeematerialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Haulages_VehicleId",
                table: "Haulages",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Historics_EmployeeId",
                table: "Historics",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Historics_haulagePathId",
                table: "Historics",
                column: "haulagePathId");

            migrationBuilder.CreateIndex(
                name: "IX_Historics_materialTypeId",
                table: "Historics",
                column: "materialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Historics_TokenRegistryId",
                table: "Historics",
                column: "TokenRegistryId");

            migrationBuilder.CreateIndex(
                name: "IX_Historics_VehicleNavigationVehicleId",
                table: "Historics",
                column: "VehicleNavigationVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Historics_WorkShiftId",
                table: "Historics",
                column: "WorkShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammingRecords_EmployeeId",
                table: "ProgrammingRecords",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammingRecords_HaulageID",
                table: "ProgrammingRecords",
                column: "HaulageID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CompanyId",
                table: "Vehicles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleTypeId",
                table: "Vehicles",
                column: "VehicleTypeId");
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

            migrationBuilder.DropTable(
                name: "VehicleType");
        }
    }
}
