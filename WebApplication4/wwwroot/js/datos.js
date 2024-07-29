$(() => {
    const msInDay = 1000 * 60 * 60 * 24;
    const selectionModes = ['single', 'multiple', 'range'];
    const date = new Date().getTime();
    const calendar = $('#calendar').dxCalendar({



        value: [date, date + msInDay],
        showWeekNumbers: true,
        selectWeekOnClick: true,
        selectionMode: 'range',
    }).dxCalendar('instance');

    $('#select-week').dxCheckBox({
        text: 'Select week on click',
        value: true,
        onValueChanged(data) {
            calendar.option('selectWeekOnClick', data.value);
        },
    });

    $('#selection-mode').dxSelectBox({
        dataSource: selectionModes,
        value: selectionModes[2],
        inputAttr: { 'aria-label': 'Selection Mode' },
        onValueChanged(data) {
            calendar.option('selectionMode', data.value);
        },
    }).dxSelectBox('instance');

    $('#min-date').dxCheckBox({
        text: 'Set minimum date',
        onValueChanged(data) {
            const minDate = new Date(date - msInDay * 3);
            calendar.option('min', data.value ? minDate : null);
        },
    });

    $('#max-date').dxCheckBox({
        text: 'Set maximum date',
        onValueChanged(data) {
            const maxDate = new Date(date + msInDay * 3);
            calendar.option('max', data.value ? maxDate : null);
        },
    });

    $('#disable-dates').dxCheckBox({
        text: 'Disable weekends',
        onValueChanged(data) {
            if (data.value) {
                calendar.option('disabledDates', (d) => d.view === 'month' && isWeekend(d.date));
            } else {
                calendar.option('disabledDates', null);
            }
        },
    });



    // Configuración del File Uploader de DevExtreme
    const fileUploader = $('#file-uploader-container').dxFileUploader({
        multiple: false, // Permitir solo la selección de un archivo a la vez
        selectButtonText: 'Seleccionar archivo',
        accept: '*', // Permitir cualquier tipo de archivo
        uploadMode: 'instantly', // Subir el archivo inmediatamente después de seleccionarlo
        uploadUrl: '', // URL de tu servidor para manejar la carga de archivos (sustituir con tu URL real)
        labelText: 'Arrastra un archivo aquí o selecciona uno', // Texto mostrado en el área del uploader

        // Evento llamado cuando la carga del archivo es exitosa
        onUploaded: function (e) {
            DevExpress.ui.notify('Archivo cargado correctamente'); // Notificar carga exitosa
            console.log(e.request); // Mostrar información de la solicitud de carga (opcional)
        },

        // Evento llamado cuando hay un error en la carga del archivo
        onUploadError: function (e) {
            DevExpress.ui.notify('Error al cargar el archivo', 'error'); // Notificar error de carga
            console.error(e.request); // Mostrar información de la solicitud de carga (opcional)
        }
    }).dxFileUploader('instance');

    // Botón para borrar archivos erróneos o cancelar subidas
    $('#clear-files-button').dxButton({
        text: 'Borrar archivos',
        onClick: function () {
            fileUploader.reset();
            DevExpress.ui.notify('Archivos borrados'); // Notificar que se han borrado los archivos
        }
    });

    // Configuración del botón usando DevExtreme
    $('#accept-button').dxButton({
        stylingMode: 'contained',
        text: 'Aceptar',
        type: 'success',
        width: 200,
        onClick() {
            DevExpress.ui.notify('Espere....');
        }
    });

    function isWeekend(d) {
        const day = d.getDay();
        return day === 0 || day === 6;
    }
    $(() => {

        $('#calendar').css({
            'float': 'left',
            'margin-right': '700px' // Ajusta el margen derecho según sea necesario
        });


        $('#accept-button').css({
            'float': 'right',
            'margin-left': '-100px' // Ajusta el margen izquierdo según sea necesario
        });


        $('#file-uploader-container').css({
            'float': 'right',
            'margin-top': '-180px', // Ajusta el margen superior según sea necesario
            'margin-left': '524px' // Ajusta el margen izquierdo según sea necesario
        });


        $(() => {
            const msInDay = 1000 * 60 * 60 * 24;
            const date = new Date().getTime();

            const calendar = $('#calendar').dxCalendar({
                value: [date, date + msInDay],
                showWeekNumbers: true,
                selectionMode: 'range', // Modo de rango 
            }).dxCalendar('instance');

            $('#clear-button').dxButton({
                text: 'Clear value',
                onClick: () => { calendar.option('value', null); },
            });
        });


    });
});