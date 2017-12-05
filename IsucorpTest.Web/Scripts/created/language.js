$(document).ready(function () {
    SelectActiveLanguaje();
});

function SelectActiveLanguaje() {
    var cookies = document.cookie.split(';');
    cookies.forEach(function (valor, indice) {
        if (valor.indexOf('CultureInfo') !== -1) {
            if (valor.indexOf('es-ES') !== -1) {
                $('#spanishLanguage').addClass('btn-primary').removeClass('btn-default');
                $('#englishLanguage').addClass('btn-default').removeClass('btn-primary');
            } else {
                $('#englishLanguage').addClass('btn-primary').removeClass('btn-default');
                $('#spanishLanguage').addClass('btn-default').removeClass('btn-primary');
            }
        }
    });
}

$('#englishLanguage').click(function () {
    $.ajax({
        type: 'POST',
        url: '/Home/LanguageEnglish',       
        data: { },
        success: function () {           
            location.reload();
        },
        error: function (textStatus, errorThrown) {
            console.log(textStatus);
            console.log(errorThrown);
        }
    });
});

$('#spanishLanguage').click(function () {
    $.ajax({
        type: 'POST',
        url: '/Home/LanguageSpanish',
        data: {},
        success: function () {            
            location.reload();
        },
        error: function (textStatus, errorThrown) {
            console.log(textStatus);
            console.log(errorThrown);
        }
    });
});