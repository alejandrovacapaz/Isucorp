var numPerPage = parseInt($("#resultPerPage").val());

$('#goResultPerPage').on("click",
    function () {
        numPerPage = parseInt($("#resultPerPage").val());
        paginate(numPerPage);
    });

$("#resultPerPage").keyup(function () {
    var perPage = $("#resultPerPage").val();
    if (perPage !== "") {
        numPerPage = parseInt(perPage);
        if (numPerPage >= 5 && numPerPage <= 10)
            return 
    }
        ($("#resultPerPage").val("7"));       
});


$('table.table-striped').each(function () {
    var currentPage = 0;
    var $table = $(this);
    $("div.pager").remove();
    $table.bind('repaginate', function () {
        $table.find('tbody tr').hide().slice(currentPage * numPerPage, (currentPage + 1) * numPerPage).show();
    });
    $table.trigger('repaginate');
    var numRows = $table.find('tbody tr').length;
    var numPages = Math.ceil(numRows / numPerPage);
    var $pager = $('<div class="pager"></div>');
    for (var page = 0; page < numPages; page++) {
        $('<span class="page-number"></span>').text(page + 1).bind('click', {
            newPage: page
        }, function (event) {
            currentPage = event.data['newPage'];
            $table.trigger('repaginate');
            $(this).addClass('active').siblings().removeClass('active');
        }).appendTo($pager).addClass('clickable');
    }

    $pager.insertAfter($table).find('span.page-number:first').addClass('active');
});

function paginate(num) {
    $('table.table-striped').each(function () {
        var currentPage = 0;
        var numPerPage = num || 7;
        var $table = $(this);
        $("div.pager").remove();
        $table.bind('repaginate', function () {
            $table.find('tbody tr').hide().slice(currentPage * numPerPage, (currentPage + 1) * numPerPage).show();
        });
        $table.trigger('repaginate');
        var numRows = $table.find('tbody tr').length;
        var numPages = Math.ceil(numRows / numPerPage);
        var $pager = $('<div class="pager"></div>');
        // delete previous spans first
        $("span.page-number").remove();
        for (var page = 0; page < numPages; page++) {
            $('<span class="page-number"></span>').text(page + 1).bind('click', {
                newPage: page
            }, function (event) {
                currentPage = event.data['newPage'];
                $table.trigger('repaginate');
                $(this).addClass('active').siblings().removeClass('active');
            }).appendTo($pager).addClass('clickable');
        }

        $pager.insertAfter($table).find('span.page-number:first').addClass('active');
    });
}