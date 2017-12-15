function GotoIndex() {
    window.location.href = "/Home/Index";
}

function toDate(dateStr) { //MM/DD/YYYY
    var parts = dateStr.split("/");
    return new Date(parts[2], parts[0] - 1, parts[1]);
}

function toDateSpanish(dateStr) { //DD/MM/YYYY
    var parts = dateStr.split("/");
    return new Date(parts[2], parts[1] - 1, parts[0]);
}

function DatetoString(date) { //MM/DD/YYYY
    return ((date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
}

function prepareModal(e, modalName) {
    e.preventDefault();
    $('#' + modalName).modal('show');
}

function createTextArea(language) {
    tinymce.init({
        selector: 'textarea',
        branding: false,
        language: language,
        height: 500,
        theme: 'modern',
        plugins: 'print preview fullpage searchreplace autolink directionality visualblocks visualchars fullscreen image link media template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists textcolor imagetools contextmenu colorpicker textpattern help',
        toolbar1: 'formatselect | bold italic strikethrough forecolor backcolor | link | alignleft aligncenter alignright alignjustify  | numlist bullist outdent indent  | removeformat',
        image_advtab: true,
        templates: [
            { title: 'Test template 1', content: 'Test 1' },
            { title: 'Test template 2', content: 'Test 2' }
        ],
        content_css: [
            '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
            '//www.tinymce.com/css/codepen.min.css'
        ]
    });
}

function onlyLetters(e) {
    // Allow: backspace, delete, tab, escape, enter, space, Upper
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 32, 20]) !== -1 ||
        // Allow: Ctrl+A, Command+A
        (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
        // Allow: home, end, left, right, down, up
        (e.keyCode >= 35 && e.keyCode <= 40)) {
        // let it happen, don't do anything
        return;
    }
    // Ensure that it is a letter and stop the keypress   
    if ((e.keyCode > 64 && e.keyCode < 91)) {
        return
    }
    else{
        e.preventDefault();
    }   
}

