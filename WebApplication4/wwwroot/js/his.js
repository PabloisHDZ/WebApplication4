let grid = $('#gridContainer').dxDataGrid({
    dataSource: [],
    width: '100%',
    height: 600,
    showBorders: true,
    groupPanel: {
        visible: true,
        emptyPanelText: 'Arrastra aquí el encabezado de una columna para agrupar por esa columna',
    },
    grouping: {
        autoExpandAll: true,
    },
    sortByGroupSummaryInfo: [{
        summaryItem: 'count',
    }],
    columns: [
        { dataField: 'Vehiculo', caption: 'Vehiculo' },
        { dataField: 'Title', caption: 'Operador' },
        { dataField: 'SitioCarga', caption: 'Sitio de Carga' },
        { dataField: 'SitioDescarga', caption: 'Sitio de Descarga' },
        { dataField: 'ToneladasTotales', caption: 'Toneladas Totales' },
        { dataField: 'TipoMineral', caption: 'Tipo de Mineral' },
        { dataField: 'FechaDescarga', caption: 'Fecha de Descarga' }
    ],
    export: {
        enabled: true,
    },
    onExporting: function (e) {
        console.log(e);
    },
    onFileSaving: async e => {
        e.cancel = true;
        await exportToExcel(e, 'Acarreos', 'Acarreos', '', '');
        console.log('exportToExcel 444');
    },
    onExported: e => {
        console.log('exportToExcel 1111');

        if (globalExportPromiseResolved) {
            console.log('%c new global excel export promise', 'background: red; color: white');
            globalExportPromise = new Promise((resolve, reject) => {
                globalExportPromiseResolved = false;
                globalExportPromiseResolve = () => {
                    globalExportPromiseResolved = true;
                    console.log('%c ------------------- global excel promise resolved', 'background: red; color: white');
                    resolve('globalExportPromiseResolved');
                };
            });
        }
    },
    onSelectionChanged(e) {
        console.log(e.selectedRowsData);
        datagridSelectedData();
    }
}).dxDataGrid('instance');

function datagridSelectedData() {
    $('#selectedData').html('');
    let arreglo = grid.getSelectedRowsData();
    arreglo.forEach(row => {
        $('#selectedData').append(`<span>${row.Address}</span>`);
    });
}

