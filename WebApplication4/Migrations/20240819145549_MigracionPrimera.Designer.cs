﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication4.Data;

#nullable disable

namespace WebApplication4.Migrations
{
    [DbContext(typeof(dbboot))]
    [Migration("20240819145549_MigracionPrimera")]
    partial class MigracionPrimera
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication4.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("WebApplication4.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaternalLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("NoEmployee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PaternalLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("WebApplication4.Models.Haulage", b =>
                {
                    b.Property<int>("HaulageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HaulageID"));

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dateofcarries")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Kilometers")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("LawType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LoadPointId")
                        .HasColumnType("int");

                    b.Property<string>("LoadPointName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaterialType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaterialTypeematerialTypeId")
                        .HasColumnType("int");

                    b.Property<int>("PathId")
                        .HasColumnType("int");

                    b.Property<int?>("ShiftId")
                        .HasColumnType("int");

                    b.Property<int?>("UnloadPointId")
                        .HasColumnType("int");

                    b.Property<string>("UnloadPointName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("haulagePathId")
                        .HasColumnType("int");

                    b.Property<int?>("materialTypeId")
                        .HasColumnType("int");

                    b.HasKey("HaulageID");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("MaterialTypeematerialTypeId");

                    b.HasIndex("VehicleId");

                    b.HasIndex("haulagePathId");

                    b.ToTable("Haulages");
                });

            modelBuilder.Entity("WebApplication4.Models.Historic", b =>
                {
                    b.Property<int>("HistoricId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HistoricId"));

                    b.Property<string>("Dateofcarries")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TokenRegistryId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleNavigationVehicleId")
                        .HasColumnType("int");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("WorkShiftId")
                        .HasColumnType("int");

                    b.Property<int>("haulagePathId")
                        .HasColumnType("int");

                    b.Property<string>("loadPointName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("materialTypeId")
                        .HasColumnType("int");

                    b.Property<string>("unLoadPointName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("vehicle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HistoricId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TokenRegistryId");

                    b.HasIndex("VehicleNavigationVehicleId");

                    b.HasIndex("WorkShiftId");

                    b.HasIndex("haulagePathId");

                    b.HasIndex("materialTypeId");

                    b.ToTable("Historics");
                });

            modelBuilder.Entity("WebApplication4.Models.Material", b =>
                {
                    b.Property<int>("materialTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("materialTypeId"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("materialTypeId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("WebApplication4.Models.ProgrammingRecord", b =>
                {
                    b.Property<int>("ProgrammingRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProgrammingRecordId"));

                    b.Property<TimeSpan>("Dateofcarries")
                        .HasColumnType("time");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("HaulageID")
                        .HasColumnType("int");

                    b.HasKey("ProgrammingRecordId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("HaulageID");

                    b.ToTable("ProgrammingRecords");
                });

            modelBuilder.Entity("WebApplication4.Models.Route", b =>
                {
                    b.Property<int>("haulagePathId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("haulagePathId"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("distance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("isEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("isExtraction")
                        .HasColumnType("bit");

                    b.Property<int>("loadPointId")
                        .HasColumnType("int");

                    b.Property<string>("loadPointName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("timeInHour")
                        .HasColumnType("time");

                    b.Property<int>("unLoadPointId")
                        .HasColumnType("int");

                    b.Property<string>("unLoadPointName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("haulagePathId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("WebApplication4.Models.Shift", b =>
                {
                    b.Property<int>("WorkShiftId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkShiftId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Enabled")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("OperationTime")
                        .HasColumnType("time");

                    b.Property<int>("ShiftId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("WorkShiftId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("WebApplication4.Models.TokenRegistry", b =>
                {
                    b.Property<int>("TokenRegistryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TokenRegistryId"));

                    b.Property<string>("access_token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("refresh_token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("token_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TokenRegistryId");

                    b.ToTable("TokenRegistries");
                });

            modelBuilder.Entity("WebApplication4.Models.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EconomicNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("EmptyWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("FuelTankCapacity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Plates")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("VehicleId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("VehicleTypeId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("WebApplication4.Models.VehicleType", b =>
                {
                    b.Property<int>("VehicleTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VehicleTypeId");

                    b.ToTable("VehicleType");
                });

            modelBuilder.Entity("WebApplication4.Models.Employee", b =>
                {
                    b.HasOne("WebApplication4.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("WebApplication4.Models.Haulage", b =>
                {
                    b.HasOne("WebApplication4.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WebApplication4.Models.Material", "MaterialTypee")
                        .WithMany()
                        .HasForeignKey("MaterialTypeematerialTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WebApplication4.Models.Vehicle", "VehicleNavigation")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WebApplication4.Models.Route", "HaulagePath")
                        .WithMany()
                        .HasForeignKey("haulagePathId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("HaulagePath");

                    b.Navigation("MaterialTypee");

                    b.Navigation("VehicleNavigation");
                });

            modelBuilder.Entity("WebApplication4.Models.Historic", b =>
                {
                    b.HasOne("WebApplication4.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WebApplication4.Models.TokenRegistry", "TokenRegistry")
                        .WithMany()
                        .HasForeignKey("TokenRegistryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WebApplication4.Models.Vehicle", "VehicleNavigation")
                        .WithMany()
                        .HasForeignKey("VehicleNavigationVehicleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WebApplication4.Models.Shift", "WorkShift")
                        .WithMany()
                        .HasForeignKey("WorkShiftId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WebApplication4.Models.Route", "HaulagePath")
                        .WithMany()
                        .HasForeignKey("haulagePathId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WebApplication4.Models.Material", "MaterialType")
                        .WithMany()
                        .HasForeignKey("materialTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("HaulagePath");

                    b.Navigation("MaterialType");

                    b.Navigation("TokenRegistry");

                    b.Navigation("VehicleNavigation");

                    b.Navigation("WorkShift");
                });

            modelBuilder.Entity("WebApplication4.Models.ProgrammingRecord", b =>
                {
                    b.HasOne("WebApplication4.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WebApplication4.Models.Haulage", "Haulage")
                        .WithMany()
                        .HasForeignKey("HaulageID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Haulage");
                });

            modelBuilder.Entity("WebApplication4.Models.Vehicle", b =>
                {
                    b.HasOne("WebApplication4.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WebApplication4.Models.VehicleType", "VehicleType")
                        .WithMany()
                        .HasForeignKey("VehicleTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("VehicleType");
                });
#pragma warning restore 612, 618
        }
    }
}
