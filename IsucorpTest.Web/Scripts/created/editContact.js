var today = new Date();
var contactModel = {};
var initialDate = new Date(today.getFullYear(), today.getMonth(), today.getDate(), 0, 0, 0);

function enableEdit() {
    return contactModel.contactName() != "" && contactModel.birthDate() != "" && ($("#editBirthDate").datepicker("getDate") < initialDate);
}

function createModel(contactName, phoneNumber, birthDateText) {
    contactModel.contactName = ko.observable(contactName);
    contactModel.phoneNumber = ko.observable(phoneNumber);
    contactModel.birthDate = ko.observable(birthDateText);
    ko.applyBindings(contactModel);
}

$(document).ready(function () {   
    var aux = $("#editBirthDate").val();
    var dateString = aux.substr(0, aux.indexOf(' '));
    var birthDate = null;
    $("#editContactType").val(contactTypeId);
    $("#ContactTypeId").val($('#editContactType option:selected').val());
    var cookies = document.cookie.split(';');
    cookies.forEach(function (valor) {
        if (valor.indexOf('CultureInfo') !== -1) {           
            if (valor.indexOf('es-ES') !== -1) {
                birthDate = toDateSpanish(dateString);
                var options = $.extend({}, 
                    $.datepicker.regional['es'], {
                        maxDate: today,                        
                        dateFormat: "'Nacimiento: 'dd-mm-yy",
                        setdate: today
                    }
                );
                $("#editBirthDate").datepicker(options);                
                createTextArea("es");
            } else {
                birthDate = toDate(dateString);
                var options = $.extend({}, 
                    $.datepicker.regional['en-US'], {
                        maxDate: today,                       
                        dateFormat: "'Birth Date: 'mm-dd-yy",
                        setdate: today
                    }
                );
                $("#editBirthDate").datepicker(options);
                createTextArea("en");
            }
        }
    });    
    $('#editBirthDate').datepicker("setDate", birthDate);
    $("#editPhoneNumber").mask("(99) 9999-9999");
    createModel($("#editContactName").val().trim(), $("#editPhoneNumber").val().trim(), $("#editBirthDate").val());    
});

$('#contactType').change(function () {
    $("#ContactTypeId").val($('#editContactType option:selected').val());
});

var data;
$('#btnEditContact').on("click",
    function () {
        $("#errorsSection").empty();
        var error = false;        
        var contactBirthDate = $("#editBirthDate").datepicker("getDate");
        if (contactModel.contactName() === "") {
            $("#errorsSection").append('<span class="text-danger" style="padding-left: 5%">' + EditResources.NameError + '</span></br>');
            error = true;
        }
        if (contactModel.contactName().length < 3 || contactModel.contactName().length > 30) {
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
                Name: contactModel.contactName(),
                PhoneNumber: $("#editPhoneNumber").val().trim(),
                BirthDate: contactBirthDate,
                ContactTypeId: parseInt($("#ContactTypeId").val().trim()),
                Description: tinymce.get('editContactDescription').getContent(),
                BirthDateString: DatetoString(contactBirthDate)
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
$('#editBirthDate').blur(function () {
    tinyMCE.activeEditor.focus();
});

$("#editContactName").keydown(function (e) {
    onlyLetters(e);
});
