$(document).ready(function () {
    $("#Username").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Home/showSugestions",
                type: "POST",
                dataType: "json",
                data: {Prefix:request.term},
                success: function (data) {
                    response($.map(data, function (item) {
                        return {  value: item.username};
                    }))

                }
            })
        },
        messages: {
            noResults: "", results: ""
        }
    });
}) 