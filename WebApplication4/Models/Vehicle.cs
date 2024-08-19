using System;
using System.ComponentModel.DataAnnotations;
using WebApplication4.Models;

namespace WebApplication4.Models
{
    // Clase que representa un veh�culo en el sistema
    public class Vehicle
    {
        // Propiedad que act�a como la clave primaria de la entidad Vehicle
        [Key]
        public int VehicleId { get; set; }

        // Matr�cula del veh�culo
        public string Plates { get; set; }

        // N�mero econ�mico del veh�culo, usado para identificarlo
        public string EconomicNumber { get; set; }

        // Identificador de la compa��a a la que pertenece el veh�culo
        public int CompanyId { get; set; }

        // Modelo del veh�culo
        public string Model { get; set; }

        // Peso vac�o del veh�culo en kilogramos
        public decimal EmptyWeight { get; set; }

        // Capacidad del tanque de combustible en litros
        public decimal FuelTankCapacity { get; set; }

        // Peso total del veh�culo en kilogramos
        public decimal Weight { get; set; }

        // Identificador del tipo de veh�culo (por ejemplo, cami�n, autom�vil, etc.)
        public int VehicleTypeId { get; set; }

        // Descripci�n adicional del veh�culo
        public string Description { get; set; }

        // Relaci�n de navegaci�n con la entidad Company
        // Permite acceder a los detalles de la compa��a a la que pertenece el veh�culo
        public virtual Company Company { get; set; }

        // Relaci�n de navegaci�n con la entidad VehicleType
        // Permite acceder a los detalles del tipo de veh�culo
        public virtual VehicleType VehicleType { get; set; }
    }
}
