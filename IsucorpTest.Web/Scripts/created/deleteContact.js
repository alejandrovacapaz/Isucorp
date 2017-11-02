$('.deleteContact').on("click",
    function (e) {        
        var id = this.id;
        var name = $(this).attr("title");
        $("#deleteContactName").text(name); // set name on modal
        prepareModal(e, "deleteContact");
        localStorage.setItem("deleteContactId", id);
    });

$('#btnDeleteContact').on("click",
    function () {
        var id = localStorage.getItem("deleteContactId");
        $.ajax({
            type: 'POST',
            url: '/Home/Delete',
            dataType: 'json',
            data: { contactId: id },
            success: function (result) {
                $('#deleteContact').modal('hide');
                if (!result.success)
                    alert("There was an error deleting the Contact, please try again");
                else
                    GotoIndex();
            },
            error: function (textStatus, errorThrown) {
                console.log(textStatus);
                console.log(errorThrown);
            }
        });
    });