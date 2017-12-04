var today = new Date();
$("#birthDate").datepicker({ maxDate: new Date, dateFormat: "'Birth Date: ' MM-mm-dd-yy" });
$('#birthDate').datepicker("setDate", today);
$("#phoneNumber").mask("(99) 9999-9999");

tinymce.init({
    selector: 'textarea',
    height: 500,
    theme: 'modern',
    plugins: 'print preview fullpage searchreplace autolink directionality visualblocks visualchars fullscreen image link media template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists textcolor wordcount imagetools contextmenu colorpicker textpattern help',
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

$(document).ready(function () {
    $("#ContactTypeId").val($('#contactType option:selected').val());
});

$('#contactType').change(function () {
    $("#ContactTypeId").val($('#contactType option:selected').val());
});

var data;
$('#btnAddContact').on("click",
    function () {
        $("#clientErrors").empty();
        var error = false;
        var contactName = $("#contactName").val().trim();
        var contactBirthDate = $("#birthDate").datepicker("getDate");
        if (contactName === "") {
            $("#clientErrors").append('<label class="text-danger">Insert Contact Name</label></br>');
            error = true;
        }
        if (contactName.length < 3 || contactName.length > 30) {
            $("#clientErrors").append('<label class="text-danger">Name should be between 3 and 30 characters</label></br>');
            error = true;
        }
        if (DatetoString(contactBirthDate) === DatetoString(today) || contactBirthDate > today) {
            $("#clientErrors").append('<label class="text-danger">Review Birth Date, it is wrong </label>');
            error = true;
        }
        else if (!error){
            $("#clientErrors").hide();
            data = {
                Id: 0,
                Name: contactName,
                PhoneNumber: $("#phoneNumber").val().trim(),
                BirthDate: $('#birthDate').datepicker("getDate"),
                ContactTypeId: parseInt($("#ContactTypeId").val().trim()),
                Description: tinymce.get('contactDescription').getContent(),
                BirthDateString: DatetoString($('#birthDate').datepicker("getDate"))
            }
            $.ajax({
                type: 'POST',
                url: '/Home/Add',
                dataType: 'json',
                data: { contact: data },
                success: function (result) {
                    if (!result.success)
                        alert("There was an error adding the contact review data");
                    else
                        GotoIndex();
                },
                error: function (textStatus, errorThrown) {
                    console.log(textStatus);
                    console.log(errorThrown);
                }
            });
        }
        $("#clientErrors").show();
    });