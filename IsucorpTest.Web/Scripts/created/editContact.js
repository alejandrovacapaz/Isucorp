var today = new Date();
var birthDate = toDate($("#editBirthDate").val());
$("#editBirthDate").datepicker({ maxDate: new Date, dateFormat: "'Birth Date: ' MM-mm-dd-yy" });
$('#editBirthDate').datepicker("setDate", birthDate);
$("#editPhoneNumber").mask("(99) 9999-9999");

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
    $("#editContactType").val(contactTypeId);
    $("#ContactTypeId").val($('#editContactType option:selected').val());
});

$('#contactType').change(function () {
    $("#ContactTypeId").val($('#editContactType option:selected').val());
});

var data;
$('#btnEditContact').on("click",
    function () {
        $("#editClientErrors").empty();
        var error = false;
        var contactName = $("#editContactName").val().trim();
        var contactBirthDate = $("#editBirthDate").datepicker("getDate");
        if (contactName === "") {
            $("#editClientErrors").append('<label class="text-danger">Insert Contact Name</label></br>');
            error = true;
        }
        if (contactName.length < 3) {
            $("#editClientErrors").append('<label class="text-danger">Name should be at least 3 characters</label></br>');
            error = true;
        }
        if (DatetoString(contactBirthDate) === DatetoString(today) || contactBirthDate > today) {
            $("#editClientErrors").append('<label class="text-danger">Review Birth Date, it is wrong </label>');
            error = true;
        }
        else if (!error) {
            $("#editClientErrors").hide();
            data = {
                Id: contactId,
                Name: contactName,
                PhoneNumber: $("#editPhoneNumber").val().trim(),
                BirthDate: $('#editBirthDate').datepicker("getDate"),
                ContactTypeId: parseInt($("#ContactTypeId").val().trim()),
                Description: tinymce.get('editContactDescription').getContent(),
                BirthDateString: DatetoString($('#editBirthDate').datepicker("getDate"))
            }
            $.ajax({
                type: 'POST',
                url: '/Home/Edit',
                dataType: 'json',
                data: { contact: data },
                success: function (result) {
                    if (!result.success)
                        alert("There was an error editing the contact review data");
                    else
                        GotoIndex();
                },
                error: function (textStatus, errorThrown) {
                    console.log(textStatus);
                    console.log(errorThrown);
                }
            });
        }
        $("#editClientErrors").show();
    });