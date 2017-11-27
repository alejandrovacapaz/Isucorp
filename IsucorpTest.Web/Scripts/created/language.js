$('#englishLanguage').click(function () {
    $('#englishLanguage').addClass('btn-primary').removeClass('btn-default');
    $('#spanishLanguage').addClass('btn-default').removeClass('btn-primary');
});

$('#spanishLanguage').click(function () {
    $('#spanishLanguage').addClass('btn-primary').removeClass('btn-default');
    $('#englishLanguage').addClass('btn-default').removeClass('btn-primary');
});