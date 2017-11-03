$("#searchElement").keyup(function () {
    var value = this.value.toLowerCase();

    if (value === "") {
        paginate();
    }
    else {
        $("table").find("tr").each(function (index) {
            if (index === 0) return;
            var id = $(this).find("td").first().text().toLowerCase();
            $(this).toggle(id.indexOf(value) !== -1);
        });
    }
});

