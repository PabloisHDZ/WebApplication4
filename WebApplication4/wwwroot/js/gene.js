$(document).ready(function () {
    var dataSource = [
        { ID: 1, Vehiculo: 'Camión A', Title: 'Operador 1', CompaniaVehiculo: 'Compañía A', CompaniaEmpleado: 'Compañía X', duracion: '2 horas', ToneladasTotales: '5', FechaDescarga: '2024-07-10', FechaModificacion: '2024-07-10', SitioCarga: 'Sitio A', SitioDescarga: 'Sitio B', UsuarioModifico: 'Usuario 1', TipoMineral: 'Mineral A', TipoAcarreo: 'Acarreo 1', TipoPeso: 'Peso 1' },
        { ID: 2, Vehiculo: 'Camión B', Title: 'Operador 2', CompaniaVehiculo: 'Compañía B', CompaniaEmpleado: 'Compañía Y', duracion: '3 horas', ToneladasTotales: '7', FechaDescarga: '2024-07-11', FechaModificacion: '2024-07-11', SitioCarga: 'Sitio B', SitioDescarga: 'Sitio C', UsuarioModifico: 'Usuario 2', TipoMineral: 'Mineral B', TipoAcarreo: 'Acarreo 2', TipoPeso: 'Peso 2' }
        // Agregar más objetos según sea necesario
    ];

    $('#treeListContainer').dxTreeList({
        dataSource: dataSource,
        keyExpr: 'ID',
        showBorders: true,
        columns: [
            { dataField: 'Vehiculo', caption: 'Vehiculo' },
            { dataField: 'Title', caption: 'Operador' },
            { dataField: 'CompaniaVehiculo', caption: 'Compañia del Vehículo' },
            { dataField: 'CompaniaEmpleado', caption: 'Compañia del empleado' },
            { dataField: 'duracion', caption: 'Duracion' },
            { dataField: 'ToneladasTotales', caption: 'Toneladas Totales' },
            { dataField: 'FechaDescarga', caption: 'Fecha de Descarga' },
            { dataField: 'FechaModificacion', caption: 'Fecha de Modificación' },
            { dataField: 'SitioCarga', caption: 'Sitio de Carga' },
            { dataField: 'SitioDescarga', caption: 'Sitio de Descarga' },
            { dataField: 'UsuarioModifico', caption: 'Usuario que Modificó' },
            { dataField: 'TipoMineral', caption: 'Tipo de Mineral' },
            { dataField: 'TipoAcarreo', caption: 'Tipo de Acarreo' },
            { dataField: 'TipoPeso', caption: 'Tipo de Peso' },
        ],
    });

    // Configuración de dxTextBox para 'Empleado'
    $('#empleado').dxTextBox({
        placeholder: 'Empleado',
        inputAttr: { 'aria-label': 'Empleado' },
    });

    $.ajax({
        url: '/api/Acarreo',
        method: 'GET',
        success: function (data) {
            // Inicializar DevExtreme TreeList con los datos recibidos
            $('#treeListContainer').dxTreeList({
                dataSource: data,
                keyExpr: 'AcarreoID',
                showBorders: true,
                columns: [
                    { dataField: 'VehiculoID', caption: 'Vehiculo' },
                    { dataField: 'OperadorID', caption: 'Operador' },
                    { dataField: 'RutaID', caption: 'Ruta' },
                    { dataField: 'Toneladas', caption: 'Toneladas' },
                    { dataField: 'MaterialID', caption: 'Material' },
                    { dataField: 'Comentarios', caption: 'Comentarios' }
                ],
            });
        },
        error: function (error) {
            console.error('Error al obtener los datos de Acarreo:', error);
        }

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
            if (e.value) {
                alert('Bot encendido!');
            } else {
                alert('Bot apagado!');
            }
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
