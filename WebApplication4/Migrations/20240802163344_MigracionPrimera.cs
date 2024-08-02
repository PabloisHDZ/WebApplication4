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
                name: "Compañias",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compañias", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Historicos",
                columns: table => new
                {
                    Vehicle = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoadPointName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnLoadPointName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfCarries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historicos", x => x.Vehicle);
                });

            migrationBuilder.CreateTable(
                name: "Materiales",
                columns: table => new
                {
                    MaterialTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiales", x => x.MaterialTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosDeToken",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    access_token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    token_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    refresh_token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosDeToken", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Sitios",
                columns: table => new
                {
                    PlaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    IsProduction = table.Column<bool>(type: "bit", nullable: false),
                    PlaceLocation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlaceType = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PositionX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PositionY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PositionZ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReferencePoints = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sitios", x => x.PlaceId);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
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
                    table.PrimaryKey("PK_Turnos", x => x.WorkShiftId);
                });

            migrationBuilder.CreateTable(
                name: "Operadores",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoEmployee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaternalLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaternalLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operadores", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Operadores_Compañias_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Compañias",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vehicle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    EconomicNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmptyWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FuelTankCapacity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Compañias_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Compañias",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rutas",
                columns: table => new
                {
                    HaulagePathId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsExtraction = table.Column<bool>(type: "bit", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LoadPointId = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoadPointName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeInHour = table.Column<TimeOnly>(type: "time", nullable: false),
                    UnLoadPointId = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnLoadPointName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rutas", x => x.HaulagePathId);
                    table.ForeignKey(
                        name: "FK_Rutas_Materiales_MaterialTypeId",
                        column: x => x.MaterialTypeId,
                        principalTable: "Materiales",
                        principalColumn: "MaterialTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Acarreos",
                columns: table => new
                {
                    CarryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    HaulageSiteId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfCarries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acarreos", x => x.CarryId);
                    table.ForeignKey(
                        name: "FK_Acarreos_Materiales_MaterialTypeId",
                        column: x => x.MaterialTypeId,
                        principalTable: "Materiales",
                        principalColumn: "MaterialTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acarreos_Operadores_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Operadores",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acarreos_Rutas_HaulageSiteId",
                        column: x => x.HaulageSiteId,
                        principalTable: "Rutas",
                        principalColumn: "HaulagePathId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acarreos_Vehiculos_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehiculos",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgramacionesDeRegistro",
                columns: table => new
                {
                    CarryId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    DateOfCarries = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramacionesDeRegistro", x => x.CarryId);
                    table.ForeignKey(
                        name: "FK_ProgramacionesDeRegistro_Acarreos_CarryId",
                        column: x => x.CarryId,
                        principalTable: "Acarreos",
                        principalColumn: "CarryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramacionesDeRegistro_RegistrosDeToken_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "RegistrosDeToken",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acarreos_EmployeeId",
                table: "Acarreos",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Acarreos_HaulageSiteId",
                table: "Acarreos",
                column: "HaulageSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Acarreos_MaterialTypeId",
                table: "Acarreos",
                column: "MaterialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Acarreos_VehicleId",
                table: "Acarreos",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Operadores_CompanyId",
                table: "Operadores",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramacionesDeRegistro_EmployeeId",
                table: "ProgramacionesDeRegistro",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rutas_MaterialTypeId",
                table: "Rutas",
                column: "MaterialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_CompanyId",
                table: "Vehiculos",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historicos");

            migrationBuilder.DropTable(
                name: "ProgramacionesDeRegistro");

            migrationBuilder.DropTable(
                name: "Sitios");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Acarreos");

            migrationBuilder.DropTable(
                name: "RegistrosDeToken");

            migrationBuilder.DropTable(
                name: "Operadores");

            migrationBuilder.DropTable(
                name: "Rutas");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Materiales");

            migrationBuilder.DropTable(
                name: "Compañias");
        }
    }
}