function importarArchivo() {
    if (document.getElementById('fileTest').files.length > 0) {
        let input = document.getElementById('fileTest');
        let files = input.files;
        let formData = {
            "files": [],
        };

        for (let i = 0; i != files.length; i++) {
            formData.files[i] = { 'file': files[i] };
        }

        const wb = new ExcelJS.Workbook();
        const FILE_DATA = formData.files[0].file;
        wb.clearThemes();

        wb.xlsx.load(FILE_DATA).then(() => {
            let sheet = wb.worksheets[0];
            let plansFound = [];
            let excelKeys = [];
            sheet.eachRow({ includeEmpty: false }, function (row, rowNumber) {
                if (rowNumber === 5) {
                    excelKeys = row.values;
                }

                if (rowNumber > 5) {
                    plansFound.push(row.values);
                }
            });

            if (excelKeys[0] === undefined) {
                excelKeys.splice(0, 1);
            }

            plansFound.forEach(arr => {
                if (arr[0] === undefined) {
                    arr.splice(0, 1);
                }
            });

            let objectsGenerated = [];

            $.each(plansFound, function (index, arr) {
                let objectCrafted = {
                    "Vehiculo": arr[0],
                    "Title": arr[1], // operador
                    "SitioCarga": arr[2],
                    "SitioDescarga": arr[3],
                    "ToneladasTotales": arr[4],
                    "TipoMineral": arr[5],
                    "FechaDescarga": arr[6],
                    "Comentarios": arr[7] // Assuming arr[7] is Comentarios
                };
                objectsGenerated.push(objectCrafted);
            });

            console.log(objectsGenerated);
            grid.option("dataSource", objectsGenerated);

            // Enviar datos al servidor
            //$.ajax({
            //    url: '/api/import/import',
            //    type: 'POST',
            //    contentType: 'application/json',
            //    data: JSON.stringify(objectsGenerated),
            //    success: function (response) {
            //        console.log('Datos enviados exitosamente', response);sS
            //    },
            //    error: function (error) {
            //        console.error('Error al enviar los datos', error);
            //    }
            //});

            //$.ajax({
            //    url: '/service/haulages/api/v2/generalsettings/vehicles/add',
            //    data: JSON.stringify(vehiclesIds),
            //    contentType: "application/json; charset=utf-8",
            //    //dataType: "json",
            //    type: "POST",
            //    headers: { "Authorization": "Bearer " + $.cookie()["AT_Us"] },
            //}).then((response) => {
            //    if (typeof response == "undefined" || response == null) {
            //        console.log("Error in assignVehiclesToHaulages");
            //    }
            //    segregateVehiclesInAssignedAndUnassigned();
            //    _instanceDataGridNonAssignedVehicles.option("selectedRowKeys", []);
            //    _instanceButtonAssignVehicles.option("disabled", true);
            //    NotificateSuccessAction("vehicles", "assign_plural");

            //}).fail((error) => {
            //    if (error.status === 200) {
            //        console.log("%cResponse of 'assignVehiclesToHaulages': ", "color: yellow;", error);
            //        console.log("error status 200");
            //        //_instanceDataGridNonAssignedVehicles.option("selectedRowKeys", [])
            //        //_instanceButtonAssignVehicles.option("disabled", true)
            //        //NotificationSuccess($.i18n("SETTINGS-VEHICLES_ASSIGNED_SUCCESSFULLY"))
            //        //getAllHaulagesSettings()
            //    } else {
            //        console.log("%cError in assignVehiclesToHaulages: ", "color: red;", error);
            //    }
            //    NotificateErrorAction("vehicles", "assign");
            //})

            //seguridad
//            $.ajax({
//                type: "GET",
//                url: "/service/catalog/api/v1/Companies/all", // the url from the back
//                headers: { "Authorization": "Bearer " + $.cookie()["AT_Us"] },
//                dataType: "json",
//                contentType: "application/json; charset=utf-8",
//                async: true,
//            }).then(function (response) {



//                // to do with the response



//            }).fail(function (err) {

//                // in case we get an error
//            })

//        }).catch(err => {
//            console.log(err.message);
//        });
//    }
//}


            //$.ajax({
            //    type: "GET",
            //    url: "/service/catalog/api/v1/Companies/all",
            //    headers: { "Authorization": "Bearer " + $.cookie()["AT_Us"] },
            //    dataType: "json",
            //    contentType: "application/json; charset=utf-8",
            //    async: false,
            //}).then(function (companies) {



            //    if (typeof companies !== "undefined" && Array.isArray(companies)) {
            //        companies_names = companies
            //        companySelected = companies_names[0]
            //        //console.log(companies)
            //        //console.log(companySelected)
            //        return { data: companies }
            //    } else {
            //        NotificateErrorAction("companies", "retrieve")



            //        companies_names = []
            //        return { data: [] }
            //    }



            //}).fail(function (response) {
            //    NotificateErrorAction("companies", "retrieve")
            //    redLog("Error al obtener compañias. Response: ", response)



            //    companies_names = []
            //    return { data: [] }
            //})

            //$.ajax({
            //    url: '/tu/url',
            //    data: JSON.stringify(objeto), // aqui va el objeto que vas a guardar, tiene que ser un json, ej: { name: 'Uriel', edad: 28 }
            //    contentType: "application/json; charset=utf-8",
            //    //dataType: "json", //esta si regresa un json
            //    type: "POST",
            //    headers: { "Authorization": "Bearer " + $.cookie()["AT_Us"] }, // esta si hay seguridad
            //}).then((response) => {
            //    // to do with the response
            //    // aqui vamos a llamar al get
            //}).fail((error) => {
            //    // en caso de que haya un error
            //})
document.getElementById("boton").addEventListener("click", function (e) {
    importarArchivo();
});

// Global vars to identify exporting promise status
var globalExportPromise;
var globalExportPromiseResolve;
var globalExportPromiseResolved = true;

function exportToExcel(e, fileName, mainTitle, secondaryTitle, dataDate, columnsNumberThatNeedToWrapText) {
    let milliSec = new Date().getTime();
    return new Promise(async function (resolve, reject) {
        console.log("---creating");
        e.cancel = true;
        const workbook = new ExcelJS.Workbook();
        await workbook.xlsx.load(e.data);
        workbook.clearThemes();
        let sheet = workbook.worksheets[0];
        let topHeaderLenght = sheet.columns.length;
        sheet.removeConditionalFormatting();
        sheet.name = 'Page 1';
        sheet.eachRow({ includeEmpty: true }, function (row, rowNumber) {
            row.eachCell(function (cell, colNumber) {
                cell.font = { name: 'Calibri', family: 2, bold: false, size: 9 };
                cell.alignment = { vertical: 'middle' };
                if (rowNumber == 1) {
                    cell.fill = { type: 'pattern', pattern: 'solid', fgColor: { argb: 'aae6e6ff' } };
                    cell.font = { name: 'Calibri', family: 2, bold: true, size: 9 };
                    cell.alignment = { horizontal: 'center' };
                    cell.showRowColHeaders = false;
                    cell.border = { top: { style: 'thin' }, left: { style: 'thin' }, bottom: { style: 'thin' }, right: { style: 'thin' } };
                }
            });
        });

        sheet.getColumn(1).width = 20;
        sheet.getColumn(5).width = 25;
        sheet.insertRow(1, getEmptyArray(topHeaderLenght));
        sheet.insertRow(1, getEmptyArray(topHeaderLenght));
        sheet.insertRow(1, getEmptyArray(topHeaderLenght));
        sheet.insertRow(1, getEmptyArray(topHeaderLenght));
        sheet.views = [{ state: 'frozen', ySplit: 5 }];
        sheet.pageSetup.printTitlesRow = '5:5';
        sheet.eachRow({ includeEmpty: true }, function (row, rowNumber) {
            row.eachCell(function (cell, colNumber) {
                if (rowNumber <= 4) {
                    cell.fill = { type: 'pattern', pattern: 'solid', fgColor: { argb: 'ff0B2A4A' } };
                    cell.font = { name: 'Calibri', family: 2, bold: true, size: 9, color: { argb: 'ffffffff' } };
                }
            });
        });

        sheet.getCell('B1').font = { name: 'Calibri', family: 2, bold: true, size: 12, color: { argb: 'ffffffff' } };
        sheet.getCell('B1').alignment = { horizontal: 'center' };
        sheet.getCell('B2').alignment = { horizontal: 'center' };
        sheet.getCell('B3').alignment = { horizontal: 'center' };
        sheet.mergeCells('B1:D1');
        sheet.mergeCells('B2:D2');
        sheet.mergeCells('B3:D3');
        sheet.mergeCells('A4:B4');
        sheet.getCell('B1').value = mainTitle;
        sheet.getCell('B2').value = secondaryTitle;
        sheet.getCell('B3').value = dataDate;
        sheet.columns.forEach(col => {
            col.width = 20;
        });
        for (let i = sheet._rows.length - 1; i > -1; i--) {
            if (i - 4 > -1) sheet._rows[i].outlineLevel = sheet._rows[i - 4].outlineLevel;
            else sheet._rows[i].outlineLevel = 0;
        }
        let tempToday = new Date();
        let textDate = `${tempToday.getFullYear()}-${tempToday.getMonth() + 1}-${tempToday.getDate()}`;
        let textHour = `${tempToday.getHours()} - ${tempToday.getMinutes()} - ${tempToday.getDate()}`;
        sheet.getCell('A4').value = `fecha ${textDate} ${textHour} `;
        sheet.eachRow({ includeEmpty: true }, function (row, rowNumber) {
            row.eachCell(function (cell, colNumber) {
                if (rowNumber > 4) {
                    if (cell.value != null && typeof cell.value == 'string' && (cell.value.length == 19 || cell.value.length == 20) && (cell.value.match(/\d\d\d\d-\d\d-\d\d\s\s\d\d:\d\d:\d\d/) || cell.value.match(/\d\d\d\d-\d\d-\d\d\s\d\d:\d\d:\d\d/) || cell.value.match(/\d\d\d\d-\d\d-\d\d\d\d:\d\d:\d\d/))) {
                        let temp = cell.value.split(' ');
                        if (temp.length == 2) cell.value = new Date(temp[0] + 'T' + temp[1] + 'Z');
                    }
                }
            });
        });
        let excelBinaryData = await workbook.xlsx.writeBuffer();
        saveAs(new Blob([excelBinaryData], { type: 'application/octet-stream' }), fileName);
        console.log('%c excel promise resolved', 'background: green; color: white');
        resolve(true);
    });
}

function getEmptyArray(length) {
    return new Array(length).fill('');
}

