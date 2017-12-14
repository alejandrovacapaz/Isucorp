var today = new Date();
var birthDate = toDate($("#editBirthDate").val());

$(document).ready(function () {   
    $("#editContactType").val(contactTypeId);
    $("#ContactTypeId").val($('#editContactType option:selected').val());
    var cookies = document.cookie.split(';');
    cookies.forEach(function (valor, indice) {
        if (valor.indexOf('CultureInfo') !== -1) {
            if (valor.indexOf('es-ES') !== -1) {
                $("#editBirthDate").datepicker({ maxDate: new Date, dateFormat: "'Nacimiento: 'mm-dd-yy" });
                createTextArea("es");
            } else {
                $("#editBirthDate").datepicker({ maxDate: new Date, dateFormat: "'Birth Date: 'mm-dd-yy" });
                createTextArea("en");
            }
        }
    });
    $('#editBirthDate').datepicker("setDate", birthDate);
    $("#editPhoneNumber").mask("(99) 9999-9999");
});

$('#contactType').change(function () {
    $("#ContactTypeId").val($('#editContactType option:selected').val());
});

var data;
$('#btnEditContact').on("click",
    function () {
        $("#errorsSection").empty();
        var error = false;
        var contactName = $("#editContactName").val().trim();
        var contactBirthDate = $("#editBirthDate").datepicker("getDate");
        if (contactName === "") {
            $("#errorsSection").append('<span class="text-danger" style="padding-left: 5%">' + EditResources.NameError + '</span></br>');
            error = true;
        }
        if (contactName.length < 3 || contactName.length > 30) {
            $("#errorsSection").append('<span class="text-danger" style="padding-left: 5%">' + EditResources.NameLengthError + '</span></br>');
            error = true;
        }
        if (DatetoString(contactBirthDate) === DatetoString(today) || contactBirthDate > today) {
            $("#errorsSection").append('<span class="text-danger" style="padding-left: 5%">' + EditResources.BirthDateError + '</span>');
            error = true;
        }
        if (!error) {         
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
                    if (!result.success) {
                        $("#errorsSection").empty();
                        $("#errorsSection").append('<span class="text-danger" style="padding-left: 5%">' + EditResources.EditContactError + '</span>');
                        $('#clientErors').modal('show');
                    }                                         
                    else
                        GotoIndex();
                },
                error: function (textStatus, errorThrown) {
                    console.log(textStatus);
                    console.log(errorThrown);
                }
            });
        }
        else {
            $('#clientErors').modal('show');
        }
    });

// Methods to fix tinyMCE component tab problem, 
// Get focus after birthDate is blured
$('#birthDate').blur(function () {
    tinyMCE.activeEditor.focus();
});

$("#editContactName").keydown(function (e) {
    onlyLetters(e);
});
