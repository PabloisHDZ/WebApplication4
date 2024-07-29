$(() => {
    $('#text3').dxTextBox({
        placeholder: 'Variación de tonelaje',
        value: '5-25%', // Valor por defecto del 5-25%
        onValueChanged: function (e) {
            console.log(e.value); // Log de cambios de valor (opcional)
        },
        // mask: 'numeric',
        // maskRules: {
        //     'X': /[0-9\-]/,
        //     '9': /[0-9]/
        // },
        maskChar: '_'
    });

    $('#text4').dxTextBox({
        placeholder: 'Ingrese tiempo',
        // value: '00:00:00', // Valor por defecto del tiempo
        value: '5-25 min.', // Valor por defecto del 5-25%
        onValueChanged: function (e) {
            console.log(e.value); // Log de cambios de valor (opcional)
        },
        // mask: '00:00:00'
    });


    // Inicializar botón de configuración del bot
    $('#button2').dxButton({
        stylingMode: 'contained',
        text: 'Configurar Bot',
        type: 'success',
        width: 200,
        onClick() {
            DevExpress.ui.notify('Configurando bot');
        },
    });


});