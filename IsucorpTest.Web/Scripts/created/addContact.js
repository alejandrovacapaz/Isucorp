var today = new Date();
var initialDate = '';

var contactModel = {
    contactName: ko.observable(''),   
    birthDate: ko.observable('')       
};

function enableSave() {
    return contactModel.contactName() != "" && contactModel.birthDate() != "" && (contactModel.birthDate() < initialDate);
}

$(document).ready(function () {
    ko.applyBindings(contactModel);
    $("#ContactTypeId").val($('#contactType option:selected').val());    
    var cookies = document.cookie.split(';');
    cookies.forEach(function (valor) {
        if (valor.indexOf('CultureInfo') !== -1) {
            if (valor.indexOf('es-ES') !== -1) {                
                var options = $.extend({}, 
                    $.datepicker.regional['es'], {
                        maxDate: today,
                        dateFormat: "'Nacimiento: 'dd-mm-yy"                        
                    } 
                );
                $("#birthDate").datepicker(options);                
                createTextArea("es");
            } else {
                lang = "es";
                var options = $.extend({},     
                    $.datepicker.regional["en-US"], {
                        maxDate: today,
                        dateFormat: "'Birth Date: 'mm-dd-yy"                        
                    }
                );
                $("#birthDate").datepicker(options);
                createTextArea("en");
            }
        }
    });    
    $('#birthDate').datepicker("setDate", today);
    $("#phoneNumber").mask("(99) 9999-9999");
    $("#contactName").focus();
    initialDate = $("#birthDate").val();
});

$('#contactType').change(function () {
    $("#ContactTypeId").val($('#contactType option:selected').val());
});

var data;
$('#btnAddContact').on("click",
    function () {
        $("#errorsSection").empty();
        var error = false;        
        var contactBirthDate = $("#birthDate").datepicker("getDate");
        if (contactModel.contactName() === "") {
            $("#errorsSection").append('<span class="text-danger" style="padding-left: 5%">' + Resources.NameError + '</span></br>');
            error = true;
        }
        if (contactModel.contactName().length < 3 || contactModel.contactName().length > 30) {
            $("#errorsSection").append('<span class="text-danger" style="padding-left: 5%">' + Resources.NameLengthError + '</span></br>');
            error = true;
        }
        if (DatetoString(contactBirthDate) === DatetoString(today) || contactBirthDate > today) {
            $("#errorsSection").append('<span class="text-danger" style="padding-left: 5%">' + Resources.BirthDateError + '</span>');
            error = true;
        }
        if (!error){          
            data = {
                Id: 0,
                Name: contactModel.contactName(),
                PhoneNumber: $("#phoneNumber").val().trim(),
                BirthDate: contactBirthDate,
                ContactTypeId: parseInt($("#ContactTypeId").val().trim()),
                Description: tinymce.get('contactDescription').getContent(),
                BirthDateString: DatetoString(contactBirthDate)
            }
            $.ajax({
                type: 'POST',
                url: '/Home/Add',
                dataType: 'json',
                data: { contact: data },
                success: function (result) {
                    if (!result.success) {
                        $("#errorsSection").empty();
                        $("#errorsSection").append('<span class="text-danger" style="padding-left: 5%">' + Resources.AddContactError + '</span>');
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

$("#contactName").keydown(function (e) {
    onlyLetters(e);
});






