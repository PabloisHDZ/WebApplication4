$(document).ready(function () {
    var dataSource = [
        //{ ID: 1, Vehiculo: 'Camión A', Title: 'Operador 1', CompaniaVehiculo: 'Compañía A', CompaniaEmpleado: 'Compañía X', duracion: '2 horas', ToneladasTotales: '5', FechaDescarga: '2024-07-10', FechaModificacion: '2024-07-10', SitioCarga: 'Sitio A', SitioDescarga: 'Sitio B', UsuarioModifico: 'Usuario 1', TipoMineral: 'Mineral A',  },
        //{ ID: 2, Vehiculo: 'Camión B', Title: 'Operador 2', CompaniaVehiculo: 'Compañía B', CompaniaEmpleado: 'Compañía Y', duracion: '3 horas', ToneladasTotales: '7', FechaDescarga: '2024-07-11', FechaModificacion: '2024-07-11', SitioCarga: 'Sitio B', SitioDescarga: 'Sitio C', UsuarioModifico: 'Usuario 2', TipoMineral: 'Mineral B',  }
        // Agregar más objetos según sea necesario
    ];

    $('#treeListContainer').dxTreeList({
        dataSource: [],
        /*keyExpr: 'ID',*/
        showBorders: true,
        width: '100%',
        height: 380,
        columns: [
            {
                dataField: 'Vehicle', caption: 'Vehiculo'
            },
            { dataField: 'Employee', caption: 'Empleado' },
            { dataField: 'Route', caption: 'Ruta' },
            { dataField: 'Weigth', caption: 'Peso' },
            { dataField: 'Material', caption: 'Material' },
        ],
    });

    // Configuración de dxTextBox para 'Empleado'
    $('#empleado').dxTextBox({
        placeholder: 'Empleado',
        inputAttr: { 'aria-label': 'Empleado' },
    });

    $.ajax({
        url: '/api/Haulages',
        method: 'GET',
        success: function (data) {
            // Inicializar DevExtreme TreeList con los datos recibidos
            $('#treeListContainer').dxTreeList({
                dataSource: data,
                keyExpr: 'HaulageId',
                showBorders: true,
                columns: [
                    { dataField: 'VehicleId', caption: 'Vehiculo' },
                    { dataField: 'EmployeeId', caption: 'Operador' },
                    { dataField: 'PathId', caption: 'Ruta' },
                    { dataField: 'Weight', caption: 'Toneladas' },
                    { dataField: 'MaterialTypeId', caption: 'Material' },
                    { dataField: 'Comments', caption: 'Comentarios' }
                ],
            });
        },
        error: function (error) {
            console.error('Error al obtener los datos de Acarreo:', error);
        }
    }); // Cierre del bloque $.ajax

    // Configuración de dxTextBox para 'Vehículo'
    $('#veiculo').dxTextBox({
        placeholder: 'Vehiculo',
        inputAttr: { 'aria-label': 'Vehículo' },
    });

    // Configuración de dxDateBox para 'Fecha'
    $('#fecha').dxDateBox({
        format: 'dd/MM/yyyy',
        displayFormat: 'dd/MM/yyyy',
        value: new Date()
    });

    // Configuración de dxTextBox para 'Ruta'
    $('#ruta').dxTextBox({
        placeholder: 'Ruta',
        inputAttr: { 'aria-label': 'Ruta' },
    });

    // Configuración de dxSwitch para 'Botón Deslizable'
    $('#switch-container').dxSwitch({
        onText: 'ON',
        offText: 'OFF',
        width: 100,
        onValueChanged: function (e) {
            var isEnabled = e.value;
            $.ajax({
                url: '/api/sync/toggle',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(isEnabled),
                success: function () {
                    if (isEnabled) {
                        alert('Sincronización habilitada.');
                    } else {
                        alert('Sincronización deshabilitada.');
                    }
                }
            });
        }
    });

    // Configuración de dxButton para 'Históricos'
    $('#btn-historicos').dxButton({
        stylingMode: 'contained',
        text: 'Históricos',
        type: 'success',
        onClick: function () {
            window.location.href = '@Url.Action("his", "home")'; // Ajusta el enlace según sea necesario
        },
    });
});
