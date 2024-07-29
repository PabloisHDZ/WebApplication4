using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compañias",
                columns: table => new
                {
                    CompañiaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compañias", x => x.CompañiaID);
                });

            migrationBuilder.CreateTable(
                name: "Historicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vehiculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sitio_de_carga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sitio_de_descarga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Toneladas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha_de_acarreos = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materiales",
                columns: table => new
                {
                    MaterialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiales", x => x.MaterialID);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosDeToken",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    access_token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    token_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    refresh_token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosDeToken", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    TurnoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.TurnoID);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    VehiculoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero_economico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tipo_de_vehiculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompañiaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.VehiculoID);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Compañias_CompañiaID",
                        column: x => x.CompañiaID,
                        principalTable: "Compañias",
                        principalColumn: "CompañiaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rutas",
                columns: table => new
                {
                    RutaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sito_De_Carga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sito_De_Descarga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distancia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tiempo_De_Ciclo = table.Column<TimeOnly>(type: "time", nullable: false),
                    Tipo_De_Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rutas", x => x.RutaID);
                    table.ForeignKey(
                        name: "FK_Rutas_Materiales_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Materiales",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Operadores",
                columns: table => new
                {
                    OperadorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TurnoID = table.Column<int>(type: "int", nullable: false),
                    CompañiaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operadores", x => x.OperadorID);
                    table.ForeignKey(
                        name: "FK_Operadores_Compañias_CompañiaID",
                        column: x => x.CompañiaID,
                        principalTable: "Compañias",
                        principalColumn: "CompañiaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operadores_Turnos_TurnoID",
                        column: x => x.TurnoID,
                        principalTable: "Turnos",
                        principalColumn: "TurnoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Acarreos",
                columns: table => new
                {
                    AcarreoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehiculoID = table.Column<int>(type: "int", nullable: false),
                    OperadorID = table.Column<int>(type: "int", nullable: false),
                    RutaID = table.Column<int>(type: "int", nullable: false),
                    Toneladas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acarreos", x => x.AcarreoID);
                    table.ForeignKey(
                        name: "FK_Acarreos_Materiales_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Materiales",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acarreos_Operadores_OperadorID",
                        column: x => x.OperadorID,
                        principalTable: "Operadores",
                        principalColumn: "OperadorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acarreos_Rutas_RutaID",
                        column: x => x.RutaID,
                        principalTable: "Rutas",
                        principalColumn: "RutaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acarreos_Vehiculos_VehiculoID",
                        column: x => x.VehiculoID,
                        principalTable: "Vehiculos",
                        principalColumn: "VehiculoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgramacionesDeRegistro",
                columns: table => new
                {
                    AcarreoID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Hora_de_registro = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramacionesDeRegistro", x => x.AcarreoID);
                    table.ForeignKey(
                        name: "FK_ProgramacionesDeRegistro_Acarreos_AcarreoID",
                        column: x => x.AcarreoID,
                        principalTable: "Acarreos",
                        principalColumn: "AcarreoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramacionesDeRegistro_RegistrosDeToken_UserID",
                        column: x => x.UserID,
                        principalTable: "RegistrosDeToken",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acarreos_MaterialID",
                table: "Acarreos",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_Acarreos_OperadorID",
                table: "Acarreos",
                column: "OperadorID");

            migrationBuilder.CreateIndex(
                name: "IX_Acarreos_RutaID",
                table: "Acarreos",
                column: "RutaID");

            migrationBuilder.CreateIndex(
                name: "IX_Acarreos_VehiculoID",
                table: "Acarreos",
                column: "VehiculoID");

            migrationBuilder.CreateIndex(
                name: "IX_Operadores_CompañiaID",
                table: "Operadores",
                column: "CompañiaID");

            migrationBuilder.CreateIndex(
                name: "IX_Operadores_TurnoID",
                table: "Operadores",
                column: "TurnoID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramacionesDeRegistro_UserID",
                table: "ProgramacionesDeRegistro",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Rutas_MaterialID",
                table: "Rutas",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_CompañiaID",
                table: "Vehiculos",
                column: "CompañiaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historicos");

            migrationBuilder.DropTable(
                name: "ProgramacionesDeRegistro");

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
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Materiales");

            migrationBuilder.DropTable(
                name: "Compañias");
        }
    }
}
